using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace login_register
{

    class User : database
    {
       
        public string id { get; set; }
        public string password { get; set; }

        public void write(SQLiteConnection con)
        {


            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO users(ids, password,authority) VALUES(@ids, @password,@authority)";

            cmd.Parameters.AddWithValue("@ids", id);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@authority", "none");

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public bool isAdmin(SQLiteConnection con)
        {

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"SELECT * FROM users WHERE ids = @id and password = @password and authority = @authority" ;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@authority", "none");

            var rdr = cmd.ExecuteReader();
            return !rdr.HasRows;
        }
        public void openTicket(string message, SQLiteConnection con)
        {
            using var cmd = new SQLiteCommand(con);


            cmd.CommandText = @"SELECT * FROM users WHERE ids = @id and password = @password";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@password", password);

            var reader = cmd.ExecuteScalar();

            var userid = reader.ToString();
            
            cmd.CommandText = "INSERT INTO tickets(Ticket, adminanswered, askedUserId) VALUES(@ticket, @adminanswered, @askeduser)";

            cmd.Parameters.AddWithValue("@ticket", message);
            cmd.Parameters.AddWithValue("@adminanswered", "no answer");
            cmd.Parameters.AddWithValue("@askeduser", userid);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void showMyTickets(SQLiteConnection con)
        {
            using var cmd = new SQLiteCommand(con);


            cmd.CommandText = @"SELECT * FROM users WHERE ids = @id and password = @password";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@password", password);

            var reader = cmd.ExecuteScalar();

            var userid = reader.ToString();

            cmd.CommandText = @"SELECT * FROM tickets WHERE askedUserId = @userid";
            cmd.Parameters.AddWithValue("@userid", userid);

            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetString(1)} {rdr.GetString(2)} \n");
            }
        }

            public void CloseATicket(SQLiteConnection con)
        {
            showAllTickets(con);
            using var cmd = new SQLiteCommand(con);
            Console.WriteLine("Which ticket are yoou going to close, write the id of it: ");
            var key = Console.ReadLine();

            cmd.CommandText = "UPDATE tickets SET adminanswered = @admin WHERE ticketid = @ticketid";

            cmd.Parameters.AddWithValue("@admin", id + " is answered");
            cmd.Parameters.AddWithValue("@ticketid", key);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

            public void showAllTickets(SQLiteConnection con)
            {



            string stm = "SELECT * FROM tickets";

            using var cmd = new SQLiteCommand(stm, con);

            using SQLiteDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine($"{rdr.GetName(0)} {rdr.GetName(1)}  {rdr.GetName(2)} {rdr.GetName(3)}");
            rdr.Read();
            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)} {rdr.GetString(3)}");
            }
        }
        
    }
}
