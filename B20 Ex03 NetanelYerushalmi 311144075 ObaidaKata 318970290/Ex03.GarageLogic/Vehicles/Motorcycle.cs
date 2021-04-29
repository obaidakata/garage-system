using System;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Tanks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_MinEngineVolume = 0;
        private const int k_NumberOfWheels = 2;
        private const int k_MaxWeheelAirPressure = 30;
        private const eGasType k_GasType = eGasType.Octan95;
        private const float k_MaxAmountOfGas = 7;
        private const float k_BatteryCapactity = 1.2f;
        private eLicenseType? m_LicenceType;
        private int? m_EngineVolume;

        public Motorcycle(string i_LicenseNumber, Tank i_Tank) : base(i_LicenseNumber, i_Tank)
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

            m_Parameters.Add("m_licenceType", "License type", typeof(eLicenseType));
            m_Parameters.Add("m_EngineVolume", "engine valume", typeof(int));
        }

        public override void SetAdditionalVehicleData()
        {
            base.SetAdditionalVehicleData();

            if (!m_LicenceType.HasValue)
            {
                try
                {
                    m_LicenceType = (eLicenseType)m_Parameters["m_licenceType"];
                }
                catch
                {
                    throw new FormatException("m_LicenceType should be eLicenseType");
                }
            }

            if (!m_EngineVolume.HasValue)
            {
                try
                {
                    m_EngineVolume = (int)m_Parameters["m_EngineVolume"];
                }
                catch
                {
                    throw new FormatException("m_EngineVolume should be int");
                }

                if (m_EngineVolume < k_MinEngineVolume)
                {
                    m_EngineVolume = null;
                    m_Parameters.Add("m_EngineVolume", "engine valume", typeof(int));
                    throw new ValueOutOfRangeException(k_MinEngineVolume, "engine valume");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Type of license: {0}, engine volume: {1}", m_LicenceType.ToString(), m_EngineVolume.ToString());
        }
    }
}
