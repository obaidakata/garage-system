using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Tanks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInfoInGarage> r_VehiclesInfo;
        
        public Garage()
        {
            r_VehiclesInfo = new Dictionary<string, VehicleInfoInGarage>();
        }

        public void AddNewCar(Vehicle i_Vehicle, Owner i_Owner)
        {
            string licenseNumber = i_Vehicle.LicenseNumber;
            bool isExists = r_VehiclesInfo.ContainsKey(licenseNumber);
            if (string.IsNullOrEmpty(licenseNumber))
            {
                throw new FormatException("Empty input");
            }
            else if(r_VehiclesInfo.ContainsKey(licenseNumber))
            { 
                ChangeVeichleStatus(licenseNumber, eVehicleStatus.InRepair);
            }
            else
            {
                VehicleInfoInGarage newVehicle = new VehicleInfoInGarage(i_Owner, i_Vehicle);
                r_VehiclesInfo.Add(licenseNumber, newVehicle);
            }
        }

        public List<string> GetLicenseNumbers()
        {
            List<string> allLicenseNumbers = new List<string>();
            allLicenseNumbers.AddRange(GetLicenseNumbers(eVehicleStatus.Fixed));
            allLicenseNumbers.AddRange(GetLicenseNumbers(eVehicleStatus.InRepair));
            allLicenseNumbers.AddRange(GetLicenseNumbers(eVehicleStatus.Paid));
            return allLicenseNumbers;
        }

        public List<string> GetLicenseNumbers(eVehicleStatus i_StatusToSort)
        {
            List<string> vehicleByStatus = new List<string>();
            foreach (VehicleInfoInGarage VehicleInfo in r_VehiclesInfo.Values)
            {
                if (VehicleInfo.Status == i_StatusToSort)
                {
                    vehicleByStatus.Add(VehicleInfo.Vehicle.LicenseNumber);
                }
            }

            return vehicleByStatus;
        }

        public void ChangeVeichleStatus(string i_LisenceNumber, eVehicleStatus i_NewStatus)
        {
            if (string.IsNullOrEmpty(i_LisenceNumber))
            {
                throw new FormatException("Empty input");
            }
            else if(!r_VehiclesInfo.ContainsKey(i_LisenceNumber))
            {
                throw new LicenseNumberNotFoundException(i_LisenceNumber);
            }
            else 
            {
                VehicleInfoInGarage vehicleData = r_VehiclesInfo[i_LisenceNumber];
                vehicleData.Status = i_NewStatus;
                r_VehiclesInfo[i_LisenceNumber] = vehicleData;
            }
        }

        public void InflateWheelsToMax(string i_LisenceNumber)
        {
            if (string.IsNullOrEmpty(i_LisenceNumber))
            {
                throw new FormatException("Empty input");
            }
            else if (!r_VehiclesInfo.ContainsKey(i_LisenceNumber))
            {
                throw new LicenseNumberNotFoundException(i_LisenceNumber);
            }
            else
            {
                VehicleInfoInGarage vehicleInGarage = r_VehiclesInfo[i_LisenceNumber];
                foreach (Wheel wheel in vehicleInGarage.Vehicle.Wheels)
                {
                    wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
            }
        }

        public bool Refuel(string i_LisenceNumber, eGasType i_GasTypeToAdd, float i_AmountOfGasToAdd)
        {
            bool vehicleCanBeReFuel = false;

            if (string.IsNullOrEmpty(i_LisenceNumber))
            {
                throw new FormatException("Empty input");
            }
            else if (!r_VehiclesInfo.ContainsKey(i_LisenceNumber))
            {
                throw new LicenseNumberNotFoundException(i_LisenceNumber);
            }
            else
            {
                Tank tank = r_VehiclesInfo[i_LisenceNumber].Vehicle.VehicleTank;
                if (tank is GasTank)
                {
                    vehicleCanBeReFuel = (tank as GasTank).Refuel(i_AmountOfGasToAdd, i_GasTypeToAdd);
                }
                else
                {
                    throw new ArgumentException("vehicle is not able to be refueld");
                }
            }

            return vehicleCanBeReFuel;
        }

        public bool Charge(string i_LisenceNumber, float i_AmountOfMinutesToAdd)
        {
            bool vehicleCanBeCharge = false;
            if (string.IsNullOrEmpty(i_LisenceNumber))
            {
                throw new FormatException("Empty input");
            }
            else if (!r_VehiclesInfo.ContainsKey(i_LisenceNumber))
            {
                throw new LicenseNumberNotFoundException(i_LisenceNumber);
            }
            else
            { 
                Vehicle vehicle = r_VehiclesInfo[i_LisenceNumber].Vehicle;
                Tank tank = vehicle.VehicleTank;
                if (tank is BatteriesTank)
                {
                    vehicleCanBeCharge = (tank as BatteriesTank).ChargeBattery(i_AmountOfMinutesToAdd);
                }
                else
                {
                    throw new ArgumentException("vehicle is not able to be charged");
                }
            }

            return vehicleCanBeCharge;
        }

        public List<string> ShowVehicleData(string i_LisenceNumber)
        {
            List<string> vehicleData = new List<string>();
            if (string.IsNullOrEmpty(i_LisenceNumber))
            {
                throw new FormatException("Empty input");
            }
            else if (!r_VehiclesInfo.ContainsKey(i_LisenceNumber))
            {
                throw new LicenseNumberNotFoundException(i_LisenceNumber);
            }
            else
            {
                VehicleInfoInGarage vehicleInfo = r_VehiclesInfo[i_LisenceNumber];
                vehicleData = vehicleInfo.GetData();
            }

            return vehicleData;
        }
    }
}