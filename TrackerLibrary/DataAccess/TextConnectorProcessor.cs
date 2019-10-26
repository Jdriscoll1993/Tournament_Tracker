using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TrackerLibrary.DataAccess.TextConnector
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // PrizeModels.csv
        {
            // C:\data\Tournament_Tracker\PrizeModels.csv
            return $"{ ConfigurationManager.AppSettings["FilePath"] }\\{ fileName} ";
        }
    }
}
