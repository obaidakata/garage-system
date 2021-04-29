using System;
using GarageLogic.Enums;

namespace GarageLogic.Tanks
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