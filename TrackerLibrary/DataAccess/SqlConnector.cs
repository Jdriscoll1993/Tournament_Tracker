using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrackerLibrary.Models;

//@PlaceNumber int,
//@PlaceName nvarchar(50),
//@PrizeAmount money,
//@PrizePercentage float,
//@id int = 0 output

namespace TrackerLibrary.DataAccess
{
    class SqlConnector : IDataConnection
    {
        // TODO - Make the CreatePrize method actually save to the database
        /// <summary>
        /// Saves a new prizse to the database.
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier.</returns>
        /// 
        // This code will execute this stored procedure dbo.spPrizes_Insert passing in all of the Added information through p
        // Then get back information (@id) though we havent captured where it comes into
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Connection to the database
            //a using statement is a safe way to connect to a database. No matter what, after the closing } the connection is closed.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString("Tournaments")))
            {
                // dapper
                var p = new DynamicParameters();

                //creating a list of the model parameters and adding them all to p.
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Execute() going to call something probably an INSERT UPDATE or DELETE not passing any info back
                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                // look at the dynamic parameter list called p and find "@id" - give me thie value of it and its of type int so can put it into model.Id
                model.Id = p.Get<int>("@id");

                return model;

                // ERROR HANDLING what happens if things go wrong? what if Tournaments doesnt exist? what if it was Tournament? How is it going to blow up?
            }
        }
    }
}
