using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace login_register
{
    class database
    {
       
        public database()
        {
            var con = new SQLiteConnection("Data Source= database.db; Version = 3;");

            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS users (number INTEGER PRIMARY KEY AUTOINCREMENT, ids TEXT, password TEXT,authority TEXT)";

            cmd.ExecuteNonQuery();

        

            // TICKET BLOCK
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS tickets (
                    ticketid INTEGER PRIMARY KEY,
                    Ticket TEXT,
                    adminanswered TEXT,
                    askedUserId TEXT
                    )";

            cmd.ExecuteNonQuery();

        }
    }
}
