using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShare.Domain.Entities;

namespace CarShare.Domain.Abstract
{
    public interface IUserRepository
    {
        User GetUserByID(int userID);

        void Save(User user);

        User Authenticate(string userName, string pwd);
    }
}
