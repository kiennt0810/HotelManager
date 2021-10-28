using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class UserDal
    {
        // public static int USER_ERROR = -1;
        // public static int ACCOUNT_WRONG = 0;
        // public static int ACCOUNT_EXIST = 1;
        // 0: account wrong
        // 1: ok
        // -1: can't connect to db or error
        private MySqlConnection connection = DbHelper.GetConnection();
        public User Login(string userName, string password){
            User user = null;
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select user_Id, user_account, user_name, user_address, user_role from Staff where user_account=@userName and user_pass=@userPass and is_active=1;";
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@userPass", Md5Algorithms.CreateMD5(password));
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    user = new User();
                    user.UserId = reader.GetString("user_id");
                    user.UserAccount = GetString(reader, 1);
                    user.UserName = GetString(reader, 2);
                    user.UserAddress = GetString(reader,3);
                    user.Role = reader.GetInt32("user_role");
                    user.IsActive = true;
                }
                reader.Close();
                connection.Close();
            }catch(Exception ex){
                Console.WriteLine(ex);
            }
            return user;
        }
        private string GetString(MySqlDataReader reader, int columnNo){
            if(reader.IsDBNull(columnNo)){
                return ""; 
            }else{
                return reader.GetString(columnNo);
            }
        }
    }
}
