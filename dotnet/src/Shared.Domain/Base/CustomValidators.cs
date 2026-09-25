using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Shared.Domain.Base
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> MatchAddressNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(
                new RegularExpressionValidator<T>
                (@"([aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9\/\-\+]*)"))
                .WithMessage("Số nhà sai định dạng!")
                ;
        }
        public static IRuleBuilderOptions<T, string> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                        .Must(phone => !string.IsNullOrEmpty(phone) && BeAValidPhoneNumber(phone))
                        .WithMessage("SĐT sai định dạng!");
        }

        public static IRuleBuilderOptions<T, string> MatchPasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^(?=.*[A-Z])(?=.*\d).{8,}$")
                .WithMessage("Mật khẩu phải có ít nhất 8 kí tự, bao gồm ít nhất 1 chữ in hoa và 1 chữ số.");
        }

        private static bool BeAValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Vietnamese mobile phone format (as of 2024)
            var pattern = @"^(0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$";
            return Regex.IsMatch(phone, pattern);
        }

    }
}
