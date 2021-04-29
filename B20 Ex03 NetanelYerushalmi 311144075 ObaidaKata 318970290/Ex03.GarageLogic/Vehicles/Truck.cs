using System;
using GarageLogic.Enums;
using GarageLogic.Exceptions;
using GarageLogic.Tanks;

namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_MaxWeheelAirPressure = 28;
        private const int k_NumberOfWheels = 16;
        private const eGasType k_GasType = eGasType.Soler;
        private const float k_MaxAmountOfGas = 120f;
        private bool? m_IsConatiningDangerousMaterials;
        private float? m_LuggageCapacity;
        private float m_MinimumLuggageCapacity = 0;

        public Truck(string i_LicenseNumber, GasTank i_Tank) : base(i_LicenseNumber, i_Tank)
        {
            SetWheels(k_NumberOfWheels, k_MaxWeheelAirPressure);

            (m_Tank as GasTank).GasType = k_GasType;
            (m_Tank as GasTank).Capacity = k_MaxAmountOfGas;

            m_Parameters.Add("m_IsConatiningDangerousMaterials", "if vehicle is containing dangerous materials", typeof(bool));
            m_Parameters.Add("m_LuggageCapacity", "luggage capacity", typeof(float));
        }

        public override void SetAdditionalVehicleData()
        {
            base.SetAdditionalVehicleData();
            if (!m_IsConatiningDangerousMaterials.HasValue)
            {
                try
                {
                    m_IsConatiningDangerousMaterials = (bool)m_Parameters["m_IsConatiningDangerousMaterials"];
                }
                catch
                {
                    throw new FormatException("m_IsConatiningDangerousMaterials should be bool");
                }
            }

            if (!m_LuggageCapacity.HasValue)
            {
                try
                {
                    m_LuggageCapacity = (float)m_Parameters["m_LuggageCapacity"];
                }
                catch
                {
                    throw new FormatException("m_LuggageCapacity should be m_LuggageCapacity");
                }

                if (m_LuggageCapacity < m_MinimumLuggageCapacity)
                {
                    m_LuggageCapacity = null;
                    m_Parameters.Add("m_LuggageCapacity", "luggage capacity", typeof(float));
                    throw new ValueOutOfRangeException(m_MinimumLuggageCapacity, "luggage capacity");
                }
            }
        }

        public override string ToString()
        {
            string toPrint;
            bool hasValue = m_IsConatiningDangerousMaterials.HasValue;
            if(hasValue && m_IsConatiningDangerousMaterials.Value)
            {
                toPrint = string.Format("Truck is containing dangerous materials");
            }
            else
            {
                toPrint = string.Format("Truck isn't containing dangerous materials");
            }

            toPrint += string.Format("{0}luggage capacity: {1}", Environment.NewLine, m_LuggageCapacity);

            return toPrint; 
        }
    }
}
