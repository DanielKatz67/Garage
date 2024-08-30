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
            { eMenuChoices.InsertVehicle, this.insertVehicleToGarage },
            { eMenuChoices.ShowLicensePlates, this.showLicensePlates },
            { eMenuChoices.ChangeVehicleStatus, this.changeVehicleStatus },
            { eMenuChoices.InflateWheelsToMax, this.inflateWheelsToMax },
            { eMenuChoices.Refuel, this.refuelVehicle },
            { eMenuChoices.Charge, this.chargeVehicleBattery },
            { eMenuChoices.ShowVehicleFullDetails, this.showVehicleFullDetails },
            { eMenuChoices.Exit, () => m_IsRunning = false }
        };
    }
    
    public void Run()
    {
        try
        {
            showWelcomeMessage();
            while (m_IsRunning)
            {
                showMenu();
                if (tryParseEnum<eMenuChoices>(Console.ReadLine(), out eMenuChoices userChoice))
                {
                    r_MenuActions[userChoice].Invoke();
                }
                else
                {
                    Console.WriteLine($"Inserted invalid choice, Please enter a number between 1-{r_MenuActions.Count}");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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
        welcomeBuilder.AppendLine("********************************************");
        welcomeBuilder.AppendLine("** Welcome to the Ultimate Garage Manager! **");
        welcomeBuilder.AppendLine("********************************************");
        welcomeBuilder.AppendLine("We are here to help you manage your vehicles efficiently and effectively.");

        Console.WriteLine(welcomeBuilder);
    }
    
    private void showMenu()
    {
        StringBuilder menuBuilder = new StringBuilder();

        menuBuilder.AppendLine($"Please select one of the options below (Choose 1-{r_MenuActions.Count}):");
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

        Console.WriteLine(menuBuilder);
    }
    
    private void insertVehicleToGarage()
    {
        eEnergySourceType energyType;
        string licensePlate = readLicensePlate();
        if (r_Garage.IsVehicleExists(licensePlate))
        {
            Console.WriteLine("This vehicle already exists, updating status to In Repair");
            r_Garage.SetStatus(licensePlate, eVehicleStatus.InRepair);
        }
        else
        {
            eVehicleType vehicleType = this.readVehicleType();
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
            eFuelType fuelType;
            float maximalTankCapacity;
            if (energyType == eEnergySourceType.Fuel)
            {
                fuelType = readFuelType();
                maximalTankCapacity = readFuelCapacity();
            }
            else if (energyType == eEnergySourceType.Electric)
            {
                
            }
            Vehicle vehicle;
            
            if (vehicleType == eVehicleType.Car)
            {
                eCarColor color = this.readCarColor();
                eCarDoorCount doorCount = this.readDoorsNumber();
                vehicle = new Car(licensePlate, model, wheelsManufacture, color, doorCount, energyType, remainingEnergyPrecentage, fuelType)
            }
            else if (vehicleType == eVehicleType.Truck)
            {
                this.userReadIsContainHazardousMaterials(vehicleToEnter);
                this.userReadTrunkCapacity(vehicleToEnter);
            }
            else if (vehicleType == eVehicleType.Motorcycle)
            {
                this.userReadLicenseType(vehicleToEnter);
                this.userReadEngineCapacity(vehicleToEnter);
            }
            
            // TODO: Initialize vehicle
            this.r_Garage.EnterVehicleToGarage(vehicleToEnter);
            Console.Clear();
            Console.WriteLine("The vehicle now is in the Garage");
        }
    }

    private float? readFuelCapacity()
    {
        Console.WriteLine("Enter fuel capacity:");
        string input = Console.ReadLine();

        if (float.TryParse(input, out float fuelCapacity))
        {
            return fuelCapacity;
        }

        return null;
    }

    private eFuelType readFuelType()
    {
        Console.WriteLine("Enter fuel type");
        
        return this.ParseEnum<eFuelType>(Console.ReadLine());
    }

    private eCarDoorCount readDoorsNumber()
    {
        Console.WriteLine("Enter doors number (2/3/4/5):");
        
        return this.ParseEnum<eCarDoorCount>(Console.ReadLine());
    }
    
    private eCarColor readCarColor()
    {
        Console.WriteLine($"Enter the car color:" +
                            $"1. Red" +
                            $"2. Blue" +
                            $"3. Black"+
                            $"4. Gray"
                        );
     
        return ParseEnum<eCarColor>(Console.ReadLine());
    }
    
    private float readCurrentAirPressure()
    {
        Console.WriteLine("Enter wheels current air pressure:");
        float userCurrentAirPressure = float.Parse(Console.ReadLine());
        
        return userCurrentAirPressure;
    }
    
    private string readWheelsManufacture()
    {
        Console.WriteLine("Enter vehicle wheels manufacture:");
        
        return Console.ReadLine();
    }
    
    private string? readModel()
    {
        Console.WriteLine("Enter model:");
        
        return Console.ReadLine();
    }
    
    private void assignOwnerDetails(VehicleOwner i_Owner)
    {
        Console.WriteLine("Enter owner name:");
        i_Owner.Name = Console.ReadLine();
        Console.WriteLine("Enter owner phone:");
        i_Owner.PhoneNumber = Console.ReadLine();
    }
    
    private eEnergySourceType readEnergyType()
    {
        Console.WriteLine($"Choose energy source type: (1 or 2)" +
                          $"1.Electric" +
                          $"2.Fuel-powered"
        );
        
        return this.ParseEnum<eEnergySourceType>(Console.ReadLine());
    }
    
    private float readRemainingEnergyPrecentage()
    {
        Console.WriteLine("The percent of energy in the vehicle (Format of: <0-100%>):");
        string remainingPrecentage = Console.ReadLine();
        if (remainingPrecentage[remainingPrecentage.Length - 1] == '%')
        {
            float leftPercentsFloat = float.Parse(remainingPrecentage.Substring(0, remainingPrecentage.Length - 1));
            if (leftPercentsFloat <= 100.0F && leftPercentsFloat >= 0.0F)
            {
                return leftPercentsFloat;
            }

            throw new ValueOutOfRangeException(0.0F, 100.0F);
        }
        
        throw new FormatException("The number you enterd should end with %  ");
    }
    
    private T ParseEnum<T>(string i_String)
    {
        if (Enum.IsDefined(typeof(T), int.Parse(i_String)))
        {
            return (T)Enum.ToObject(typeof(T), int.Parse(i_String));
        }
        else if (Enum.IsDefined(typeof(T), i_String))
        {
            return (T)Enum.Parse(typeof(T), i_String, true);
        }
        else
        {
            throw new FormatException();
        }
    }
    
    private eVehicleType readVehicleType()
    {
        Console.WriteLine($"Enter the vehicle type:" +
                          $"1.Motorcycle" +
                          $"2.Car" +
                          $"3.Truck");
        
        return this.ParseEnum<eVehicleType>(Console.ReadLine());
    }


    private string readLicensePlate()
    {
        Console.WriteLine("Enter License Plate:");
        
        return Console.ReadLine();
    }
    
    private void showLicensePlates()
    {
        Console.WriteLine("showLicensePlates");
    }

    private void changeVehicleStatus()
    {
        Console.WriteLine("changeVehicleStatus");
    }

    private void inflateWheelsToMax()
    {
        Console.WriteLine("inflateWheelsToMax");
    }

    private void refuelVehicle()
    {
        Console.WriteLine(refuelVehicle);
    }

    private void chargeVehicleBattery()
    {
        Console.WriteLine("chargeVehicleBattery");
    }

    private void showVehicleFullDetails()
    {
        Console.WriteLine("showVehicleFullDetails");
    }
}