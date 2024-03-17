using BookSale.Management.Application.Abstracts;

namespace BookSale.Management.Application.Services
{
    public class CommonService : ICommonService
    {
        //Tạo code random cho book
        public string GenerateRandomCode(int number)
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();

            var blindCharacters = Enumerable.Range(0, number).Select(c => characters[random.Next(0, characters.Length)]);    //random các ký tự từ 0 -> số truyền vào

            return new string(blindCharacters.ToArray());
        }
    }
}
