using System;
using System.Text;
using GarageLogic.Enums;
using GarageLogic.Exceptions;
using GarageLogic.Tanks;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_LicenseNumber;
        protected Tank m_Tank;
        protected Wheel[] m_Wheels;
        protected Parameters m_Parameters;

        protected string m_ModelName;
        private float? m_RemainingEnergy;
        private string m_ManufatorerName;
        private float? m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Parameters VehicleParameters
        {
            get
            {
                return m_Parameters;
            }
        }

        protected Vehicle(string i_LicenseNumber, Tank i_Tank)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Tank = i_Tank;

            m_Parameters = new Parameters();
            m_Parameters.Add("m_ModelName", "model name", typeof(string));
            m_Parameters.Add("m_RemainingEnergy", "remaining energy in tank", typeof(float));

            m_Parameters.Add("m_ManufatorerName", "name of manufatorer of wheels", typeof(string));
            m_Parameters.Add("m_CurrentAirPressure", "current air pressure in wheels", typeof(float));
        }

        public virtual void SetAdditionalVehicleData()
        {
            if (string.IsNullOrEmpty(m_ModelName)) 
            {
                m_ModelName = m_Parameters["m_ModelName"].ToString();
            }

            if (!m_RemainingEnergy.HasValue) 
            {
                try
                {
                    m_RemainingEnergy = (float)m_Parameters["m_RemainingEnergy"];
                }
                catch
                {
                    throw new FormatException("m_RemainingEnergy should be float");
                }

                bool remainingEnergyGreaterThanMinimum = m_RemainingEnergy >= 0;
                bool remainingEnergyLessThanMaximum = m_RemainingEnergy <= m_Tank.Capacity;
                if (remainingEnergyGreaterThanMinimum && remainingEnergyLessThanMaximum)
                {
                    m_Tank.RemainingEnergy = m_RemainingEnergy.Value;
                }
                else
                {
                    m_RemainingEnergy = null;
                    m_Parameters.Add("m_RemainingEnergy", "remaining energy in tank", typeof(float));
                    throw new ValueOutOfRangeException(0, m_Tank.Capacity, "remaining energy in tank");
                }
            }

            if (!m_CurrentAirPressure.HasValue)
            {
                try
                {
                    m_CurrentAirPressure = (float)m_Parameters["m_CurrentAirPressure"];
                }
                catch
                {
                    throw new FormatException("m_CurrentAirPressure should be float");
                }

                bool airPressureGreaterThanMinimum = m_CurrentAirPressure >= 0;
                bool airPressureLessThanMaximum = m_CurrentAirPressure <= m_MaxAirPressure;
                if (airPressureGreaterThanMinimum && airPressureLessThanMaximum)
                {
                    foreach (Wheel wheel in m_Wheels)
                    {
                        wheel.CurrentAirPressure = m_CurrentAirPressure.Value;
                    }
                }
                else
                {
                    m_CurrentAirPressure = null;
                    m_Parameters.Add("m_CurrentAirPressure", "current air pressure in wheels", typeof(float));
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure, "current air pressure in wheels");
                }
            }

            if (string.IsNullOrEmpty(m_ManufatorerName))
            {
                m_ManufatorerName = m_Parameters["m_ManufatorerName"].ToString();
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.ManufacturerName = m_ManufatorerName;
                }
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        protected void SetWheels(int i_numberOfWheels, int i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;

            m_Wheels = new Wheel[i_numberOfWheels];
            for (int i = 0; i < i_numberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(m_MaxAirPressure);
            }
        }

        public string Model
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return (m_Tank.RemainingEnergy / m_Tank.Capacity) * 100;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public string GetWheelsInfo()
        {
            string wheelsInfo;
            if (m_Wheels != null)
            {
                StringBuilder WheelsInfo = new StringBuilder();
                int i = 0;
                foreach (Wheel wheel in m_Wheels)
                {
                    WheelsInfo.AppendFormat("Wheel number {0} {1}{2}", i++, wheel.ToString(), Environment.NewLine);
                }

                wheelsInfo = WheelsInfo.ToString();
            }
            else
            {
                wheelsInfo = string.Empty;
            }

            return wheelsInfo;
        }

        public Tank VehicleTank
        {
            get 
            { 
                return m_Tank; 
            }
        }

        public string NameOfTank
        {
            get
            {
                string name;
                if(m_Tank is BatteriesTank)
                {
                    name = string.Format("battery tank, remaining energy {0}", m_Tank.RemainingEnergy);                 
                }
                else
                {
                    name = string.Format("gas tank, type of gas: {0} remaining energy: {1}", (m_Tank as GasTank).GasTypeName, m_Tank.RemainingEnergy);
                }

                return name;
            }
        }
    }
}