using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Tanks;

namespace Ex03.GarageLogic
{
    public class VehiclesCreator
    {
        public Vehicle CreateNewVehicle(string i_LicenseNumber, eVehiclesType i_VehicleType)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case eVehiclesType.GasCar:
                    {
                        vehicle = new Car(i_LicenseNumber, new GasTank());
                        break;
                    }

                case eVehiclesType.ElectricCar:
                    {
                        vehicle = new Car(i_LicenseNumber, new BatteriesTank());
                        break;
                    }

                case eVehiclesType.GasMotorcycle:
                    {
                        vehicle = new Motorcycle(i_LicenseNumber, new GasTank());
                        break;
                    }

                case eVehiclesType.ElectricMotorcycle:
                    {
                        vehicle = new Motorcycle(i_LicenseNumber, new BatteriesTank());
                        break;
                    }

                case eVehiclesType.Truck:
                    {
                        vehicle = new Truck(i_LicenseNumber, new GasTank());
                        break;
                    }

                default:
                    {
                        vehicle = null;
                        break;
                    }
            }

            return vehicle;
        }
    }
}
