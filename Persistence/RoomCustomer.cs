using System;
using System.Collections.Generic;

namespace Persistance
{
    public class RoomCustomer 
    {
        public Room RoomInfo {set;get;}
        public List<Customer> CustomerList {set;get;} = new List<Customer>();
        public List<Service> ServiceList{set;get;} = new List<Service>();
    }
}