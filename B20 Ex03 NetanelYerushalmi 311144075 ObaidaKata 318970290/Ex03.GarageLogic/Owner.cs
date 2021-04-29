namespace GarageLogic
{
    public struct Owner
    {
        private string m_OwnerName;
        private string m_PhoneNumber;

        public Owner(string i_OwnerName, string i_PhoneNumber)
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