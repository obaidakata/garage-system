namespace GarageLogic
{
    public struct VehicleOwner
    {
        private string m_OwnerName;
        private string m_PhoneNumber;

        public VehicleOwner(string i_OwnerName, string i_PhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_PhoneNumber = i_PhoneNumber;
        }

        public string Name
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
        }
    }
}