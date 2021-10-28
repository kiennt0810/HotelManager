using System;
using BL;
using Persistance;
using ConsoleTables;
using System.Collections.Generic;

namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            UserBL bl = new UserBL();
            CustomerBL cusBL = new CustomerBL();
            User user = new User();
            RoomBL romBL = new RoomBL();
            Room rom = new Room();
            Customer cus = new Customer();
            do
            {
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.Write("User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = GetPassword();
                Console.WriteLine();
                user = bl.Login(userName, password);
                if (user != null)
                {
                    Console.WriteLine("Login successfully!");
                }
                else
                {
                    Console.WriteLine("Login fail!");
                    Console.WriteLine("Please re-enter!");
                    Console.ReadLine();
                }
            } while (user == null);
            switch (user.Role)
            {
                case 1:
                    int choice;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("-------Wellcome to Hotel-------");
                        Console.WriteLine("===============================");
                        DisplayMenu(mainMenu);
                        choice = InputChoice();

                        switch (choice)
                        {
                            case 1:
                                DisplaysubMenu1();
                                break;
                            case 2:
                                DisplaysubMenu2();
                                break;
                        }
                    } while (choice != 0);
                    break;

                case 2:
                    Console.WriteLine("Service");
                    break;
                default:
                    Console.WriteLine("default user");
                    break;
            }
        }


        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }

        public static Customer FindCustomer(string idcard, List<Customer> customers)
        {
            Customer cus = null;
            if (customers != null & customers.Count > 0)
            {
                cus = customers.Find(x => x.Id_Card == idcard);
            }

            return cus;
        }

        static string[] mainMenu = { "1. Check In", "2. Check Out", "0. Exit" };
        static string[] subMenu1 = { "1. List of empty room", "2. View room info", "3. Enter Customer info", "4. View Customer info ", "5. Check In ", "0. Back to main menu " };
        static string[] subMenu2 = { "1. Enter Room Number", "2. View Bill info", "0. Back to main menu " };


        static void DisplayMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }
        }

        static void DisplaysubMenu1()
        {
            RoomBL romBL = new RoomBL();
            Room rom = new Room();
            CustomerBL cusBL = new CustomerBL();
            User user = new User();
            CheckIn chk = new CheckIn();
            CheckInBL chkBL = new CheckInBL();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("-------Check In------- ");
                Console.WriteLine("=======================");
                DisplayMenu(subMenu1);
                try
                {
                    choice = 1;
                    choice = 2;
                    choice = 3;
                    choice = 4;
                    choice = 5;
                    choice = 0;
                }
                catch (System.Exception)
                {
                    Console.Write("Please re-choice!!");
                }
                choice = InputChoice();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("List of empty Room");
                        List<Room> listRoom = romBL.GetRooms();
                        var table = new ConsoleTable("Room ID", "Room Type", "Room Status");

                        foreach (Room item in listRoom)
                        {
                            table.AddRow(item.Room_Id, item.RoomTypeInfo, item.Room_Status);
                        }
                        table.Write();
                        Console.Write("\n     Press any key to continue...");
                        Console.ReadKey();
                        DisplaysubMenu1();
                        break;
                    case 2:
                        int romId;
                        Console.Write("Enter an Room's ID: ");
                        romId = Convert.ToInt32(Console.ReadLine());

                        rom = romBL.GetRoomById(romId);
                        if (rom != null)
                        {
                            Console.Clear();
                            Console.WriteLine("+-----------------------+");
                            Console.WriteLine("|Room Id: " + rom.Room_Id + "\t\t|");
                            Console.WriteLine("|Room Type Info: " + rom.RoomTypeInfo + "\t|");
                            Console.WriteLine("|Room Status: " + rom.Room_Status + "\t\t|");
                            Console.WriteLine("+-----------------------+");
                        }
                        else
                        {
                            Console.WriteLine("Room Id does not exist!");
                        }
                        Console.WriteLine("\n    Press Enter key to back menu...");
                        Console.ReadLine();
                        DisplaysubMenu1();
                        break;
                    case 3:
                        Console.WriteLine("Enter Customer Data: ");
                        List<Customer> customers = cusBL.GetListCustomers();
                        Customer cus1 = new Customer();
                        if (customers == null)
                        {
                            Console.WriteLine("Empty!!");
                        }
                        else
                        {
                            string NewId_card;
                            Console.Write("Id Card: ");
                            NewId_card = Convert.ToString(Console.ReadLine());
                            Customer FindCus = FindCustomer(NewId_card, customers);
                            // tim kiem thong tin khach hang trong database
                            if ((cus1 = FindCus) != null)
                            {
                                Console.WriteLine("ID Card already exist!!");
                            }
                            else
                            {
                                Console.Write("Cutomer ID: ");
                                cus1.CustomerId = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Cutomer Name: ");
                                cus1.Customer_Name = Convert.ToString(Console.ReadLine());

                                Console.Write("Address: ");
                                cus1.Address = Convert.ToString(Console.ReadLine());

                                Console.Write("Phone Number: ");
                                cus1.Phone_Number = Convert.ToString(Console.ReadLine());

                                Console.Write("Email: ");
                                cus1.Email = Convert.ToString(Console.ReadLine());

                                Console.Write("Gender: ");
                                cus1.Gender = Convert.ToString(Console.ReadLine());

                                if (cusBL.AddCustomer(cus1))
                                {
                                    Console.WriteLine("Add Customer Complete!");
                                }
                                else
                                {
                                    Console.WriteLine("Fail to add Customer");
                                }

                            }
                        }


                        Console.WriteLine("\n    Press Enter key to back menu...");
                        Console.ReadLine();
                        DisplaysubMenu1();
                        break;
                    case 4:
                        int cusId;
                        Console.Write("Enter an Customer's ID: ");
                        cusId = Convert.ToInt32(Console.ReadLine());

                        Customer cus = cusBL.GetCustomerById(cusId);
                        if (cus != null)
                        {
                            Console.WriteLine("Customer ID: " + cus.CustomerId);
                            Console.WriteLine("Customer Name: " + cus.Customer_Name);
                            Console.WriteLine("Address: " + cus.Address);
                            Console.WriteLine("Id Card: " + cus.Id_Card);
                            Console.WriteLine("Phone Number: " + cus.Phone_Number);
                            Console.WriteLine("Email: " + cus.Email);
                            Console.WriteLine("Gender: " + cus.Gender);
                        }
                        Console.WriteLine("\n    Press Enter key to back menu...");
                        Console.ReadLine();
                        DisplaysubMenu1();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("1. List Of Check In");
                        Console.WriteLine("2. Enter Check In info");
                        choice = InputChoice();
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("List Of Check In");
                                List<CheckIn> listCheckIn = chkBL.GetListCheckIn();
                                var table1 = new ConsoleTable("Check In ID", "Staff ID", "Customer ID", "Room Type Name", "Check In Time", "Check Out Time", "Status");

                                foreach (CheckIn item in listCheckIn)
                                {
                                    table1.AddRow(item.CheckInId, item.StaffId, item.Customer_Id, item.RTname, item.Check_In, item.Check_Out, item.Status);
                                }
                                table1.Write();
                                Console.Write("\n     Press any key to continue...");
                                Console.ReadKey();
                                DisplaysubMenu1();
                                break;
                            case 2:
                                Console.WriteLine("Enter Check In info");
                                Console.Write("\n     Press any key to continue...");
                                Console.ReadKey();
                                DisplaysubMenu1();
                                break;
                        }
                        break;
                    case 0:
                        DisplayMenu(mainMenu);
                        break;
                }
            } while (choice != 0);
        }

        static void DisplaysubMenu2()
        {
            RoomBL romBL = new RoomBL();
            Room rom = new Room();
            CustomerBL cusBL = new CustomerBL();
            User user = new User();
            CheckIn chk = new CheckIn();
            CheckInBL chkBL = new CheckInBL();
            CheckOut chkout = new CheckOut();
            CheckOutBL chkoutBL = new CheckOutBL();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("-------Check Out------- ");
                Console.WriteLine("=======================");
                DisplayMenu(subMenu2);
                try
                {
                    choice = 1;
                    choice = 2;
                    choice = 0;
                }
                catch (System.Exception)
                {
                    Console.Write("Please re-choice!!");
                }
                choice = InputChoice();

                switch(choice)
                {
                    case 1:
                        int roomId;
                        Console.Write("Enter an Room's ID: ");
                        roomId = Convert.ToInt32(Console.ReadLine());

                        chkout = chkoutBL.CheckOutbyRoomId(roomId);
                        if (chkout != null)
                        {
                            Console.Clear();
                            Console.WriteLine("+-------------------------------+");
                            Console.WriteLine("|Room Id: " + chkout.roomID + "\t\t\t|");
                            Console.WriteLine("|Time Stay(Day): " + chkout.TimeStay + "\t\t|");
                            Console.WriteLine("|Price: " + chkout.Price + "\t\t|");
                            Console.WriteLine("+-------------------------------+");
                        }
                        else
                        {
                            Console.WriteLine("Room Id does not exist!");
                        }
                        Console.WriteLine("\n    Press Enter key to back menu...");
                        Console.ReadLine();
                        DisplaysubMenu2();

                    break;

                    case 2:
                    break;
                    case 0:
                    DisplayMenu(mainMenu);
                    break;
                }

            }while (choice != 0);

        }
        static int InputChoice()
            {
                Console.Write("Choice : ");
                int choice = Convert.ToInt32(Console.ReadLine());
                return choice;
            }

    }
}
