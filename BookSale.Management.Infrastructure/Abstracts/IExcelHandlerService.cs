namespace BookSale.Management.Infrastructure.Abstracts
{
    public interface IExcelHandlerService
    {
        Task<byte[]> Export<T>(List<T> data) where T : class, new();
    }
}
