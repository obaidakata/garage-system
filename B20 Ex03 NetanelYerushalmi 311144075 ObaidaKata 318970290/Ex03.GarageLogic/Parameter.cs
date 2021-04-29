using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace GarageLogic
{
    public class Parameter
    {
        private object m_Value;
        private Type m_Type;
        private string m_NameForUser;

        internal Parameter(Type i_Type, string i_NameForUser)
        {
            m_Value = null;
            m_Type = i_Type;
            m_NameForUser = i_NameForUser;
        }

        public string Name
        {
            get
            {
                return m_NameForUser;
            }
        }

        public bool TryParse(string i_UserInput)
        {
            bool isParseSucceeded = false;
            if (m_Type == typeof(int))
            {
                int valueAsInt;
                isParseSucceeded = int.TryParse(i_UserInput, out valueAsInt);
                if (isParseSucceeded)
                {
                    m_Value = valueAsInt;
                }
            }
            else if (m_Type == typeof(uint))
            {
                uint valueAsUint;
                isParseSucceeded = uint.TryParse(i_UserInput, out valueAsUint);
                if (isParseSucceeded)
                {
                    m_Value = valueAsUint;
                }
            }
            else if (m_Type == typeof(string))
            {
                isParseSucceeded = true;
                m_Value = i_UserInput;
            }
            else if (m_Type == typeof(float))
            {
                float valueAsFloat;
                isParseSucceeded = float.TryParse(i_UserInput, out valueAsFloat);
                if (isParseSucceeded)
                {
                    m_Value = valueAsFloat;
                }
            }
            else if (m_Type == typeof(double))
            {
                double valueAsDouble;
                isParseSucceeded = double.TryParse(i_UserInput, out valueAsDouble);
                if (isParseSucceeded)
                {
                    m_Value = valueAsDouble;
                }
            }
            else if (m_Type == typeof(bool))
            {
                bool valueAsBool;
                int chooseFromUser;
                isParseSucceeded = bool.TryParse(i_UserInput, out valueAsBool);
                bool isLegal = int.TryParse(i_UserInput, out chooseFromUser);
                if (isParseSucceeded)
                {
                    m_Value = valueAsBool;
                }
                else if(isLegal)
                {
                    if(chooseFromUser == 1)
                    {
                        m_Value = true;
                    }  
                    else if(chooseFromUser == 0)
                    {
                        m_Value = false;
                    }

                    isParseSucceeded = true;
                }
            }
            else if (m_Type.IsEnum)
            {
                int userInputAsInt;
                bool isInt = int.TryParse(i_UserInput, out userInputAsInt);
                if (!isInt)
                {
                    foreach (string enumName in Enum.GetNames(m_Type))
                    {
                        if (string.Equals(i_UserInput, enumName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            object enumAsobject = Enum.Parse(m_Type, enumName);
                            m_Value = enumAsobject;
                            isParseSucceeded = true;
                        }
                    }
                }
                else
                {
                    foreach (int enumValue in Enum.GetValues(m_Type))
                    {
                        if (enumValue == userInputAsInt)
                        {
                            m_Value = enumValue;
                            isParseSucceeded = true;
                        }
                    }
                }
            }

            return isParseSucceeded;
        }

        public bool IsParsed
        {
            get
            {
                return m_Value != null;
            }
        }

        internal object Value
        {
            get
            {
                return m_Value;
            }
        }

        public string[] Format
        {
            get
            {
                string[] format;
                if (m_Type.IsEnum)
                {
                    format = Enum.GetNames(m_Type);
                }
                else if(m_Type == typeof(bool))
                {
                    format = new string[2];
                    format[0] = "Yes - 1";
                    format[1] = "No - 0";
                }
                else
                {
                    format = new string[0];
                }

                return format;
            }
        }
    }
}
