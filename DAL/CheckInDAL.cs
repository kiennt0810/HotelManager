using System;
using Persistance;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL

{
    public class CheckInDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();

        private MySqlDataReader reader;

        public CheckIn GetCheckInById(string CheckInId)
        {
            CheckIn checkin = null;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"select * from checkin where checkin_id=" + CheckInId + ";";

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    checkin = GetCheckInInfo(reader);
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
            return checkin;
        }

        private CheckIn GetCheckInInfo(MySqlDataReader reader)
        {
            CheckIn chk = new CheckIn();
            chk.CheckInId = reader.GetString("checkin_id");
            chk.StaffId = reader.GetString("staffid");
            chk.Customer_Id = reader.GetInt32("customerid");
            chk.RTname = reader.GetString("rtname");
            chk.Check_In = reader.GetDateTime("checkin");
            chk.Check_Out = reader.GetDateTime("checkout");
            chk.Status = reader.GetInt32("checkstatus");
            return chk;
        }
        public List<CheckIn> GetListCheckIn()
        {
            List<CheckIn> listCheckIn = new List<CheckIn>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select * from checkin";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CheckIn check = new CheckIn();
                        check.CheckInId = reader.GetString("checkin_id");
                        check.StaffId = reader.GetString("staffid");
                        check.Customer_Id = reader.GetInt32("customerid");
                        check.RTname = reader.GetString("rtname");
                        check.Check_In = reader.GetDateTime("checkin");
                        check.Check_Out = reader.GetDateTime("checkout");
                        check.Status = reader.GetInt32("checkstatus");
                        listCheckIn.Add(check);
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
            }
            return listCheckIn;
        }
        
    }
}