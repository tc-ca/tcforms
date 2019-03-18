using GoC.WebTemplate;
using KEEN.Entities.Models;
using KEEN.Entities.Models.Fields;
using MoreLinq;
using Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using TCFORMS.Extensions;
using TCMailer;

namespace TCFORMS.Controllers
{
    public class FormController : BaseController
    {
        // GET: Display
        [Route("form")]
        public async Task<ActionResult> Index()
        {
            CurrentUser = await GetCurrentNTCUser();
            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);
            var form = formResponse.Response;

            AddBreadcrumb(form.Label);
            AddLeftMenu(form);

            return View(form);
        }

        [HttpPost]
        [Route("form")]
        public async Task<ActionResult> Index(FormCollection values)
        {

            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);
            return RedirectToAction("Section", new { id = formResponse.Response.Sections.First().Id });
        }

        // GET: Display
        [Route("form/section/{id}")]
        public async Task<ActionResult> Section(int id)
        {
            CurrentUser = await GetCurrentNTCUser();
            var userId = await GetUserId();
            var sectionResponse = await ApiClient.Section(Config.ProgramId, Config.FormId, id, userId, Language);
            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);

            AddBreadcrumb(formResponse.Response.Label, Url.Action("Index"));
            AddBreadcrumb(sectionResponse.Response.Label);
            AddLeftMenu(formResponse.Response);

            return View(sectionResponse.Response);
        }

        [HttpPost]
        [Route("form/section/{id}")]
        public async Task<ActionResult> Section(int id, FormCollection values)
        {
            var regex = new Regex(@"^(\d+)(?:\[\])?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var postData = values.AllKeys
                .Where(k => regex.IsMatch(k))
                .ToDictionary(
                    k => Convert.ToInt32(regex.Match(k).Groups[1].Value),
                    v => values[v]
                );

            // Add hidden fields for missing checkboxes
            regex = new Regex(@"^(\d+).hidden?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            values.AllKeys
                .Where(k => regex.IsMatch(k))
                .ForEach(k =>
                {
                    var key = Convert.ToInt32(regex.Match(k).Groups[1].Value);
                    if (!postData.ContainsKey(key))
                    {
                        postData.Add(key, values[k]);
                    }
                });

            var userId = await GetUserId();
            await ApiClient.SaveSection(Config.ProgramId, Config.FormId, id, userId, postData, Language);

            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);
            var form = formResponse.Response;

            var section = form.Sections.First(s => s.Id == id);
            var targetId = (int?)id;
            var dict = new Dictionary<string, int?>
            {
                { "previous", section.PreviousSectionId },
                { "next", section.NextSectionId }
            };
            if (dict.ContainsKey(values["nav"])) targetId = dict[values["nav"]];

            return targetId == null
                ? RedirectToAction("Review")
                : RedirectToAction("Section", new { id = targetId });
        }

        // GET: Review
        [Route("form/review")]
        public async Task<ActionResult> Review()
        {
            CurrentUser = await GetCurrentNTCUser();
            var userId = await GetUserId();
            var sectionsResponse = await ApiClient.Sections(Config.ProgramId, Config.FormId, Language, true, userId);
            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);

            AddBreadcrumb(formResponse.Response.Label, Url.Action("Index"));
            AddBreadcrumb(Labels.Review);
            AddLeftMenu(formResponse.Response);

            return View(sectionsResponse.Response);
        }

        // GET: Submit
        [Route("form/submit")]
        public async Task<ActionResult> Submit(bool? hasError = false)
        {
            CurrentUser = await GetCurrentNTCUser();
            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);

            AddBreadcrumb(formResponse.Response.Label, Url.Action("Index"));
            AddBreadcrumb(Labels.Submit);
            AddLeftMenu(formResponse.Response);

            if (hasError == true)
            {
                ViewBag.ErrorMessage = Labels.SubmitErrorMessage;
            }

            return View();
        }

        // GET: Complete
        [Route("form/complete")]
        public async Task<ActionResult> Complete()
        {
            CurrentUser = await GetCurrentNTCUser();
            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);

            AddBreadcrumb(formResponse.Response.Label, Url.Action("Index"));
            AddBreadcrumb(Labels.Submit);

            return View();
        }

        // POST: Complete
        [HttpPost]
        [ActionName("Complete")]
        [Route("form/complete")]
        public async Task<ActionResult> Complete_Post()
        {
            var userId = await GetUserId();
            var submissionId = (await ApiClient.GetCurrentSubmissionId(Config.ProgramId, userId, Config.FormId, Language)).Response;
            if (submissionId == null)
            {
                return RedirectToAction("Submit", new { hasError = true });
            }

            var contactFields = (await ApiClient.GetFields(Config.ProgramId, userId, submissionId.Value, "contact", Language)).Response;
            var ccFields = (await ApiClient.GetFields(Config.ProgramId, userId, submissionId.Value, "contact-cc", Language)).Response;

            var recipients = string.Join(" ", contactFields.OfType<TextField>().Select(f => f.Value));
            var cc = string.Join(" ", ccFields.OfType<TextField>().Select(f => f.Value));

            if (string.IsNullOrWhiteSpace(recipients))
            {
                return RedirectToAction("Submit", new { hasError = true });
            }

            await ApiClient.Submit(Config.ProgramId, userId, submissionId.Value, Language);

            var formResponse = await ApiClient.Form(Config.ProgramId, Config.FormId, Language);

            AddBreadcrumb(formResponse.Response.Label, Url.Action("Index"));
            AddBreadcrumb(Labels.Submit);

            CreateAndSendEmail(submissionId.Value, recipients, cc);

            return View();
        }

        // GET: Preview
        [Route("form/preview/{id}")]
        public async Task<ActionResult> Preview(int id)
        {
            CurrentUser = await GetCurrentNTCUser();
            bool isAdmin = await IsUserAdmin();
            TCWebAPI.WebApiResponse<Form> submissionResponse;

            ViewBag.isAdmin = isAdmin;

            // If admin, bypass user matching
            if (isAdmin)
            {
                submissionResponse = await ApiClient.GetSubmission(Config.ProgramId, id, Language);
            }
            else
            {
                var userId = await GetUserId();
                submissionResponse = await ApiClient.GetSubmission(Config.ProgramId, userId, id, Language);
            }

            AddBreadcrumb(submissionResponse.Response.Label);

            return View(submissionResponse.Response);
        }

        private void CreateAndSendEmail(int submissionId, string recipient, string cc)
        {
            using (var client = InitializeEmailClient())
            {
                try
                {
                    client.MessageFromAddress = client.MessageBccAddress = Config.SenderEmail;
                    client.MessageRecipientAddress = recipient;
                    client.MessageCCAddress = cc;

                    client.MessageSubject = EmailConfirmation.Subject;

                    var generateBody = new Func<string>(() => $"{EmailConfirmation.Paragraph1}\n\n" +
                                             $"{EmailConfirmation.Link}\n" +
                                             $"{Url.Action("Preview", "Form", new {id = submissionId}, Request?.Url?.Scheme)}\n\n" +
                                             $"{EmailConfirmation.Paragraph2}\n\n" +
                                             $"{EmailConfirmation.Paragraph3}\n\n" +
                                             $"{EmailConfirmation.ThankYou}");

                    var englishCulture = CultureInfo.CreateSpecificCulture("en-CA");
                    var frenchCulture = CultureInfo.CreateSpecificCulture("fr-CA");
                    client.MessageBody = englishCulture.RunWithCulture(generateBody) + "\n\n" +
                                         "****************\n\n" +
                                         frenchCulture.RunWithCulture(generateBody);

                    var sendSuccess = client.Send();

                    if (!sendSuccess)
                    {
                        switch (client.GetError.ErrorType)
                        {
                            case Error.Type.NONE:
                                Logger.Debug("No errors occurred.");
                                break;

                            case Error.Type.EMAIL_NOT_SENT_FAILSAFE_SUCCEEDED:
                                Logger.Error("Email not sent. Saved to failsafe location to be sent later. "
                                             + "Possible connection problem with server.");
                                break;

                            case Error.Type.EMAIL_RECIPIENT_FIELD_BLANK:
                                Logger.Error("The email recipient field is blank. Possible malformed email "
                                             + "address. Email not sent. Not saved to failsafe.");
                                break;

                            case Error.Type.EMAIL_FROM_FIELD_BLANK:
                                Logger.Error("The email from field is blank. Possible malformed email address. "
                                             + "Email not sent. Not saved to failsafe.");
                                break;

                            case Error.Type.EMAIL_NOT_SENT_FAILSAFE_FAILED:
                                Logger.Error("Email not sent, was not saved in failsafe, will not be sent at "
                                             + "a later time.");
                                break;

                            default:
                                Logger.Error("An unknown error occured while trying to send an email.");
                                break;
                        }
                    }
                }
                catch (EmailFormatException e)
                {
                    Logger.Error($"Email failed to send due to formatting errors: {e.Message}");
                }
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var result = base.BeginExecuteCore(callback, state);

            AddBreadcrumb(Labels.ApplicationMenuHeader, Url.Action("Index", "Home"));

            return result;
        }

        private void AddLeftMenu(Form form)
        {
            WebTemplateCore.LeftMenuItems.Add(new MenuSection
            {
                Name = form.Label,
                Items = new List<Link>
                {
                    new MenuItem { Href = Url.Action("Index"), Text = Labels.Overview }
                }.Concat(form.Sections.Select(s => new MenuItem
                {
                    Href = Url.Action("Section", new { id = s.Id }),
                    Text = s.Label
                })).Concat(new List<Link>
                {
                    new MenuItem { Href = Url.Action("Review"), Text = Labels.Review },
                    new MenuItem { Href = Url.Action("Submit"), Text = Labels.Submit }
                }).ToList()
            });
        }
    }
}
