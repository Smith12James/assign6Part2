using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsDeletePassenger
    {
        /// <summary>
        /// initialize database access
        /// </summary>
        clsDataAccess dbAccess = new clsDataAccess();

        /// <summary>
        /// initialize clsSQL to get SQL query string
        /// </summary>
        clsSQL clsSQL = new clsSQL();

        /// <summary>
        /// public facing method used to delete passenger using passenger ID and flight ID.
        /// </summary>
        public void deletePassenger(int iPassengerID, int iFlightID)
        {
            try
            {
                dbAccess.ExecuteNonQuery(clsSQL.deletePassengerLink(iFlightID, iPassengerID));

                dbAccess.ExecuteNonQuery(clsSQL.deletePassenger(iPassengerID));

            }
            catch (Exception ex)
            {
                throw new Exception($"Delete Passenger: {ex.Message}");
            
            }

        }

    }
}
