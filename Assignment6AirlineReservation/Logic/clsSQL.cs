using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Logic
{
    internal class clsSQL
    {

        /// <summary>
        /// select flight ID, flight number, and aircraft type from flight
        /// </summary>
        /// <returns></returns>
        public string selectFlightInfo()
        {
            return "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT;";

        }

        /// <summary>
        /// select passenger info using specific flight ID
        /// </summary>
        /// <param name="iFlightID"></param>
        /// <returns></returns>
        public string selectPassengerInfo(int iFlightID)
        {
            return $"SELECT PASSENGER.Passenger_ID, PASSENGER.First_Name, PASSENGER.Last_Name, FLIGHT.Flight_ID, FLIGHT_PASSENGER_LINK.Seat_Number FROM (FLIGHT INNER JOIN FLIGHT_PASSENGER_LINK ON FLIGHT.Flight_ID=FLIGHT_PASSENGER_LINK.FLIGHT_ID) INNER JOIN PASSENGER ON PASSENGER.Passenger_ID=FLIGHT_PASSENGER_LINK.Passenger_ID WHERE FLIGHT_PASSENGER_LINK.Flight_ID = {iFlightID};";

        }

        /// <summary>
        /// insert into passenger db, requires only first name and last name
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <returns></returns>
        public string insertIntoPassenger(string sFirstName, string sLastName)
        {
            return $"INSERT INTO PASSENGER (First_Name, Last_Name) VALUES ('{sFirstName}', '{sLastName}');";

        }

        /// <summary>
        /// select passenger ID searching by first and last name
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <returns></returns>
        public string selectPassengerID(string sFirstName, string sLastName)
        {
            return $"SELECT Passenger_ID from Passenger where First_Name = '{sFirstName}' AND Last_Name = '{sLastName}';";

        }

        /// <summary>
        /// insert into link table, requires: flight ID and passenger ID
        /// </summary>
        /// <returns></returns>
        public string insertIntoLinkTable(int iFlightID, int iPassengerID)
        {
            return $"INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) VALUES({iFlightID}, {iPassengerID}, '0');";

        }

        /// <summary>
        /// update flight passenger link table seat number, requires seat number, flight ID, and passenger ID
        /// </summary>
        /// <param name="sSeatNumber"></param>
        /// <param name="iFlightID"></param>
        /// <param name="iPassengerID"></param>
        /// <returns></returns>
        public string updateSeatNumber(string sSeatNumber, int iFlightID, int iPassengerID)
        {
            return $"UPDATE FLIGHT_PASSENGER_LINK SET Seat_Number = '{sSeatNumber}' WHERE FLIGHT_ID = {iFlightID} AND PASSENGER_ID = {iPassengerID};";

        }

        /// <summary>
        /// delete passenger link using flight ID and passenger ID. This is only to delete the link.
        /// </summary>
        /// <param name="iFlightID"></param>
        /// <param name="iPassengerID"></param>
        /// <returns></returns>
        public string deletePassengerLink(int iFlightID, int iPassengerID)
        {
            return $"Delete FROM FLIGHT_PASSENGER_LINK WHERE FLIGHT_ID = {iFlightID} AND PASSENGER_ID = {iPassengerID};";

        }

        /// <summary>
        /// delete passenger using passenger ID. This will need to be executed after deletePassengerLink()
        /// </summary>
        /// <param name="iPassengerID"></param>
        /// <returns></returns>
        public string deletePassenger(int iPassengerID)
        {
            return $"Delete FROM PASSENGER WHERE PASSENGER_ID = {iPassengerID};";

        }

    }
}
