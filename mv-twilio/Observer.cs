namespace mv_twilio
{
    public enum DataPosition
    {
        Number = 0,
        Code = 1
    }
    public class Observer
    {
        public string Code { get; private set; }

        public Observer(string[] fileData)
        {
            FullPhoneNumber = fileData[(int)DataPosition.Number];
            Code = fileData[(int) DataPosition.Code];
        }

        public Observer(string fileLine) : this(fileLine.Split("\t"))
        {
        }

        public string FullPhoneNumber { get; }
    }
}
