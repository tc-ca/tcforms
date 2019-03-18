using System;
using System.Globalization;
using System.Threading;

namespace TCFORMS.Extensions
{
    public static class CultureInfoExtensions
    {
        public static T RunWithCulture<T>(this CultureInfo culture, Func<T> func)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            var oldUiCulture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                return func();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = oldCulture;
                Thread.CurrentThread.CurrentUICulture = oldUiCulture;
            }
        }

        public static void RunWithCulture(this CultureInfo culture, Action action)
        {
            culture.RunWithCulture<object>(() =>
            {
                action();
                return null;
            });
        }
    }
}