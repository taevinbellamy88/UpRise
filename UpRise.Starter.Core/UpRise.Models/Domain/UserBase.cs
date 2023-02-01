using UpRise.Models.Interfaces;

namespace UpRise.Models.Domain
{
    public class UserBase : IUserAuthData
    {
        public int Id
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public IEnumerable<string> Roles
        {
            get; set;
        }
    }
}