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
            try
            {
                eMenuChoices userChoice = this.ParseEnum<eMenuChoices>(Console.ReadLine());
                if (m_MenuActions.ContainsKey(userChoice))
                {
                    m_MenuActions[userChoice].Invoke();
                }
                else
                {
                    Console.WriteLine("Inserted invalid choice, Please enter a number between 1-8");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
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

    
    private void insertVehicleToGarage() { /* ... */ }
    private void showLicensePlates() { /* ... */ }
    private void changeVehicleStatus() { /* ... */ }
    private void inflateWheelsToMax() { /* ... */ }
    private void refuelVehicle() { /* ... */ }
    private void chargeVehicleBattery() { /* ... */ }
    private void showVehicleFullDetails() { /* ... */ }
}