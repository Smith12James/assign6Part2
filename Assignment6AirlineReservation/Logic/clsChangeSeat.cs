using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsChangeSeat
    {
        /// <summary>
        /// initialize database connection
        /// </summary>
        clsDataAccess clsDB = new clsDataAccess();

        /// <summary>
        /// initialize sql class
        /// </summary>
        clsSQL clsSQL = new clsSQL();

        /// <summary>
        /// query database to change seat number, requires flight ID, passenger ID, and new seat number
        /// </summary>
        /// <param name="iFlightID"></param>
        /// <param name="iPassengerID"></param>
        /// <param name="sSeatNumber"></param>
        public void changeSeat(int iFlightID, int iPassengerID, string sSeatNumber)
        {
            clsDB.ExecuteNonQuery(clsSQL.updateSeatNumber(sSeatNumber, iFlightID, iPassengerID));

        }

    }
}
