namespace RentaVex.Services.Data
{
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;

    public class GlobalService
    {
        public static string GetPageName(HttpContext httpContext)
        {
            string currentPageUrl = httpContext.Request.Path;
            string pattern = @"/([^/]+)$";

            Match match = Regex.Match(currentPageUrl, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return "Products";
            }
        }
    }
}
