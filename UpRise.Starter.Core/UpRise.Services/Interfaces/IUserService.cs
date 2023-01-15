using UpRise.Requests.User;

namespace UpRise
{
    public interface IUserService
    {
        int Create(UserAddRequest userModel);

        void InsertToken(string token, int userId, int tokenType);

        void ConfirmUser(string token);

        Task<bool> LogInAsync(string email, string password);

        Task<bool> LogInTest(string email, string password, int id, string[] roles = null);


        void DeleteToken(string token);
    }
}