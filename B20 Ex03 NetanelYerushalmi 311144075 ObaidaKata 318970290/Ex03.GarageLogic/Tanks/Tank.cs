using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.Tanks
{
    public abstract class Tank
    {
        protected float m_Capacity;
        protected float m_RemainingEnergy;

        public float RemainingEnergy
        {
            get
            {
                return m_RemainingEnergy;
            }

            set
            {
                m_RemainingEnergy = value;
            }
        }
        
        public float Capacity
        {
            get
            {
                return m_Capacity;
            }

            set
            {
                m_Capacity = value;
            }
        }
    }
}