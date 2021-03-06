﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace login_register
{
    class machine
    {
 
        public static int checkUser(string id, string password, SQLiteConnection con)
        {
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"SELECT * FROM users WHERE ids = @id and password = @password";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                var result = cmd.ExecuteReader();


                if (result.HasRows)
                {
                    return 1;
                }
            }
            catch
            {

            }
            return 0;
        }
        public static void start()
        {


            var con = new SQLiteConnection("Data Source= database.db; Version = 3;");
            con.Open();
            string key = null;

            while (key != "q")
            {
                Console.WriteLine("what do you want to do ? :  \n 1- login \n 2- register \n q- quit \n");
                key = Console.ReadLine();
                if (key == "2")
                {
                    Console.WriteLine("Enter your id:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();
                    Console.Clear();
                    User new_user = new User();
                    new_user.id = id;
                    new_user.password = password;
                    new_user.write(con);
                }
                else if (key == "1")
                {
                   
                    Console.WriteLine("Enter your id:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();
                    Console.Clear();

                    int login = checkUser(id, password, con);
                    if (login == 0)
                    {
                        Console.WriteLine("log in failed\n");
                    }
                    else if (login == 1) {
                        var user = new User();
                        user.id = id;
                        user.password = password;
                        string key2 = null;
                        Console.WriteLine("log in succes\n");
                        while (key2 != "3")
                        {
                            if (!user.isAdmin(con))
                            {
                                
                                Console.WriteLine("What do you want to do ?\n 1 - open a ticket\n 2 - dispay your tickets\n 3 - Exit");
                                key2 = Console.ReadLine();
                                Console.Clear();

                                if (key2 == "1")
                                {
                                    Console.WriteLine("Write your message : \n");
                                    string message = Console.ReadLine();
                                    Console.Clear();

                                    user.openTicket(message, con);
                                }
                                else if (key2 == "2")
                                {
                                    user.showMyTickets(con);

                                }
                            }
                            else
                            {
                                Console.WriteLine("What do you want to do ?\n 1 - see all tickets\n 2 - close a ticket\n 3 - Exit");
                                key2 = Console.ReadLine();
                                Console.Clear();
                                if (key2 == "1")
                                {
                                    user.showAllTickets(con);
                                }
                                else if (key2 == "2")
                                {
                                    user.CloseATicket(con);
                                    Console.Clear();



                                }
                            }
                        }
                    }

                }

            }
        }
    }
}
