using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace COMP266EyeMaxLib.Data
{
    // Internal access modifier protect our class from outside access
    internal class ConnectionBuilder
    {
        private readonly static string conString;

        static ConnectionBuilder()
        {
            //conString = "Data Source=ZEPHYR14US\\MSSQLSERVER04; Initial Catalog=COMP266_EyeMax; Integrated Security=True; Encrypt = False;";

            // 
            conString = "Persist Security Info=False;User ID=EyeMaxService;Password=2026COMP266Final;Initial Catalog=COMP266_EyeMax;Server=ZEPHYR14US\\MSSQLSERVER04;Encrypt=False;";
        }

        public static string ConnectionString()
        {
            return conString;
        }
    }
}
