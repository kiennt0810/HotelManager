using System;
using Persistance;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL

{
    public class CustomerDAL
    {
        // private string query;

        private MySqlConnection connection = DbHelper.GetConnection();

        private MySqlDataReader reader;

        public Customer GetCustomerById(int CustomerId)
        {
            Customer customer = null;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"select * from customer where customerid=" + CustomerId + ";";

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = GetCustomerInfo(reader);
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
            return customer;
        }

        private Customer GetCustomerInfo(MySqlDataReader reader)
        {
            Customer cus = new Customer();
            cus.CustomerId = reader.GetInt32("customerid");
            cus.Customer_Name = reader.GetString("customername");
            cus.Address = reader.GetString("address");
            cus.Id_Card = reader.GetString("idcard");
            cus.Phone_Number = reader.GetString("phonenumber");
            cus.Email = reader.GetString("email");
            cus.Gender = reader.GetString("gender");

            return cus;
        }

        public int CreateCustomer(Customer cus)
        {
            int rt = 0;
            try
            {
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"insert into customer(customername, address, idcard, phonenumber, email, gender) values
        (@CName, @CAddress, @CIDCard, @CPhone, @CEmail, @CGender);";
                command.Parameters.AddWithValue("@CName", cus.Customer_Name);
                command.Parameters.AddWithValue("@CAddress", cus.Address);
                command.Parameters.AddWithValue("@CIDCard", cus.Id_Card);
                command.Parameters.AddWithValue("@CPhone", cus.Phone_Number);
                command.Parameters.AddWithValue("@CEmail", cus.Email);
                command.Parameters.AddWithValue("@CGender", cus.Gender);
                command.ExecuteNonQuery();
                command.CommandText = @"select * from customer where idcard = @CIDCard;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rt = reader.GetInt32("customerid");
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
            return rt;
        }

        public Customer GetCustomerByIdcard(string Id_Card)
        {
            Customer customer = null;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"select * from customer where idcard=" + Id_Card + ";";

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = GetCustomerInfo(reader);
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
            return customer;
        }


        


        public List<Customer> GetListCustomers()
        {
            List<Customer> listCus = new List<Customer>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select * from customer";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer cus = new Customer();
                        cus.CustomerId = reader.GetInt32("customerid");
                        cus.Customer_Name = reader.GetString("customername");
                        cus.Address = reader.GetString("address");
                        cus.Id_Card = reader.GetString("idcard");
                        cus.Phone_Number = reader.GetString("phonenumber");
                        cus.Email = reader.GetString("email");
                        cus.Gender = reader.GetString("gender");
                        listCus.Add(cus);
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
            return listCus;
        }
        // public int AddCustomer(Customer cus)
        // {
        //     int result = null;
        //     if (connection.State == System.Data.ConnectionState.Closed)
        //     {
        //         connection.Open();
        //     }
        //     MySqlCommand cmd = new MySqlCommand("sp_createCustomer", connection);
        //     try
        //     {
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.Add("@Customer_Name", MySqlDbType.string);
        //         cmd.Parameters["@Customer_Name"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.Parameters.Add("@Customer_Address", MySqlDbType.string);
        //         cmd.Parameters["@Customer_Address"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.Parameters.Add("@Id_card", MySqlDbType.string);
        //         cmd.Parameters["@Id_card"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.Parameters.Add("@Phone_Number", MySqlDbType.string);
        //         cmd.Parameters["@Phone_Number"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.Parameters.Add("@E_mail", MySqlDbType.string);
        //         cmd.Parameters["@E_mail"].Direction = System.Data.ParameterDirection.Output;


        //         cmd.Parameters.Add("@Gen_Der", MySqlDbType.string);
        //         cmd.Parameters["@Gen_Der"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.Parameters.AddWithValue("@CustomerId", MySqlDbType.Int32);
        //         cmd.Parameters["@CustomerId"].Direction = System.Data.ParameterDirection.Output;

        //         cmd.ExecuteNonQuery();
        //         result = (int)cmd.Parameters["@CustomerId"].Value;
        //     }
        //     catch { }
        //     finally
        //     {
        //         connection.Close();
        //     }
        //     return result;
        //     }
        // }
    }
}