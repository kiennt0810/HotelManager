using System;
using Persistance;
using DAL;
using System.Collections.Generic;
namespace BL
{
    public class CheckInBL
    {
        private CheckInDAL checkinDAL;

        public CheckInBL()
        {
            checkinDAL = new CheckInDAL();
        }

        public CheckIn GetCheckInById(string CheckInId)
        {
            return checkinDAL.GetCheckInById(CheckInId);
        }
        public List<CheckIn> GetListCheckIn()
        {
            return checkinDAL.GetListCheckIn();
        }
    }
}