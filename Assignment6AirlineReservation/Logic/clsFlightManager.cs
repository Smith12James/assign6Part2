using Assignment6AirlineReservation.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsFlightManager
    {
        #region Variables
        /// <summary>
        /// instantiate clsFlight class
        /// </summary>
        public List<clsFlights> flights = new List<clsFlights>();

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
        /// get all flight info and add to a List using clsFlight as the type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsFlights> GetFlights()
        {
            try
            {
                DataSet dsFlights = new DataSet();

                int i = 0;

                dsFlights = clsDataAcc.ExecuteSQLStatement(clsSQL.selectFlightInfo(), ref i);

                foreach (DataRow dr in dsFlights.Tables[0].Rows)
                {
                    flights.Add(new clsFlights { iFlightID = (int)dr["Flight_ID"], sFlightNumber = (string)dr["Flight_Number"], sAircraftType = (string)dr["Aircraft_Type"] });

                }

                return flights;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }
}
