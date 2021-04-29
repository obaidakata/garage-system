using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal class VehicleInfoInGarage
    {
        private readonly Vehicle r_Vehicle;
        private Owner m_Owner;
        private eVehicleStatus m_Status;
        
        public VehicleInfoInGarage(Owner i_Owner, Vehicle i_Vehicle)
        {
            m_Owner = i_Owner;
            r_Vehicle = i_Vehicle;
            m_Status = eVehicleStatus.InRepair;
        }

        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public string CarLicenseNumber
        {
            get
            {
                return r_Vehicle.LicenseNumber;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public List<string> GetData()
        {
            List<string> vehicleData = new List<string>();
            vehicleData.Add(string.Format("License number: {0}", r_Vehicle.LicenseNumber));
            vehicleData.Add(string.Format("Car model: {0}", r_Vehicle.Model));
            vehicleData.Add(string.Format("Owner name: {0}, owner phone number: {1}", m_Owner.Name, m_Owner.PhoneNumber));
            vehicleData.Add(string.Format("Car status: {0}", Enum.GetName(typeof(eVehicleStatus), m_Status)));
            vehicleData.Add(string.Format("Wheels status: {0}{1}", Environment.NewLine, r_Vehicle.GetWheelsInfo()));
            vehicleData.Add(string.Format("type of tank: {0}", r_Vehicle.NameOfTank));
            vehicleData.Add(string.Format(r_Vehicle.ToString()));

            return vehicleData;
        }
    }
}
