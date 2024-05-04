using BookSale.Management.Application.Abstracts;
using System.Text.RegularExpressions;

namespace BookSale.Management.Application.Services
{

    public class ErrorMessageService : IErrorMessageService
    {
        private readonly Dictionary<string, string> _errorMessages = new Dictionary<string, string>
        {
            // Mật khẩu
            { "PasswordRequiresDigit", "Mật khẩu phải có ít nhất một chữ số." },
            { "PasswordTooShort", "Mật khẩu phải có ít nhất 8 ký tự." },
            { "PasswordRequiresLower", "Mật khẩu phải có ít nhất một ký tự viết thường." },
            { "PasswordRequiresUpper", "Mật khẩu phải có ít nhất một ký tự viết hoa." },
            { "PasswordRequiresUniqueChars", "Mật khẩu phải có 5 ký tự khác nhau." },
            { "PasswordRequiresNonAlphanumeric", "Mật khẩu phải có ít nhất một ký tự đặc biệt." },

            // Email
            { "DuplicateEmail", "Địa chỉ email đã được sử dụng." },
            { "InvalidEmail", "Địa chỉ email không hợp lệ." },

            // Username
            { "DuplicateUserName", "Username đã được sử dụng." },

            // Phone
            { "InvalidPhoneNumber", "Số điện thoại không hợp lệ." }
        };

        public string GetErrorMessage(string errorCode)
        {
            if (_errorMessages.TryGetValue(errorCode, out string errorMessage))
            {
                return errorMessage;
            }

            return null;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra nếu số điện thoại chỉ chứa các ký tự số
            if (!Regex.IsMatch(phoneNumber, @"^\d+$"))
                return false;

            // Kiểm tra số lượng ký tự từ 10 đến 11
            if (phoneNumber.Length < 10 || phoneNumber.Length > 11)
                return false;

            // Kiểm tra định dạng số điện thoại Việt Nam
            string pattern = @"^(0?\d{9,10})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
