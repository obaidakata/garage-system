using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GarageLogic;
using GarageLogic.Enums;

namespace ConsoleUI
{
    public class GarageUI
    {
        private StringBuilder m_LineToPrint;
        
        public GarageUI()
        {
            m_LineToPrint = new StringBuilder();
        }

        public void ShowWelcomeScreen()
        {
            m_LineToPrint.Clear();
            m_LineToPrint.Append("Welcome to garage");
            Console.WriteLine(m_LineToPrint);
        }

        public void ShowMenu()
        {
            printEnumNames(Enum.GetNames(typeof(eMenuOptions)));
        }

        public eMenuOptions GetAction()
        {
            eMenuOptions userChoice;
            m_LineToPrint.Clear();
            m_LineToPrint.Append("Please type your choice from the menu above");
            Console.WriteLine(m_LineToPrint);
            bool isChoiceLegal = Enum.TryParse<eMenuOptions>(Console.ReadLine(), out userChoice);
            while(!isChoiceLegal)
            {
                m_LineToPrint.Clear();
                m_LineToPrint.Append("Invalid input, try again");
                Console.WriteLine(m_LineToPrint);
                isChoiceLegal = Enum.TryParse<eMenuOptions>(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }

        public void ShowVehiclesOptions()
        {
            printEnumNames(Enum.GetNames(typeof(eVehiclesType)));
        }

        private void printEnumNames(string[] i_enumNames)
        {
            Console.Clear();
            m_LineToPrint.Clear();
            int i = 1;
            foreach (string name in i_enumNames)
            {
                m_LineToPrint.AppendFormat("({0}) {1} {2}", i++, name, Environment.NewLine);
            }

            Console.Write(m_LineToPrint);
        }

        public bool GetVehiclesType(out eVehiclesType o_VehicleType)
        {
            m_LineToPrint.Clear();
            m_LineToPrint.Append("Choose on of the following options Above");
            Console.WriteLine(m_LineToPrint);
            bool isChoiceLegal = Enum.TryParse<eVehiclesType>(Console.ReadLine(), out o_VehicleType);
            while (!isChoiceLegal)
            {
                m_LineToPrint.Clear();
                m_LineToPrint.Append("Invalid input try again");
                Console.WriteLine(m_LineToPrint);
                isChoiceLegal = Enum.TryParse<eVehiclesType>(Console.ReadLine(), out o_VehicleType);
            }

            return isChoiceLegal;
        }

        public eVehicleStatus GetVehicleStatusType()
        {
            Console.Clear();
            eVehicleStatus status;
            Console.Write("please insert status: ");
            bool isChoiceLegal = Enum.TryParse<eVehicleStatus>(Console.ReadLine(), out status);
            while (!isChoiceLegal)
            {
                Console.Write("Invalid input, try again: ");
                isChoiceLegal = Enum.TryParse<eVehicleStatus>(Console.ReadLine(), out status);
            }

            return status;
        }

        public void GetOwner(out VehicleOwner i_OVehicleOwner)
        {
            Console.WriteLine("Insert vehicleOwner's name: ");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Insert vehicleOwner's phone number: ");
            string ownerPhoneNumber = Console.ReadLine();
            i_OVehicleOwner = new VehicleOwner(ownerName, ownerPhoneNumber);
            Console.Clear();
        }

        internal bool AskUserForFilter()
        {
            bool withFiltering;
            m_LineToPrint.Clear();
            m_LineToPrint.Append("To watch license numbers by filter press 2.");
            m_LineToPrint.Append(" Press any number to watch all license numbers");
            Console.WriteLine(m_LineToPrint);
            int userInput;
            bool isChoiceLegal = int.TryParse(Console.ReadLine(), out userInput);
            while(!isChoiceLegal)
            {
                Console.Write("Invalid input, try again: ");
                isChoiceLegal = int.TryParse(Console.ReadLine(), out userInput);
            }

            withFiltering = userInput == 2;

            return withFiltering;
        }

        public void GetLicenseNumber(out string o_LicenseNumber)
        {
            m_LineToPrint.Clear();
            m_LineToPrint.Append("Please type your license number");
            Console.WriteLine(m_LineToPrint);
            o_LicenseNumber = Console.ReadLine();
            while(string.IsNullOrEmpty(o_LicenseNumber))
            {
                m_LineToPrint.Clear();
                m_LineToPrint.Append("Invalid input, try again: ");
                Console.Write(m_LineToPrint);
                o_LicenseNumber = Console.ReadLine();
            }
        }

        public void PrintsuccessfulCommand(eMenuOptions i_MenuOption)
        {
            m_LineToPrint.Clear();
            m_LineToPrint.AppendFormat("The command {0} was successfuly", Enum.GetName(typeof(eMenuOptions), i_MenuOption));
            Console.WriteLine(m_LineToPrint);
        }

        public void GetInputsForVehicle(Parameters i_Parameters)
        {
            string[] options;
            foreach (Parameter param in i_Parameters.GetValues())
            {
                if(!param.IsParsed)
                 {
                    Console.WriteLine("please type {0} for the vehicle", param.Name);
                    options = param.Format;
                    if (options.Length > 0)
                    {
                        Console.WriteLine("Should be in the format");
                        foreach (string option in options)
                        {
                            Console.WriteLine("({0})", option);
                        }
                    }

                    bool isSucsses = param.TryParse(Console.ReadLine());
                    while (!isSucsses)
                    {
                        Console.WriteLine("please type {0} was wrong", param.Name);
                        isSucsses = param.TryParse(Console.ReadLine());
                    }
                }
            }
        }

        public void PrintVehicleData(List<string> vehicleData)
        {
            Console.Clear();
            PrintList(vehicleData);
            AskUserToReturnToMenu();
        }

        public void AskUserToReturnToMenu()
        {
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        public void OneOfParametersWasWrongMassage(string i_Message)
        {
            Console.WriteLine(i_Message);
            Thread.Sleep(2000);
        }

        public void PrintList(List<string> i_ListToPrint)
        {
            m_LineToPrint.Clear();
            if (i_ListToPrint.Count > 0)
            {
                foreach (string str in i_ListToPrint)
                {
                    m_LineToPrint.AppendFormat(str);
                    m_LineToPrint.AppendFormat("{0}", Environment.NewLine);
                }

                Console.WriteLine(m_LineToPrint);
            }
            else
            {
                Console.WriteLine("Sorry. No vehicle found to print");
            }

            Thread.Sleep(2000);
        }

        public float GetAmountOfEnergy()
        {
            Console.Write("Please insert amount of energy you would like to add: ");
            float energy;
            bool validInput = float.TryParse(Console.ReadLine(), out energy);
            while (!validInput)
            {
                Console.Write("Invalid input, try again: ");
                validInput = float.TryParse(Console.ReadLine(), out energy);
            }

            return energy;
        }

        public eGasType GetGasType()
        {
            m_LineToPrint.Clear();
            m_LineToPrint.AppendFormat("Please choose one of the gas types below:");
            ShowGasType();
            eGasType gasType;
            bool validInput = Enum.TryParse<eGasType>(Console.ReadLine(), out gasType);
            if (!validInput)
            {
                Console.Write("Invalid input, try again: ");
                validInput = Enum.TryParse<eGasType>(Console.ReadLine(), out gasType);
            }

            return gasType;
        }

        public void ShowGasType()
        {
            int i = 0;
            foreach (string gasType in Enum.GetNames(typeof(eGasType)))
            {
                m_LineToPrint.AppendFormat("({0}) {1} {2}", i++, gasType, Environment.NewLine);
            }

            Console.Write(m_LineToPrint);
        }
    }

    public enum eMenuOptions
    {
        AddNewVehicle = 1,
        ShowLicenseNumbers,
        ChangeVehicleStatus,
        InflateToMax,
        Refuel,
        Charge,
        ShowVehicleData,
        Exit
    }
}
