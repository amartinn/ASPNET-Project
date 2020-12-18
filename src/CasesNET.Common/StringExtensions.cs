namespace CasesNET.Common
{
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToSEOFriendlyURL(this string title, int maxLength = 20)
        {
            var match = Regex.Match(title.ToLower(), "[\\w]+");
            var result = new StringBuilder(string.Empty);
            var maxLengthHit = false;
            while (match.Success && !maxLengthHit)
            {
                if (result.Length + match.Value.Length <= maxLength)
                {
                    result.Append(match.Value + "-");
                }
                else
                {
                    maxLengthHit = true;
                    if (result.Length == 0)
                    {
                        result.Append(match.Value.Substring(0, maxLength));
                    }
                }

                match = match.NextMatch();
            }

            if (result[^1] == '-')
            {
                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }
    }
}
