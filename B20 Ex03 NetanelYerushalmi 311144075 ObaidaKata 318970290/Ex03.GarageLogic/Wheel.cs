using GarageLogic.Exceptions;

namespace GarageLogic
{
    public class Wheel
    {
        private const float k_MinimumAirPressure = 0;
        private readonly float r_MaximumAirPressure;
        private float m_CurrentAirPressure;
        private string m_ManufacturerName;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaximumAirPressure = i_MaxAirPressure;
        }

        public bool Inflate(float i_AirToAdd)
        {
            bool inflatingSucceeded = i_AirToAdd + m_CurrentAirPressure <= r_MaximumAirPressure;
            if(inflatingSucceeded)
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(k_MinimumAirPressure, r_MaximumAirPressure, "current air pressure");
            }

            return inflatingSucceeded;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaximumAirPressure;
            }
        }

        public override string ToString()
        {
            string data = string.Format("Current air pressure is {0}", m_CurrentAirPressure);
            data += string.Format("{0} manufactorer name is {1}", System.Environment.NewLine, m_ManufacturerName);
            return data;
        }
    }
}
