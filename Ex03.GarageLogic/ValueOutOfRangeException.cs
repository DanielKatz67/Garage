namespace Ex03.GarageLogic;

public class ValueOutOfRangeException : Exception
{
    private readonly float r_MaxValue;
    private readonly float r_MinValue;

    public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) 
        : base(String.Format("Value range is between {0} to {1}", i_MinValue, i_MaxValue))
    {
        this.r_MinValue = i_MinValue;
        this.r_MaxValue = i_MaxValue;
    }
    
    public float MaxValue
    {
        get { return this.r_MaxValue; }
    }
    public float MinValue
    {
        get { return this.r_MinValue; }
    }
}