﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private const string db = "Tournaments";
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                var p = new DynamicParameters();

                //How to write information to the database: 
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Execute() going to call something probably an INSERT UPDATE or DELETE not passing any info back
                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                // how to capture variables that have been marked as output variables (using dapper): 
                model.Id = p.Get<int>("@id");

                return model;
            }
        }
        /// <summary>
        /// Saves a new prizse to the database.
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier.</returns>
        /// 
        // This code will execute this stored procedure dbo.spPrizes_Insert passing in all of the Added information through p
        // Then get back information (@id)
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Connection to the database
            //a using statement is a safe way to connect to a database. No matter what, after the closing } the connection is closed.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                // dapper
                var p = new DynamicParameters();
                //How to write information to the database: 
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Execute() going to call something probably an INSERT UPDATE or DELETE not passing any info back
                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                // how to capture variables that have been marked as output variables (using dapper): 
                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
