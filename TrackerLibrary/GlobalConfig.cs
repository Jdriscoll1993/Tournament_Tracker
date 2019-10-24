using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        //only method inside this global config can change the value of connections, but everyone can read the value of connections.

        //we have the option of saving to both text files and the database. So the List<IDataConnection> allows for one or more data sources to save to and pull from.

        //instantiate a new List<IdataConnection> right from the method.
        //this Connections List holds anything that implements the IDataConnection interface - In this case, SqlConnector and TextConnection
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        //initialize connections here
        //call at beginning of application -- here are the connections I want you to set up
        public static void InitializeConnections(bool database, bool textFiles)
        {
            
            if (database)
            {
                //TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textFiles)
            {
                //TODO - Create the Text Connection
                TextConnection txt = new TextConnection();
                Connections.Add(txt);
            }
        }
    }
}
