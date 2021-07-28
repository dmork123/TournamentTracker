using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAcess;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class GlobalConfig // which means you cant insantiate GlobalConfig 
    {
        public static IDataConnection Connection { get; private set; } 


        public static void InitializeConections(DatabaseType db)
        {
           
            if (db == DatabaseType.Sql)
            {
                // TODO: Se up Sql connector properly 
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseType.TextFile)
            {
                // TODO: Set up Text Connection properly 
                TextConnector txt = new TextConnector();
                Connection = txt;
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
