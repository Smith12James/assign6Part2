using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsAddPassenger
    {
        /// <summary>
        /// initialize sql class
        /// </summary>
        clsSQL clsSQL = new clsSQL();

        /// <summary>
        /// initialize clsDataAccess to query database
        /// </summary>
        clsDataAccess clsDB = new clsDataAccess();

        /// <summary>
        /// method is called to add passenger to db
        /// </summary>
        public void AddToDB(string sFirstName, string sLastName, int iFlightID)
        {
            if (sFirstName.Length < 2 || sLastName.Length < 2)
            {
                throw new Exception("First and Last name must be at least 2 characters long");

            }
            else
            {
                clsDB.ExecuteNonQuery(clsSQL.insertIntoPassenger(sFirstName, sLastName));

                int i = 0;
                DataSet ds = clsDB.ExecuteSQLStatement(clsSQL.selectPassengerID(sFirstName, sLastName), ref i);

                int iNewPassengerID = 0;

                foreach (DataRow dr in ds.Tables[0].Rows) 
                {
                    iNewPassengerID = (int)dr["Passenger_ID"];

                }

                if (iNewPassengerID == 0)
                {
                    throw new Exception("Error creating/locating new passenger");

                }
                else
                {
                    clsDB.ExecuteNonQuery(clsSQL.insertIntoLinkTable(iFlightID, iNewPassengerID));

                }
                
            }

        }

    }
}
