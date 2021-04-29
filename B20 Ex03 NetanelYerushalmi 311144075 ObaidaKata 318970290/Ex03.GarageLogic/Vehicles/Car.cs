using System;
using System.Collections.Generic;
using GarageLogic.Enums;
using GarageLogic.Tanks;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_MaxWeheelAirPressure = 32;
        private const int k_NumberOfWheels = 4;
        private const float k_BatteryCapactity = 2.1f;
        private const float k_MaxAmountOfGas = 60;
        private const eGasType k_GasType = eGasType.Octan96;
        private eColor? m_Color;
        private eNumberOfDoors? m_NumberOfDoors;

        public Car(string i_LicenseNumber, Tank i_Tank) : base(i_LicenseNumber, i_Tank)
        {
            SetWheels(k_NumberOfWheels, k_MaxWeheelAirPressure);
            if (m_Tank is GasTank)
            {
                (m_Tank as GasTank).GasType = k_GasType;
                (m_Tank as GasTank).Capacity = k_MaxAmountOfGas;
            }
            else
            {
                (m_Tank as BatteriesTank).Capacity = k_BatteryCapactity;
            }

            m_Parameters.Add("m_Color", "car color", typeof(eColor));

            m_Parameters.Add("m_NumberOfDoors", "number of doors", typeof(eNumberOfDoors));
        }

        public override void SetAdditionalVehicleData()
        {
            base.SetAdditionalVehicleData();
            if (!m_Color.HasValue)
            {
                try
                {
                    m_Color = (eColor)m_Parameters["m_Color"];
                }
                catch
                {
                    throw new FormatException("m_Color should be eColor");
                }
            }

            if (!m_NumberOfDoors.HasValue)
            {
                try
                {
                    m_NumberOfDoors = (eNumberOfDoors)m_Parameters["m_NumberOfDoors"];
                }
                catch
                {
                    throw new FormatException("m_NumberOfDoors should be eNumberOfDoors");
                }
            }
        }

        public override string ToString()
        {
            string toPrint;
            toPrint = string.Format("color: {0}, number of doors: {1}", m_Color.ToString(), m_NumberOfDoors);

            return toPrint;
        }
    }
}
