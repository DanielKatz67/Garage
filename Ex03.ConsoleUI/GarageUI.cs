using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI;

public class GarageUI
{
    private bool m_IsRunning;
    private readonly Garage r_Garage;
    private readonly Dictionary<eMenuChoices, Action> r_MenuActions;
    private readonly string r_Menu;
    private const bool k_IsShowMessage = true;

    public GarageUI()
    {
        m_IsRunning = true;
        r_Garage = new Garage();
        r_MenuActions = new Dictionary<eMenuChoices, Action>
        {
            { eMenuChoices.InsertVehicle, insertVehicleToGarage },
            { eMenuChoices.ShowLicensePlates, showLicensePlates },
            { eMenuChoices.ChangeVehicleStatus, updateVehicleStatus },
            { eMenuChoices.InflateWheelsToMax, inflateWheelsToMax },
            { eMenuChoices.Refuel, refuelVehicle },
            { eMenuChoices.Charge, chargeVehicleBattery },
            { eMenuChoices.ShowVehicleFullDetails, showVehicleFullDetails },
            { eMenuChoices.Exit, () => m_IsRunning = false }
        };
        r_Menu = buildMenu();
    }
    
    public void Run()
    {
        showWelcomeMessage();
        while (m_IsRunning)
        {
            try
            {
                showMenu();
                if (tryParseEnum<eMenuChoices>(readLineWithMessage(), out eMenuChoices userChoice))
                {
                    r_MenuActions[userChoice].Invoke();
                }
                else
                {
                    Console.WriteLine("Inserted invalid choice");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        showGoodbyeMessage();
    }

    private void showGoodbyeMessage()
    {
        Console.WriteLine("Thank you for using the Ultimate Garage Manager. Goodbye!");
    }

    private bool tryParseEnum<T>(string i_Input, out T o_Result) where T : struct, Enum
    {
        o_Result = default;

        if (int.TryParse(i_Input, out int intValue) && Enum.IsDefined(typeof(T), intValue))
        {
            o_Result = (T)Enum.ToObject(typeof(T), intValue);
            return true;
        }

        if (Enum.TryParse(i_Input, true, out T enumValue) && Enum.IsDefined(typeof(T), enumValue))
        {
            o_Result = enumValue;
            return true;
        }

        return false;
    }

    private void showWelcomeMessage()
    {
        StringBuilder welcomeBuilder = new StringBuilder();
        welcomeBuilder.AppendLine("** Welcome to the Ultimate Garage Manager! **");
        welcomeBuilder.AppendLine("We are here to help you manage your vehicles efficiently and effectively.\n");
        Console.Write(welcomeBuilder);
        
    }
    
    private void showMenu()
    {
        Console.Write(r_Menu);
    }

    private string buildMenu()
    {
        StringBuilder menuBuilder = new StringBuilder();

        menuBuilder.AppendLine($"Main Menu:");
        menuBuilder.AppendLine("1. Enter a vehicle to the garage");
        menuBuilder.AppendLine("2. Show license plates by status");
        menuBuilder.AppendLine("3. Change vehicle status");
        menuBuilder.AppendLine("4. Inflate wheels to maximum");
        menuBuilder.AppendLine("5. Refuel");
        menuBuilder.AppendLine("6. Charge");
        menuBuilder.AppendLine("7. Show vehicle full details by license plate");
        menuBuilder.AppendLine("8. Exit");
        menuBuilder.AppendLine();
        menuBuilder.AppendLine($"Enter your choice from the following (1-{r_MenuActions.Count}):");

        return menuBuilder.ToString();
    }
    
    private void insertVehicleToGarage()
    {
        string licensePlate = readLicensePlate();
        
        if (r_Garage.IsVehicleExists(licensePlate))
        {
            Console.WriteLine("This vehicle already exists, updating status to In Repair");
            r_Garage.SetStatus(licensePlate, eVehicleStatus.InRepair);
        }
        else
        {
            Vehicle vehicle = generateVehicle(licensePlate);
            r_Garage.AssignNewVehicle(vehicle, eVehicleStatus.InRepair);
            Console.WriteLine("The vehicle now is in the Garage\n");
        }
    }

    private Vehicle generateVehicle(string i_LicensePlate)
    {
        eEnergySourceType energyType;
        eVehicleType vehicleType = readVehicleType();
        
        if (vehicleType == eVehicleType.Car || vehicleType == eVehicleType.Motorcycle)
        {
            energyType = readEnergyType();
        }
        else
        {
            energyType = eEnergySourceType.Fuel;
        }

        float remainingEnergyCapacity = readRemainingEnergyCapacity(energyType);
        
        VehicleOwner owner = new VehicleOwner();
        assignOwnerDetails(owner);
        string model = readModel();
        string wheelsManufacture = readWheelsManufacture();
        float currentAirPressure = readCurrentAirPressure();
        
        if (vehicleType == eVehicleType.Car)
        {
            eCarColor color = readCarColor();
            eCarDoorCount doorCount = readDoorsNumber();
            return new Car(i_LicensePlate, model, wheelsManufacture, currentAirPressure,
                                color, doorCount, energyType, remainingEnergyCapacity, owner);
        }

        if (vehicleType == eVehicleType.Truck)
        {
            bool isContainHazardousMaterials = readIsContainHazardousMaterials();
            float trunkCapacity = readTrunkCapacity();
            return new Truck(i_LicensePlate, model, wheelsManufacture, currentAirPressure,
                                isContainHazardousMaterials, trunkCapacity, energyType, 
                                remainingEnergyCapacity, owner);
        }

        eMotorcycleLicenseType licenseType = readLicenseType();
        int engineCapacity = readEngineCapacity();
        
        return new Motorcycle(i_LicensePlate, model, wheelsManufacture, currentAirPressure,
                                licenseType, engineCapacity, energyType, 
                                remainingEnergyCapacity, owner);
    }
    
    private int readEngineCapacity()
    {
        Console.WriteLine("Enter engine capacity:");
        
        return int.Parse(readLineWithMessage());
    }
    
    private eMotorcycleLicenseType readLicenseType()
    {
        Console.WriteLine("Enter motorcycle license type:\n" +
                                    "1. A1\n" +
                                    "2. A2\n" + 
                                    "3. AB\n" +
                                    "4. B"
        );
        
        return ParseEnum<eMotorcycleLicenseType>(readLineWithMessage(k_IsShowMessage));
    }

    private float readTrunkCapacity()
    {
        Console.WriteLine("Enter truck's trunk capacity :");
        
        return float.Parse(readLineWithMessage());
    }

    private bool readIsContainHazardousMaterials()
    {
        Console.WriteLine("Is the truck carry hazardous materials?\n" +
                                    "1.Yes\n" +
                                    "2.No" 
                                    );
        int isContainHazardous = int.Parse(readLineWithMessage(k_IsShowMessage));

        return (isContainHazardous == 1);
    }
    
    private eCarDoorCount readDoorsNumber()
    {
        Console.WriteLine("Enter doors number (2/3/4/5):");
        
        return ParseEnum<eCarDoorCount>(readLineWithMessage());
    }
    
    private eCarColor readCarColor()
    {
        Console.WriteLine("Enter the car color:\n" +
                            "1. Blue\n" +
                            "2. White\n" +
                            "3. Black\n"+
                            "4. Red"
                        );
     
        return ParseEnum<eCarColor>(readLineWithMessage(k_IsShowMessage));
    }

    private string readLineWithMessage(bool i_IsShowMessage = false)
    {
        if (i_IsShowMessage)
        {
            Console.WriteLine("\nEnter your input here:");
        }
        
        string input = Console.ReadLine();
        Console.Clear();

        return input;
    }
    
    private float readCurrentAirPressure()
    {
        Console.WriteLine("Enter wheels current air pressure:");
        float userCurrentAirPressure = float.Parse(readLineWithMessage());
        
        return userCurrentAirPressure;
    }
    
    private string readWheelsManufacture()
    {
        Console.WriteLine("Enter vehicle wheels manufacture:");
        
        return readLineWithMessage();
    }
    
    private string? readModel()
    {
        Console.WriteLine("Enter model:");
        
        return readLineWithMessage();
    }
    
    private void assignOwnerDetails(VehicleOwner i_Owner)
    {
        Console.WriteLine("Owner Details\n");
        Console.WriteLine("Enter owner name:");
        i_Owner.Name = Console.ReadLine();
        Console.WriteLine("\nEnter owner phone:");
        i_Owner.PhoneNumber = readLineWithMessage();
    }
    
    private eEnergySourceType readEnergyType()
    {
        Console.WriteLine("Choose energy source type:\n" +
                          "1.Fuel\n" +
                          "2.Electric"
        );
        
        return ParseEnum<eEnergySourceType>(readLineWithMessage(k_IsShowMessage));
    }
    
    private float readRemainingEnergyCapacity(eEnergySourceType i_EnergyType)
    {
        string energyUnits = i_EnergyType == eEnergySourceType.Electric ? "hours" : "liters";
        Console.WriteLine($"Enter the remaining {i_EnergyType.ToString().ToLower()} in {energyUnits}:");
        string remainingCapacity = readLineWithMessage();
        string pattern = @"^\s*\d+(\.\d+)?\s*$";
        
        if (System.Text.RegularExpressions.Regex.IsMatch(remainingCapacity, pattern))
        {
            float result = float.Parse(remainingCapacity);
            
            if (result > 0)
            {
                return result;
            }
        }
        
        throw new FormatException("Invalid remaining energy capacity input. Please enter a valid positive float number."); //TODO
    }
    
    private T ParseEnum<T>(string i_String)
    {
        if (Enum.IsDefined(typeof(T), int.Parse(i_String)))
        {
            return (T)Enum.ToObject(typeof(T), int.Parse(i_String));
        }

        if (Enum.IsDefined(typeof(T), i_String))
        {
            return (T)Enum.Parse(typeof(T), i_String, true);
        }
        
        throw new FormatException();
    }
    
    private eVehicleType readVehicleType()
    {
        Console.WriteLine("Enter the vehicle type:\n" +
                          "1. Motorcycle\n" +
                          "2. Car\n" +
                          "3. Truck");
        
        return ParseEnum<eVehicleType>(readLineWithMessage(k_IsShowMessage));
    }

    private string readLicensePlate()
    {
        Console.WriteLine("Enter License Plate:");
        
        return readLineWithMessage();
    }
    
    private void showLicensePlates()
    {
        Console.WriteLine("Filter by status: (Choose 1-4)\n" +
                          "1. In Repair\n" +
                          "2. Repaired and waiting for payment\n" +
                          "3. Paid\n" +
                          "4. All licenses\n");
        string input = readLineWithMessage(k_IsShowMessage);
        List<string> licensePlates = getLicensePlates(input);
        
        if (licensePlates.Count > 0)
        {
            Console.WriteLine("License Plates:");
            
            foreach (string licensePlate in licensePlates)
            {
                Console.WriteLine($"- {licensePlate}");
            }
        }
        else
        {
            Console.WriteLine("No license plates found with the given status.\n");
        }
    }

    private List<string> getLicensePlates(string i_Filter)
    {
        if (i_Filter == "4")
        {
            return r_Garage.SearchLicensePlates(null);
        }

        eVehicleStatus status = ParseEnum<eVehicleStatus>(i_Filter);
        
        return r_Garage.SearchLicensePlates(status);            
    }

    private void updateVehicleStatus()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine("Update status:\n" +
                          "1. In Repair\n" +
                          "2. Repaired and waiting for payment\n" +
                          "3. Paid\n");
        eVehicleStatus newStatus = ParseEnum<eVehicleStatus>(readLineWithMessage(k_IsShowMessage));
        r_Garage.SetStatus(licensePlate, newStatus);
        Console.WriteLine($"Successfully updated status to {newStatus}\n");
    }

    private void inflateWheelsToMax()
    {
        string licensePlate = readLicensePlate();
        r_Garage.InflateWheelToMax(licensePlate);
        Console.WriteLine("Inflated wheels to max!\n");
    }

    private void refuelVehicle()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine("Enter quantity:");
        float fuelAmount = float.Parse(Console.ReadLine());
        Console.WriteLine("Enter fuel type:\n" +
                          "1. Soler\n" +
                          "2. Octan95\n" +
                          "3. Octan96\n" +
                          "4. Octan98\n");
        eFuelType fuelType = ParseEnum<eFuelType>(readLineWithMessage(k_IsShowMessage));
        r_Garage.RefuelVehicle(licensePlate, fuelType, fuelAmount);
        Console.WriteLine($"Successfully refueled by {fuelAmount} liters\n");
    }

    private void chargeVehicleBattery()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine("How many hours to charge?");
        float hoursToCharge = float.Parse(readLineWithMessage());
        r_Garage.Charge(licensePlate, hoursToCharge);
        Console.WriteLine($"Successfully charged by {hoursToCharge} hours");
    }

    private void showVehicleFullDetails()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine($"Information about vehicle {licensePlate} :\n" + 
                          $"{r_Garage.GetVehicleDetails(licensePlate)}\n");
    }
}