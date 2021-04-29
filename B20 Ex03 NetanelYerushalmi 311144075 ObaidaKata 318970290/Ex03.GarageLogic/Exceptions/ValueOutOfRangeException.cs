using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        private float? m_MaxValue;
        private float? m_MinValue;
        private string m_Message;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_PrarameterName)
        {
            m_Message = string.Format("{0} should beBetween {1}-{2}", i_PrarameterName, i_MinValue, i_MaxValue);
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;  
        }

        public ValueOutOfRangeException(float i_MinValue, string i_PrarameterName)
        {
            m_MinValue = i_MinValue;
            m_Message = string.Format("{0} should be grater then {1}", i_PrarameterName, i_MinValue);
        }

        public override string Message
        {
            get
            {
                return m_Message;
            }
        }
    }
}
