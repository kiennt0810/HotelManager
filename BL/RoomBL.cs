using System;
using Persistance;
using DAL;
using System.Collections.Generic;
namespace BL
{
    public class RoomBL
    {
        private RoomDAL roomDAL;

        public RoomBL()
        {
            roomDAL = new RoomDAL();
        }

        public Room GetRoomById(int Room_Id)
        {
            return roomDAL.GetRoomById(Room_Id);
        }
        public List<Room> GetRooms()
        {
            return roomDAL.GetRooms();
        }
    }
}