namespace IDonEnglist.Application.Utils
{
    public static class UrlValidator
    {
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            // Use Uri class to check if the string is a valid URL
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
