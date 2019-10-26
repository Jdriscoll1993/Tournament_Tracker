using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        // TODO - Wire up the CreatePrize for text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            // Convert the text to List<PrizeModel>
            // Find the max ID
            // Add the new record with the new ID (max + 1)
            // Covert the prizes to list<string>
            // Save the list<string> to the text file
        }
    }
}
