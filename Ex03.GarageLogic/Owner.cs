namespace Ex03.GarageLogic;

public class VehicleOwner
{
    private string m_Name;
    private string m_PhoneNumber;
    
    public string Name
    {
        get { return this.m_Name; }
        set { this.m_Name = value; }
    }
    
    public string PhoneNumber
    {
        get { return this.m_PhoneNumber; }
        set { this.m_PhoneNumber = value; }
    }

    
    public override string ToString()
    {
        return string.Format("Vehicle owner Name: {0}, phone number: {1}", m_Name, m_PhoneNumber);
    }
}