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
                Console.WriteLine("\t+------------------------------------------------+");
                Console.WriteLine("\t|                                                |");
                Console.WriteLine("\t|            |     ___    ___   .   __           |");
                Console.WriteLine("\t|            |    |   |  |___|  |  |  |          |");
                Console.WriteLine("\t|            |___ |___|   ___|  |  |  |          |");
                Console.WriteLine("\t|                                                |");
                Console.WriteLine("\t+------------------------------------------------+");
                Console.Write("\t\t\tUser Name: ");
                string userName = Console.ReadLine();
                Console.Write("\t\t\tPassword: ");
                string password = GetPassword();
                Console.WriteLine();
                user = bl.Login(userName, password);
                if (user != null)
                {
                    Console.WriteLine("\t\t\tLogin successfully!");
                }
                else
                {
                    Console.WriteLine("\t-------------------------------------------------");
                    Console.WriteLine("\t\t\tLogin fail!");
                    Console.WriteLine("\t\t\tPlease re-enter!");
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
                        Console.WriteLine("\t+------------------------------------------------+");
                        Console.WriteLine("\t|    WellCome To                                 |");
                        Console.WriteLine("\t|         _   _       _          _               |");
                        Console.WriteLine("\t|        | |_| | ___ | |__  ____| |              |");
                        Console.WriteLine("\t|        |  _  || _ ||  _/ /___/| |__            |");
                        Console.WriteLine("\t|        |_| |_||___||_|  /__|  |____/           |");
                        Console.WriteLine("\t+------------------------------------------------+");
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
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Wrong!! Please re-enter");
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

        static private char ChoiceYOrN()
        {
        while (true)
            {
                char select = Convert.ToChar(Console.ReadLine());
                if (select == 'Y' || select == 'y')
                {
                    select = 'y';
                    return select;
                }
                else if (select == 'N' || select == 'n')
                {
                    select = 'n';
                    return select;
                }
                else
                {
                    Console.Write("\t\tWrong!! Please re-enter");
                }
            }
        }
        static private void ContinueOrNot1()
        {
            Console.Write("\t\tDo you want to continue (Y/N): ");
            char select = ChoiceYOrN();
            if (select == 'y')
            {
                return;
            }
            else
            {
                DisplaysubMenu1();
            }
        }

        static string[] mainMenu = { "\t\t1. Check In", "\t\t2. Check Out", "\t\t0. Exit" };
        static string[] subMenu1 = { "\t\t1. List of empty room", "\t\t2. View room info", "\t\t3. Check Customer ID Card", "\t\t4. New Customer ","\t\t5. View Customer info ", "\t\t6. Check In ", "\t\t0. Back to main menu " };
        static string[] subMenu2 = { "\t\t1. Enter Room Number", "\t\t0. Back to main menu " };


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
                Console.WriteLine("\t+------------------------------------------------+");
                Console.WriteLine("\t|                                                |");
                Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    _  _____     |");
                Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  | ||  __ |    |");
                Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | || | | |    |");
                Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |_||_| |_|    |");
                Console.WriteLine("\t+------------------------------------------------+");
                DisplayMenu(subMenu1);
                try
                {
                    choice = 1;
                    choice = 2;
                    choice = 3;
                    choice = 4;
                    choice = 5;
                    choice = 6;
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
                        Console.Clear();
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
                        Console.Write("\t\tEnter an Room's ID: ");
                        romId = Convert.ToInt32(Console.ReadLine());

                        rom = romBL.GetRoomById(romId);
                        if (rom != null)
                        {
                            Console.Clear();
                            Console.WriteLine("\t+------------------------------------------------+");
                            Console.WriteLine("\t|                                                |");
                            Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    _  _____     |");
                            Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  | ||  __ |    |");
                            Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | || | | |    |");
                            Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |_||_| |_|    |");
                            Console.WriteLine("\t+------------------------------------------------+");
                            Console.WriteLine("\t\tRoom Detail");
                            Console.WriteLine("\t\t+-------------------------------+");
                            Console.WriteLine("\t\t     Room Id       : " + rom.Room_Id +     "");
                            Console.WriteLine("\t\t     Room Type Info: " + rom.RoomTypeInfo+ "");
                            Console.WriteLine("\t\t     Room Status   : " + rom.Room_Status + "");
                            Console.WriteLine("\t\t+-------------------------------+");
                        }
                        else
                        {
                            Console.WriteLine("\t\tRoom Id does not exist!");
                        }
                        ContinueOrNot1();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\t+------------------------------------------------+");
                        Console.WriteLine("\t|                                                |");
                        Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    _  _____     |");
                        Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  | ||  __ |    |");
                        Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | || | | |    |");
                        Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |_||_| |_|    |");
                        Console.WriteLine("\t+------------------------------------------------+");
                        Console.WriteLine("\t\tCheck Customer ID Card");
                        List<Customer> customers = cusBL.GetListCustomers();
                        Customer cus1 = new Customer();
                        if (customers == null)
                        {
                            Console.WriteLine("Empty!!");
                        }
                        else
                        {
                            string NewId_card;
                            Console.Write("\t\tId Card: ");
                            NewId_card = Convert.ToString(Console.ReadLine());
                            Customer FindCus = FindCustomer(NewId_card, customers);
                            // tim kiem thong tin khach hang trong database
                            if ((cus1 = FindCus) != null)
                            {
                                Console.WriteLine("\t\tID Card already exist!!");
                            }
                            else
                            {
                               Console.WriteLine("\t\tNew Customer!!");
                            }
                        }
                        ContinueOrNot1();
                        break;
                    case 4:
                        Customer cus2 = new Customer();
                        Console.Clear();
                        Console.WriteLine("\t+------------------------------------------------+");
                        Console.WriteLine("\t|                                                |");
                        Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    _  _____     |");
                        Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  | ||  __ |    |");
                        Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | || | | |    |");
                        Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |_||_| |_|    |");
                        Console.WriteLine("\t+------------------------------------------------+");
                        Console.Write("\t\tCutomer Name  : ");
                        cus2.Customer_Name = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tAddress       : ");
                        cus2.Address = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tId Card       : ");
                        cus2.Id_Card = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tPhone Number  : ");
                        cus2.Phone_Number = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tEmail         : ");
                        cus2.Email = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tGender        : ");
                        cus2.Gender = Convert.ToString(Console.ReadLine());

                        if (cusBL.AddCustomer(cus2))
                        {
                            Console.WriteLine("\t\tAdd Customer Complete!");
                        }
                        else
                        {
                            Console.WriteLine("\t\tFail to add Customer");
                        }
                        ContinueOrNot1();
                        break;
                    case 5:
                        int cusId;
                        Console.Write("\t\tEnter an Customer's ID: ");
                        cusId = Convert.ToInt32(Console.ReadLine());

                        Customer cus = cusBL.GetCustomerById(cusId);
                        if (cus != null)
                        {
                            Console.Clear();
                            Console.WriteLine("\t+------------------------------------------------+");
                            Console.WriteLine("\t|                                                |");
                            Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    _  _____     |");
                            Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  | ||  __ |    |");
                            Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | || | | |    |");
                            Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |_||_| |_|    |");
                            Console.WriteLine("\t+------------------------------------------------+");
                            Console.WriteLine("\t\tCustomer Detail");
                            Console.WriteLine("\t\t+------------------------------------+");
                            Console.WriteLine("\t\t  Customer ID   : " + cus.CustomerId+"");
                            Console.WriteLine("\t\t  Customer Name : " + cus.Customer_Name+ "");
                            Console.WriteLine("\t\t  Address       : " + cus.Address+ "");
                            Console.WriteLine("\t\t  Id Card       : " + cus.Id_Card+ "");
                            Console.WriteLine("\t\t  Phone Number  : " + cus.Phone_Number+ "");
                            Console.WriteLine("\t\t  Email         : " + cus.Email+ "");
                            Console.WriteLine("\t\t  Gender        : " + cus.Gender+ "");
                            Console.WriteLine("\t\t+------------------------------------+");
                        }
                        ContinueOrNot1();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("List Of Check In");
                        List<CheckIn> listCheckIn = chkBL.GetListCheckIn();
                        var table1 = new ConsoleTable("Check In ID", "Staff ID", "Customer ID", "Room Type Name", "Check In Time", "Check Out Time", "Status");

                            foreach (CheckIn item in listCheckIn)
                            {
                                table1.AddRow(item.CheckInId, item.StaffId, item.Customer_Id, item.RTname, item.Check_In, item.Check_Out, item.Status);
                            }
                            table1.Write();
                            Console.Write("\t\tPress any key to continue...");
                            Console.ReadKey();
                            DisplaysubMenu1();
                        break;
                    case 0:
                    
                        DisplayMenu(mainMenu);
                        break;
                    default:
                    Console.WriteLine("Wrong!! Please re-enter");
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
                Console.WriteLine("\t+------------------------------------------------------+");
                Console.WriteLine("\t|                                                      |");
                Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    ___  _   _  _____  |");
                Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  |   || | | ||_   _| |");
                Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | | || |_| |  | |   |");
                Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |___||_____|  |_|   |");
                Console.WriteLine("\t+------------------------------------------------------+");
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
                        Console.Write("\t\tEnter an Room's ID: ");
                        roomId = Convert.ToInt32(Console.ReadLine());

                        chkout = chkoutBL.CheckOutbyRoomId(roomId);
                        if (chkout != null)
                        {
                            Console.Clear();
                            Console.WriteLine("\t+------------------------------------------------------+");
                            Console.WriteLine("\t|                                                      |");
                            Console.WriteLine("\t|    ___  _   _  ___  ___  _   _    ___  _   _  _____  |");
                            Console.WriteLine("\t|   |  _|| |_| || __||  _|| |_|_|  |   || | | ||_   _| |");
                            Console.WriteLine("\t|   | |_ |  _  || __|| |_ |  _|_   | | || |_| |  | |   |");
                            Console.WriteLine("\t|   |___||_| |_||___||___||_| |_|  |___||_____|  |_|   |");
                            Console.WriteLine("\t+------------------------------------------------------+");
                            Console.WriteLine("\t\t+-------------------------------+");
                            Console.WriteLine("\t\t|Room Id: " + chkout.roomID + "\t\t\t|");
                            Console.WriteLine("\t\t|Time Stay(Day): " + chkout.TimeStay + "\t\t|");
                            Console.WriteLine("\t\t|Price: " + chkout.Price + " VND\t\t|");
                            Console.WriteLine("\t\t+-------------------------------+");
                        }
                        else
                        {
                            Console.WriteLine("Room Id does not exist!");
                        }
                        Console.WriteLine("\t\tPress Enter key to back menu...");
                        Console.ReadLine();
                        DisplaysubMenu2();

                    break;
                    case 0:
                    DisplayMenu(mainMenu);
                    break;
                }

            }while (choice != 0);

        }
        static int InputChoice()
            {
                Console.Write("\t\tChoice : ");
                int choice = Convert.ToInt32(Console.ReadLine());
                return choice;
            }

    }
}
