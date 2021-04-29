using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic
{
    public partial class Parameters
    {
        private Dictionary<string, Parameter> m_Prameters;

        public object this[string parameterName]
        {
            get
            {
                bool Exist = m_Prameters.ContainsKey(parameterName);

                if (Exist && m_Prameters[parameterName].IsParsed)
                {
                    object toDelete = m_Prameters[parameterName].Value;
                    m_Prameters.Remove(parameterName);
                    return toDelete;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_Prameters.Count, parameterName);
                }
            }
        }

        public Dictionary<string, Parameter>.ValueCollection GetValues()
        {
            return m_Prameters.Values;
        }
        
        public int Count
        {
            get
            {
                return m_Prameters.Count;
            }
        }

        public Parameters()
        {
            m_Prameters = new Dictionary<string, Parameter>();
        }

        public void Add(string i_ParameterName, string i_NameForUser, Type i_Type)
        {
            if (!m_Prameters.ContainsKey(i_ParameterName))
            {
                Parameter parameter = new Parameter(i_Type, i_NameForUser);
                m_Prameters.Add(i_ParameterName, parameter);
            }
            else
            {
                throw new ArgumentException("Cant add towice");
            }
        }
    }
}
