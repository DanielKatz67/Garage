using System.Text;

namespace Ex03.ConsoleUI;

public class GarageUI
{
    private bool m_IsRunning;
    private Dictionary<eMenuChoices, Action> m_MenuActions;

    public GarageUI()
    {
        m_IsRunning = true;

        m_MenuActions = new Dictionary<eMenuChoices, Action>
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
        showWelcomeMessage();
        while (m_IsRunning)
        {
            showMenu();
            if (tryParseEnum<eMenuChoices>(Console.ReadLine(), out eMenuChoices userChoice) && m_MenuActions.ContainsKey(userChoice))
            {
                m_MenuActions[userChoice].Invoke();
            }
            else
            {
                Console.WriteLine($"Inserted invalid choice, Please enter a number between 1-{m_MenuActions.Count}");
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
        welcomeBuilder.AppendLine("********************************************");
        welcomeBuilder.AppendLine("** Welcome to the Ultimate Garage Manager! **");
        welcomeBuilder.AppendLine("********************************************");
        welcomeBuilder.AppendLine("We are here to help you manage your vehicles efficiently and effectively.");
        welcomeBuilder.AppendLine();
    }
    
    private void showMenu()
    {
        StringBuilder menuBuilder = new StringBuilder();

        menuBuilder.AppendLine($"Please select one of the options below (Choose 1-{m_MenuActions.Count}):");
        menuBuilder.AppendLine("1. Enter a new vehicle to the garage");
        menuBuilder.AppendLine("2. Show plates list by filter");
        menuBuilder.AppendLine("3. Change vehicle status");
        menuBuilder.AppendLine("4. Inflate vehicle tires to the maximum");
        menuBuilder.AppendLine("5. Refuel fuel-powered vehicle");
        menuBuilder.AppendLine("6. Charge an electric vehicle");
        menuBuilder.AppendLine("7. Show vehicle info by its plate");
        menuBuilder.AppendLine("8. Quit");
        menuBuilder.AppendLine();
        menuBuilder.AppendLine($"Enter your choice from the following (1-{m_MenuActions.Count}):");

        Console.WriteLine(menuBuilder.ToString());
    }
    
    private void insertVehicleToGarage()
    {
        Console.WriteLine("insertVehicleToGarage");
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