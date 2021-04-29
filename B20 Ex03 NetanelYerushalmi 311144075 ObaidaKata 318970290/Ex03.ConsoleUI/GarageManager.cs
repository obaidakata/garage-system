using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private readonly Garage r_Garage;
        private readonly VehiclesCreator r_VehiclesCreator;
        private readonly GarageUI r_ConsoleUI;

        public GarageManager()
        {
            r_Garage = new Garage();
            r_VehiclesCreator = new VehiclesCreator();
            r_ConsoleUI = new GarageUI();
        }

        public void Run()
        {
            r_ConsoleUI.ShowWelcomeScreen();
            eMenuOptions userChoice;
            while (true)
            {
                r_ConsoleUI.ShowMenu();
                userChoice = r_ConsoleUI.GetAction();
                parseAction(userChoice);
            }
        }

        private void parseAction(eMenuOptions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eMenuOptions.AddNewVehicle:
                    addNewVehicle();
                    break;
                case eMenuOptions.ShowLicenseNumbers:
                    showLicenseNumbers();
                    break;
                case eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOptions.InflateToMax:
                    inflateToMax();
                    break;
                case eMenuOptions.Refuel:
                    refuel();
                    break;
                case eMenuOptions.Charge:
                    charge();
                    break;
                case eMenuOptions.ShowVehicleData:
                    showVehicleData();
                    break;
                case eMenuOptions.Exit:
                    exit();
                    break;
            }
        }
        
        private void addNewVehicle()
        {
            r_ConsoleUI.ShowVehiclesOptions();

            eVehiclesType vehicleType;
            r_ConsoleUI.GetVehiclesType(out vehicleType);
			
			string licenseNumber;
            r_ConsoleUI.GetLicenseNumber(out licenseNumber);

            Owner owner;
            r_ConsoleUI.GetOwner(out owner);

            Vehicle newVehicle = r_VehiclesCreator.CreateNewVehicle(licenseNumber, vehicleType);
            Parameters vehicleParameters = newVehicle.VehicleParameters;
            r_ConsoleUI.GetInputsForVehicle(vehicleParameters);
            try
            {
                newVehicle.SetAdditionalVehicleData();
            }
            catch (ValueOutOfRangeException valueEx)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(valueEx.Message);
            }

            while (vehicleParameters.Count > 0)
            {
                r_ConsoleUI.GetInputsForVehicle(vehicleParameters);
                try
                {
                    newVehicle.SetAdditionalVehicleData();
                }
                catch(ValueOutOfRangeException valueEx)
                {
                    r_ConsoleUI.OneOfParametersWasWrongMassage(valueEx.Message);
                }
            }

            r_Garage.AddNewCar(newVehicle, owner);
        }

        private void showLicenseNumbers()
        {
            List<string> licenseNumbers;
            bool withFiltering = r_ConsoleUI.AskUserForFilter();
            if (withFiltering)
            {
                eVehicleStatus status = r_ConsoleUI.GetVehicleStatusType();
                licenseNumbers = r_Garage.GetLicenseNumbers(status);
            }
            else
            {
                licenseNumbers = r_Garage.GetLicenseNumbers();
            }
            
            r_ConsoleUI.PrintList(licenseNumbers);
        }

        private void changeVehicleStatus()
        {
            string licenseNumber;
            r_ConsoleUI.GetLicenseNumber(out licenseNumber);
            eVehicleStatus newStatus = r_ConsoleUI.GetVehicleStatusType();
            try
            {
                r_Garage.ChangeVeichleStatus(licenseNumber, newStatus);
                r_ConsoleUI.PrintsuccessfulCommand(eMenuOptions.ChangeVehicleStatus);
            }
            catch (FormatException formatException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(formatException.Message);
            }
            catch (LicenseNumberNotFoundException ex)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(ex.Message);
            }
            finally
            {
                r_ConsoleUI.AskUserToReturnToMenu();
            }
        }

        private void inflateToMax()
        {
            string licenseNubmer;
            r_ConsoleUI.GetLicenseNumber(out licenseNubmer);
            try
            {
                r_Garage.InflateWheelsToMax(licenseNubmer);
                r_ConsoleUI.PrintsuccessfulCommand(eMenuOptions.InflateToMax);
            }
            catch (FormatException formatException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(formatException.Message);
            }
            catch (LicenseNumberNotFoundException licenseException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(licenseException.Message);
            }
            catch(ValueOutOfRangeException valueException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(valueException.Message);
            }
            finally
            {
                r_ConsoleUI.AskUserToReturnToMenu();
            }
        }

        private void refuel()
        {
            string licenseNubmer;
            r_ConsoleUI.GetLicenseNumber(out licenseNubmer);

            eGasType GasType = r_ConsoleUI.GetGasType();

            float fuelAmountToAdd = r_ConsoleUI.GetAmountOfEnergy();
            try
            {
                r_Garage.Refuel(licenseNubmer, GasType, fuelAmountToAdd);
                r_ConsoleUI.PrintsuccessfulCommand(eMenuOptions.Refuel);
            }
            catch (FormatException formatException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(formatException.Message);
            }
            catch (LicenseNumberNotFoundException licenseException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(licenseException.Message);
            }
            catch (ArgumentException logicException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(logicException.Message);
            }
            finally
            {
                r_ConsoleUI.AskUserToReturnToMenu();
            }
        }

        private void charge()
        {
            string licenseNubmer;
            r_ConsoleUI.GetLicenseNumber(out licenseNubmer);
            float minutesToAdd = r_ConsoleUI.GetAmountOfEnergy();
            try
            {
                r_Garage.Charge(licenseNubmer, minutesToAdd);
                r_ConsoleUI.PrintsuccessfulCommand(eMenuOptions.Charge);
            }
            catch (FormatException formatException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(formatException.Message);
            }
            catch (LicenseNumberNotFoundException licenseException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(licenseException.Message);
            }
            catch (ArgumentException logicException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(logicException.Message);
            }
            finally
            {
                r_ConsoleUI.AskUserToReturnToMenu();
            }
        }

        private void showVehicleData()
        {
            string licenseNumber;
            r_ConsoleUI.GetLicenseNumber(out licenseNumber);
            try
            {
                List<string> vehicleData = r_Garage.ShowVehicleData(licenseNumber);
                r_ConsoleUI.PrintVehicleData(vehicleData);
            }
            catch (FormatException formatException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(formatException.Message);
            }
            catch (LicenseNumberNotFoundException licenseException)
            {
                r_ConsoleUI.OneOfParametersWasWrongMassage(licenseException.Message);
            }
            finally
            {
                r_ConsoleUI.AskUserToReturnToMenu();
            }
        }

        private void exit()
        {
            Environment.Exit(0);
        }
    }
}
