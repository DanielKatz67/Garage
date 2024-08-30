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
        while (m_IsRunning)
        {
            Console.WriteLine($"\nEnter your choice from the following (1-{m_MenuActions.Count}):");

            if (TryParseEnum<eMenuChoices>(Console.ReadLine(), out eMenuChoices userChoice) && m_MenuActions.ContainsKey(userChoice))
            {
                m_MenuActions[userChoice].Invoke();
            }
            else
            {
                Console.WriteLine($"Inserted invalid choice, Please enter a number between 1-{m_MenuActions.Count}");
            }
        }
    }

    private bool TryParseEnum<T>(string input, out T result) where T : struct, Enum
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