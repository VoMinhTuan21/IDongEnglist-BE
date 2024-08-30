using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace IDonEnglist.Application.Utils
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Normalize the string to remove accents
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) // Remove accents
                {
                    stringBuilder.Append(c);
                }
            }

            // Convert to string and normalize again
            var slug = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Replace spaces and non-alphanumeric characters with hyphens
            slug = Regex.Replace(slug, @"\s+", "-"); // Replace spaces with hyphens
            slug = Regex.Replace(slug, @"[^a-zA-Z0-9\-]", ""); // Remove non-alphanumeric characters
            slug = slug.ToLowerInvariant(); // Convert to lowercase

            return slug;
        }
    }
}
