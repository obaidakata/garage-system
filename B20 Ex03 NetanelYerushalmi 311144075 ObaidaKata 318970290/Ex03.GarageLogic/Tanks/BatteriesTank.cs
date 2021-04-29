using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Tanks
{
    public class BatteriesTank : Tank
    {
        public bool ChargeBattery(float i_HoursToAdd)
        {
            bool isChargingSucceeded = m_RemainingEnergy + i_HoursToAdd <= m_Capacity;
            if (isChargingSucceeded)
            {
                m_RemainingEnergy += i_HoursToAdd;
            }
            else
            {
                throw new ArgumentException("Number of hours to charge is exceeded from battey limit");
            }

            return isChargingSucceeded;
        }
    }
}
