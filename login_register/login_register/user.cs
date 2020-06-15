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
            cmd.CommandText = "INSERT INTO users(ids, password) VALUES(@ids, @password)";

            cmd.Parameters.AddWithValue("@ids", id);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
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
            cmd.Parameters.AddWithValue("@adminanswered", "none");
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
    }
}
