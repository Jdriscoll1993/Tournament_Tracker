using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        //only method inside this global config can change the value of connections, but everyone can read the value of connections.
        //we have the option of saving to both text files and the database. So the List<IDataConnection> allows for one or more data sources to save to and pull from.
        //instantiate a new List<IdataConnection> right from the method.
        //this Connections List holds anything that implements the IDataConnection interface - In this case, SqlConnector and TextConnector
        //public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static IDataConnection Connection { get; private set; }

        //INITIALIZE CONNECTIONS HERE
        //call at beginning of application -- here are the connections I want you to set up
        public static void InitializeConnections(DatabaseType db)
        {

            if (db == DatabaseType.Sql)
            {
                //TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseType.TextFile)
            {
                //TODO - Create the Text Connection
                TextConnector txt = new TextConnector();
                Connection = txt;
            }
        }

        public static string ConnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
