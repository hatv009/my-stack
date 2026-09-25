using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shared.Domain.Common
{
    public enum EPaymentMethod
    {
        [Display(Name = "Tỷ")]
        Ty = 1,
        [Display(Name = "Triệu")]
        Trieu,
        [Display(Name = "Triệu USD")]
        TrieuUSD,
        [Display(Name = "USD")]
        USD,
    }

    public static class CommonMethod
    {
        public static T? CloneObject<T>(object param) where T : class
        {
            var json = JsonSerializer.Serialize(param);
            return JsonSerializer.Deserialize<T>(json);
        }
        public static decimal CalToPriceVnd(decimal price, EPaymentMethod paymentMethod)
        {
            if (paymentMethod == EPaymentMethod.Trieu)
                return price / 1000;

            if (paymentMethod == EPaymentMethod.USD)
                return price * 23 / 1000000;

            if (paymentMethod == EPaymentMethod.TrieuUSD)
                return price * 23;

            return price;
        }

        public static decimal CalToFullPrice(decimal price, EPaymentMethod paymentMethod)
        {
            if (paymentMethod == EPaymentMethod.Ty)
                return (price * 1000000) * 1000;
            if (paymentMethod == EPaymentMethod.Trieu)
                return price * 1000000;

            if (paymentMethod == EPaymentMethod.USD)
                return price * 23 * 1000;

            if (paymentMethod == EPaymentMethod.TrieuUSD)
                return price * 23 * 1000000;

            return price;
        }
        public static decimal CalToPrice(decimal fullPrice)
        {
            if (fullPrice >= 1000000000)
                return fullPrice / 1000000000;
            else return fullPrice / 1000000;
        }

        public static EPaymentMethod CalToMethod(decimal fullPrice)
        {
            return fullPrice >= 1_000_000_000 ? EPaymentMethod.Ty : EPaymentMethod.Trieu;
        }

        public static string ToMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
        public static string ConvertToNonUnicode(this string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static bool IsEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }

        private const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";
        public static string RandomPassword(int length)
        {
            char[] password = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomData = new byte[length];
                rng.GetBytes(randomData);

                for (int i = 0; i < length; i++)
                {
                    password[i] = ValidChars[randomData[i] % ValidChars.Length];
                }
            }
            return new string(password);
        }

        public static string HashPassword(string password, string salt)
           => Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                       password: password!,
                                       salt: Encoding.ASCII.GetBytes(salt),
                                       prf: KeyDerivationPrf.HMACSHA1,
                                       iterationCount: 100000,
                                       numBytesRequested: 256 / 8));

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageSize, int pageIndex) where T : class
            => source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

        public static string GetStrCompareWard(this string text)
        {
            return text
                .Replace("Phường 0", string.Empty)
                .Replace("Phường", string.Empty)
                .Replace("Xã", string.Empty)
                .Replace("xã", string.Empty)
                .Replace("một phần", string.Empty)
                .Replace("Phần còn lại của", string.Empty)
                .Replace(" - Cũ", string.Empty)
                .Trim();
        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
