namespace BookSale.Management.Application.Abstracts
{
    public interface IErrorMessageService
    {
        string GetErrorMessage(string errorCode);
        bool IsValidPhoneNumber(string phoneNumber);
    }
}
