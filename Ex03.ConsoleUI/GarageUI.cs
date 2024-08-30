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
        string licensePlate = readLicensePlate();
        if (r_Garage.IsVehicleExists(licensePlate))
        {
            Console.WriteLine("This vehicle already exists, updating status to In Repair");
            r_Garage.SetStatus(licensePlate, eVehicleStatus.InRepair);
        }
        else
        {
            
        }
    }

    private string readLicensePlate()
    {
        Console.WriteLine("Enter Plate id:");
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