using Assignment6AirlineReservation.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsPassengerManager
    {
        #region Variables
        /// <summary>
        /// instantiate clsFlight class
        /// </summary>
        public List<clsPassengers> passengers = new List<clsPassengers>();

        /// <summary>
        /// instantiate FlightSQL to get string to use in dbConn
        /// </summary>
        private clsSQL clsSQL = new clsSQL();

        /// <summary>
        /// instantiate DBConn class to query data using SQL statements from FlightSQL
        /// </summary>
        private clsDataAccess clsDataAcc = new clsDataAccess();

        #endregion

        /// <summary>
        /// get all passengers and return as a list using clsPassenger type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsPassengers> GetPassengers(int iFlightID)
        {
            try
            {
                DataSet dsPassengers = new DataSet();
                int i = 0;

                dsPassengers = clsDataAcc.ExecuteSQLStatement(clsSQL.selectPassengerInfo(iFlightID), ref i);

                foreach (DataRow dr in dsPassengers.Tables[0].Rows)
                {
                    passengers.Add(new clsPassengers { iPassengerID = (int)dr["Passenger_ID"], sFirstName = (string)dr["First_Name"], sLastName = (string)dr["Last_Name"], sSeatNumber = (string)dr["Seat_Number"]});

                }

                return passengers;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }
}
