using System.Collections.Generic;
using UpRise.Models.Interfaces;

namespace UpRise.Models.Domain
{
    public class UserBase : IUserAuthData
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public IEnumerable<string> Roles
        {
            get; set;
        }

        public object TenantId
        {
            get; set;
        }
    }
}