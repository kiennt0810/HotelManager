using System;
using Xunit;
using Persistance;
using DAL;

namespace DAL.TEST
{
   public class UserDalTest
    {
        private UserDal uDal = new UserDal();
        [Fact]
        public void LoginTest1()
        {
            string userName = "pf11";
            string userPass = "pf11VTCAcademy";
            User result = uDal.Login(userName, userPass);
            Assert.True(result != null);
            Assert.True(result.UserAccount.Equals(userName));
            Assert.True(result.Role == 1);
        }

        [Theory]
        [InlineData("pf111", "pf11VTCAcademy")]
        [InlineData("pf11", "pf11VTCAcademy1")]
        [InlineData("asdfg", "pf11VTCAcademy")]
        [InlineData("pf11", "sdfgsdhfb")]
        public void LoginTest2(string userName, string userPass)
        {
            User result = uDal.Login(userName, userPass);
            Assert.True(result == null);
        }
    }
}
