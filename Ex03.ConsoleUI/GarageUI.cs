using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI;

public class GarageUI
{
    private bool m_IsRunning;
    private readonly Garage r_Garage;
    private readonly Dictionary<eMenuChoices, Action> r_MenuActions;

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
    }
    
    public void Run()
    {
        showWelcomeMessage();
        while (m_IsRunning)
        {
            try
            {
                showMenu();
                if (tryParseEnum<eMenuChoices>(Console.ReadLine(), out eMenuChoices userChoice))
                {
                    Console.Clear();
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
    }

    private bool tryParseEnum<T>(string input, out T result) where T : struct, Enum
    {
        result = default;

        if (int.TryParse(input, out int intValue) && Enum.IsDefined(typeof(T), intValue))
        {
            result = (T)Enum.ToObject(typeof(T), intValue);
            return true;
        }

        if (Enum.TryParse(input, true, out T enumValue) && Enum.IsDefined(typeof(T), enumValue))
        {
            result = enumValue;
            return true;
        }

        return false;
    }

    private void showWelcomeMessage()
    {
        StringBuilder welcomeBuilder = new StringBuilder();
        welcomeBuilder.AppendLine("** Welcome to the Ultimate Garage Manager! **");
        welcomeBuilder.AppendLine("We are here to help you manage your vehicles efficiently and effectively.");
        Console.Write(welcomeBuilder);
    }
    
    private void showMenu()
    {
        StringBuilder menuBuilder = new StringBuilder();

        menuBuilder.AppendLine();
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

        Console.Write(menuBuilder);
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
            r_Garage.AssignNewVehicle(vehicle.LicenseLicensePlate, vehicle, eVehicleStatus.InRepair);
            Console.WriteLine("\nThe vehicle now is in the Garage");
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

        float remainingEnergyPrecentage = readRemainingEnergyPrecentage();
        VehicleOwner owner = new VehicleOwner();
        assignOwnerDetails(owner);
        string model = readModel();
        string wheelsManufacture = readWheelsManufacture();
        float currentAirPressure = readCurrentAirPressure();
        eFuelType fuelType = readFuelType();
        float? maximalTankCapacity = readFuelCapacity();
        float? maximalChargeCapacity = readHourlyMaximalChargeCapacity();

        if (vehicleType == eVehicleType.Car)
        {
            eCarColor color = readCarColor();
            eCarDoorCount doorCount = readDoorsNumber();
            return new Car(i_LicensePlate, model, wheelsManufacture, color,
                doorCount, energyType, remainingEnergyPrecentage, fuelType,
                maximalTankCapacity, maximalChargeCapacity, owner
            );
        }

        if (vehicleType == eVehicleType.Truck)
        {
            bool isContainHazardousMaterials = readIsContainHazardousMaterials();
            float trunkCapacity = readTrunkCapacity();
            return new Truck(i_LicensePlate, model, wheelsManufacture, isContainHazardousMaterials,
                trunkCapacity, energyType, remainingEnergyPrecentage, fuelType,
                maximalTankCapacity, owner
            );
        }

        eMotorcycleLicenseType licenseType = readLicenseType();
        int engineCapacity = readEngineCapacity();
        return new Motorcycle(i_LicensePlate, model, wheelsManufacture, licenseType,
            engineCapacity, energyType, remainingEnergyPrecentage, fuelType,
            maximalTankCapacity, maximalChargeCapacity, owner
        );

    }
    
    private int readEngineCapacity()
    {
        Console.WriteLine("\nEnter engine capacity:");
        return int.Parse(Console.ReadLine());
    }
    
    private eMotorcycleLicenseType readLicenseType()
    {
        Console.WriteLine("\nEnter motorcycle license type:\n" +
                                    "1. A1\n" +
                                    "2. A2\n" + 
                                    "3. AB\n" +
                                    "4. B\n"
        );
        
        return ParseEnum<eMotorcycleLicenseType>(Console.ReadLine());
    }

    private float readTrunkCapacity()
    {
        Console.WriteLine("\nEnter truck's trunk capacity :");
        
        return float.Parse(Console.ReadLine());
    }

    private bool readIsContainHazardousMaterials()
    {
        Console.WriteLine("\nIs the truck carry hazardous materials?\n" +
                                    "1.Yes\n" +
                                    "2.No\n" 
                                    );
        int isContainHazardous = int.Parse(Console.ReadLine());

        return (isContainHazardous == 1);
    }
    
    private float? readHourlyMaximalChargeCapacity()
    {
        Console.WriteLine("\nEnter hourly maximal charge capacity:");
        string input = Console.ReadLine();

        if (float.TryParse(input, out float hourlyChargeCapacity))
        {
            return hourlyChargeCapacity;
        }

        return null;    }

    private float? readFuelCapacity()
    {
        Console.WriteLine("\nEnter fuel capacity:");
        string input = Console.ReadLine();

        if (float.TryParse(input, out float fuelCapacity))
        {
            return fuelCapacity;
        }

        return null;
    }

    private eFuelType readFuelType()
    {
        Console.WriteLine("\nEnter fuel type");
        
        return ParseEnum<eFuelType>(Console.ReadLine());
    }

    private eCarDoorCount readDoorsNumber()
    {
        Console.WriteLine("\nEnter doors number (2/3/4/5):");
        
        return ParseEnum<eCarDoorCount>(Console.ReadLine());
    }
    
    private eCarColor readCarColor()
    {
        Console.WriteLine("\nEnter the car color:\n" +
                            "1. Blue\n" +
                            "2. White\n" +
                            "3. Black\n"+
                            "4. Red\n"
                        );
     
        return ParseEnum<eCarColor>(Console.ReadLine());
    }
    
    private float readCurrentAirPressure()
    {
        Console.WriteLine("\nEnter wheels current air pressure:");
        float userCurrentAirPressure = float.Parse(Console.ReadLine());
        
        return userCurrentAirPressure;
    }
    
    private string readWheelsManufacture()
    {
        Console.WriteLine("\nEnter vehicle wheels manufacture:");
        
        return Console.ReadLine();
    }
    
    private string? readModel()
    {
        Console.WriteLine("\nEnter model:");
        
        return Console.ReadLine();
    }
    
    private void assignOwnerDetails(VehicleOwner i_Owner)
    {
        Console.WriteLine("\nEnter owner name:");
        i_Owner.Name = Console.ReadLine();
        Console.WriteLine("\nEnter owner phone:");
        i_Owner.PhoneNumber = Console.ReadLine();
    }
    
    private eEnergySourceType readEnergyType()
    {
        Console.WriteLine("\nChoose energy source type:\n" +
                          "1.Electric\n" +
                          "2.Fuel"
        );
        
        return ParseEnum<eEnergySourceType>(Console.ReadLine());
    }
    
    private float readRemainingEnergyPrecentage()
    {
        Console.WriteLine("\nEnter the remaining energy percentage:");
        string remainingPercentage = Console.ReadLine();
        string pattern = @"^(100|[1-9]?[0-9])%$";

        if (System.Text.RegularExpressions.Regex.IsMatch(remainingPercentage, pattern))
        {
            float percentageValue = float.Parse(remainingPercentage.Substring(0, remainingPercentage.Length - 1));
            
            return percentageValue;
        }

        throw new FormatException("The input must be a valid percentage between 0 and 100, ending with %.");
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
        Console.WriteLine("\nEnter the vehicle type:\n" +
                          "1. Motorcycle\n" +
                          "2. Car\n" +
                          "3. Truck");
        
        return ParseEnum<eVehicleType>(Console.ReadLine());
    }

    private string readLicensePlate()
    {
        Console.WriteLine("Enter License Plate:");
        
        return Console.ReadLine();
    }
    
    private void showLicensePlates()
    {
        Console.WriteLine("Filter by status: (Choose 1-4)\n" +
                          "1. In Repair\n" +
                          "2. Repaired and waiting for payment\n" +
                          "3. Paid\n" +
                          "4. All licenses\n");
        string input = Console.ReadLine();
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
            Console.WriteLine("No license plates found with the given status.");
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
        eVehicleStatus newStatus = ParseEnum<eVehicleStatus>(Console.ReadLine());
        r_Garage.SetStatus(licensePlate, newStatus);
    }

    private void inflateWheelsToMax()
    {
        string licensePlate = readLicensePlate();
        r_Garage.InflateWheelToMax(licensePlate);
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
        eFuelType fuelType = ParseEnum<eFuelType>(Console.ReadLine());
        r_Garage.RefuelVehicle(licensePlate, fuelType, fuelAmount);
    }

    private void chargeVehicleBattery()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine("How many hours to charge?");
        float hoursToCharge = float.Parse(Console.ReadLine());
        r_Garage.Charge(licensePlate, hoursToCharge);
    }

    private void showVehicleFullDetails()
    {
        string licensePlate = readLicensePlate();
        Console.WriteLine($"Information about vehicle {licensePlate} : {r_Garage.GetVehicleDetails(licensePlate)}");
    }
}