namespace Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeEmail (this string email)
        {
            var indexToSlice = email.IndexOf('+') != -1 ? email.IndexOf('+') : email.IndexOf('@');

            return email.Remove(indexToSlice);
        }
    }
}
