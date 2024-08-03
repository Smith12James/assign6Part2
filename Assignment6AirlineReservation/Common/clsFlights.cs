using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Common
{
    internal class clsFlights
    {
        #region Attribules
        /// <summary>
        /// get and set flight id
        /// </summary>
        public int iFlightID { get; set; }

        /// <summary>
        /// get and set flight number
        /// </summary>
        public string sFlightNumber { get; set; }

        /// <summary>
        /// get and set aircraft type
        /// </summary>
        public string sAircraftType { get; set; }
        #endregion

        public override string ToString()
        {
            return $"{sFlightNumber} - {sAircraftType}";

        }

    }
}
