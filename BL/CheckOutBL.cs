using System;
using Persistance;
using DAL;
using System.Collections.Generic;
namespace BL
{
    public class CheckOutBL
    {
        private CheckOutDAL checkoutDAL;

        public CheckOutBL()
        {
            checkoutDAL = new CheckOutDAL();
        }

        public CheckOut CheckOutbyRoomId(int roomID)
        {
            return checkoutDAL.CheckOutbyRoomId(roomID);
        }
    }
}