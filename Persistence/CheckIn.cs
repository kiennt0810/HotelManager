using System;

namespace Persistance
{
    public class CheckIn
    {
        public string CheckInId{set; get;}
        public string StaffId{set; get;}
        public int Customer_Id{set;get;}
        public string RTname {set;get;}
        public DateTime Check_In {set;get;}
        public DateTime Check_Out {set;get;}
        public int Status {set; get;}
    }
}
