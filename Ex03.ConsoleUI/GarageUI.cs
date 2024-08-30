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
        
    }
    
    private void insertVehicleToGarage() { /* ... */ }
    private void showLicensePlates() { /* ... */ }
    private void changeVehicleStatus() { /* ... */ }
    private void inflateWheelsToMax() { /* ... */ }
    private void refuelVehicle() { /* ... */ }
    private void chargeVehicleBattery() { /* ... */ }
    private void showVehicleFullDetails() { /* ... */ }
}