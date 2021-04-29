using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic.Enums;

namespace GarageLogic.Tanks
{
    public class GasTank : Tank
    {
        private eGasType m_GasType;

        public bool Refuel(float i_AmountGasToAddToAdd, eGasType i_FuelType)
        {
            bool fuelOverflow = m_RemainingEnergy + i_AmountGasToAddToAdd > m_Capacity;
            if(fuelOverflow)
            {
                throw new ArgumentException("amount of gas to add is exceeded from maximum amount in tank");
            }

            bool appropriateFuelType = i_FuelType == m_GasType;
            bool isRefuelSucceeded = !fuelOverflow && appropriateFuelType;
            if (isRefuelSucceeded)
            {
                m_RemainingEnergy += i_AmountGasToAddToAdd;
            }

            return isRefuelSucceeded;
        }

        public eGasType GasType
        {
            get
            {
                return m_GasType;
            }

            set
            {
                m_GasType = value;
            }
        }

        public string GasTypeName
        {
            get
            {
                return m_GasType.ToString();
            }
        }
    }
}
