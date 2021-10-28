using System;
using Persistance;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class RoomDAL
    {
        // private string query;

        private MySqlConnection connection = DbHelper.GetConnection();

        private MySqlDataReader reader;

        public Room GetRoomById(int Room_Id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"select * from room where roomid=" + Room_Id + ";";

            connection.Open();
            reader = command.ExecuteReader();

            Room room = null;
            if (reader.Read())
            {
                room = GetRoomInfo(reader);
            }
            connection.Close();
            return room;
        }

        private Room GetRoomInfo(MySqlDataReader reader)
        {
            Room rom = new Room();
            rom.Room_Id = reader.GetInt32("roomid");
            rom.RoomTypeInfo = reader.GetString("rtname");
            rom.Room_Status = reader.GetInt32("roomstatus");

            return rom;
        }
        public List<Room> GetRooms()
        {
            List<Room> listRoom = new List<Room>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select * from room where roomstatus = 0;";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Room rom = new Room();
                        rom.Room_Id = reader.GetInt32("roomid");
                        rom.RoomTypeInfo = reader.GetString("rtname");
                        rom.Room_Status = reader.GetInt32("roomstatus");
                        listRoom.Add(rom);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                finally
                {
                    connection.Close();
                }
            }
            return listRoom;
        }
    }
}