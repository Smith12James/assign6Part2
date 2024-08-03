using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation.Common
{
    internal class clsPassengers
    {
        #region Attribules
        /// <summary>
        /// get and set passenger id
        /// </summary>
        public int iPassengerID { get; set; }

        /// <summary>
        /// get and set First Name
        /// </summary>
        public string sFirstName { get; set; }

        /// <summary>
        /// get and set Last Name
        /// </summary>
        public string sLastName { get; set; }

        /// <summary>
        /// get and set seat number
        /// </summary>
        public string sSeatNumber { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{sFirstName} {sLastName}";

        }

    }
}
