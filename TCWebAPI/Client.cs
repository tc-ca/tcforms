using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using KEEN.Entities.Models;
using log4net;
using Newtonsoft.Json;

namespace TCWebAPI
{
    public class Client
    {
        public const string CorrelationHeader = "X-Correlation-ID";

        private readonly HttpClient _client;
        private readonly ILog _logger;
        public string EndPoint { get; }
        public int Version { get; }

        private readonly string _authEndpoint;

        private readonly string _accessToken;

        // Overload in case the client doesn't use log4net
        public Client(string endpoint, int version = 1)
        {
            _logger = LogManager.GetLogger(GetType());
            EndPoint = endpoint;
            Version = version;
            _client = InitializeClient();

            var disco = new DiscoveryClient(Config.IdSrvBaseUrl).GetAsync().Result;

            _authEndpoint = disco.TokenEndpoint;
            _accessToken = RequestToken().AccessToken;
        }

        public Client(string endpoint, ILog logger, int version = 1)

        {
            _logger = logger;
            EndPoint = endpoint;
            Version = version;
            _client = InitializeClient();
            var disco = new DiscoveryClient(Config.IdSrvBaseUrl).GetAsync().Result;

            _authEndpoint = disco.TokenEndpoint;
            _accessToken = RequestToken().AccessToken;

        }

        private HttpClient InitializeClient()
        {
            //TODO: REMOVE THIS - IT IS A MASSIVE SECURITY VULNERABILITY.
            //Server should be configured to accept it's own certificate.
            ServicePointManager.ServerCertificateValidationCallback =
                delegate
                {
                    return true;
                };

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{EndPoint}"),
                Timeout = TimeSpan.FromSeconds(30)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            return httpClient;
        }

        private TokenResponse RequestToken()
        {
            var client = new TokenClient(_authEndpoint, Config.TcForms, Config.TcFormsSecret);
            var token = client.RequestClientCredentialsAsync("keen").Result;


            return token;
        }

        private async Task<WebApiResponse<T>> PerformRequest<T>(string endpoint, string language, HttpMethod method = null, HttpContent postData = null, string mediaType = "application/json")
        {
            method = method ?? HttpMethod.Get;
            var msg = new HttpRequestMessage(method, endpoint)
            {
                Content = postData
            };

            if (msg.Content != null)
            {
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            }

            var correlationId = Guid.NewGuid().ToString();
            msg.Headers.Add(CorrelationHeader, correlationId);
            msg.Headers.Add("ACCEPT-LANGUAGE", language);

            var culture = new CultureInfo(language);
            msg.Headers.Add("ACCEPT-LANGUAGE", $"{culture.TwoLetterISOLanguageName};q=0.9");
            msg.Headers.Add("ACCEPT-LANGUAGE", "*;q=0.1");

            //TODO: Only include when going to an API endpoint that needs it (will work as-is)
            _client.SetBearerToken(_accessToken);

            _logger.Info($"Calling KeenAPI with {method?.Method} and correlationId {correlationId}: {endpoint}");

            var response = await _client.SendAsync(msg);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (!response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    _logger.Error($"Keen returned status code {response.StatusCode.ToString()} with correlationId {correlationId}");
                    _logger.Error(contents);
                    throw new HttpRequestException("Request Failed");
                }
                else
                {
                    _logger.Error($"Keen rejected authorization {_accessToken} with correlationId {correlationId}");
                    throw new HttpRequestException("API denied authorization.");
                }
            }


            _logger.Info($"Keen successully returned with correlationId {correlationId}");

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

            return new WebApiResponse<T>
            {
                CorrelationId = correlationId,
                Response = JsonConvert.DeserializeObject<T>(contents, settings),
                IsError = !response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };
        }

        public async Task<WebApiResponse<Form>> Form(int programId, int formId, string language)
        {
            return await PerformRequest<Form>($"programs/{programId}/forms/{formId}", language);
        }

        public async Task<WebApiResponse<Form>> GetSubmission(int programId, int userId, int submissionId, string language)
        {
            var url = $"programs/{programId}/users/{userId}/submissions/{submissionId}";
            return await PerformRequest<Form>(url, language);
        }

        public async Task<WebApiResponse<Form>> GetSubmission(int programId, int submissionId, string language)
        {
            var url = $"programs/{programId}/submissions/{submissionId}";
            return await PerformRequest<Form>(url, language);
        }

        public async Task<WebApiResponse<IList<Section>>> Sections(int programId, int formId, string language, bool addFields = false, int? userId = null)
        {
            var url = $"programs/{programId}/forms/{formId}/sections?addFields={addFields}&userId={userId}";
            return await PerformRequest<IList<Section>>(url, language);
        }

        public async Task<WebApiResponse<Section>> Section(int programId, int formId, int sectionId, int userId, string language)
        {
            var url = $"programs/{programId}/forms/{formId}/sections/{sectionId}?userId={userId}";
            return await PerformRequest<Section>(url, language);
        }

        public async Task<WebApiResponse<string>> SaveSection(int programId, int formId, int sectionId, int userId, IDictionary<int, string> fields, string language)
        {
            var url = $"programs/{programId}/forms/{formId}/sections/{sectionId}/save?userId={userId}";
            return await PerformRequest<string>(url, language, HttpMethod.Put, new StringContent(JsonConvert.SerializeObject(fields)));
        }

        public async Task<WebApiResponse<string>> Submit(int programId, int userId, int submissionId, string language)
        {
            var url = $"programs/{programId}/users/{userId}/submissions/{submissionId}/submit";
            return await PerformRequest<string>(url, language, HttpMethod.Put);
        }

        public async Task<WebApiResponse<int>> GetOrCreateUser(int programId, string identifier, string language)
        {
            var url = $"programs/{programId}/users/create";
            return await PerformRequest<int>(url, language, HttpMethod.Post, new StringContent(JsonConvert.SerializeObject(identifier)));
        }

        public async Task<WebApiResponse<IList<Field>>> GetFields(int programId, int userId, int submissionId, string fieldType, string language)
        {
            var url = $"programs/{programId}/users/{userId}/submissions/{submissionId}/fields/{fieldType}";
            return await PerformRequest<IList<Field>>(url, language);
        }
        public async Task<WebApiResponse<int?>> GetCurrentSubmissionId(int programId, int userId, int formId, string language)
        {
            var url = $"programs/{programId}/users/{userId}/submissions/current/{formId}";
            return await PerformRequest<int?>(url, language);
        }
    }
}
