using System;

namespace Persistance
{
    public class User
    {
        public string UserId {set;get;}
        public string UserAccount {set;get;}
        public string UserPassword {set;get;}
        public string UserName {set;get;}
        public string UserAddress {set;get;}
        public int Role {set;get;}
        public bool IsActive {set;get;}
    }
}