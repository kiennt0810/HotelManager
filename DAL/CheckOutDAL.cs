using System;
using Persistance;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class CheckOutDAL
    {

        private MySqlConnection connection = DbHelper.GetConnection();

        private MySqlDataReader reader;

        public CheckOut CheckOutbyRoomId (int roomID)
        {
             CheckOut checkout = null;
            try
            {
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"select roomid as MaPhong, roomtype.rtname as LoaiPhong, TIMESTAMPDIFF(DAY,checkin.checkin, current_timestamp()) as SoNgayO, 
                                    TIMESTAMPDIFF(DAY,checkin.checkin, current_timestamp()) * roomtype.price as GiaTien 
                                    from room inner join roomtype  
                                    on room.rtname = roomtype.rtname 
                                    inner join checkin
                                    on roomtype.rtname = checkin.rtname where room.roomstatus = '1' group by roomid order by roomid;";

            reader = command.ExecuteReader();
            if (reader.Read())
                {
                checkout = GetCheckOutInfo(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return checkout;
        }
         private CheckOut GetCheckOutInfo(MySqlDataReader reader)
        {
            CheckOut chk = new CheckOut();
            chk.roomID = reader.GetInt32("MaPhong");
            chk.TimeStay = reader.GetInt32("SoNgayO");
            chk.Price = reader.GetInt32("GiaTien");
            return chk;
        }
    }
}