using System;
using System.Collections.Generic;

namespace GarageLogic.Exceptions
{
    public class LicenseNumberNotFoundException : Exception
    {
        private readonly string r_LisenceNumber;

        public LicenseNumberNotFoundException(string i_LisenceNumber)
        {
            r_LisenceNumber = i_LisenceNumber;
        }

        public override string Message
        {
            get
            {
                string message = string.Format(
                    "The Vehicle with lisence number {0} does not exist in the garage.", r_LisenceNumber);
                return message;
            }
        }
    }
}