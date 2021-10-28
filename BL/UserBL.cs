using System;
using DAL;
using Persistance;
namespace BL
{
    public class UserBL
    {
        private UserDal dal = new UserDal();
        
        public User Login(string userName, string password)
        {
            return dal.Login(userName, password);
        }
    }
}