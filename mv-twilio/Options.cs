namespace mv_twilio
{
    public class Options
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string Template { get; set; }
        public string FailFile { get; set; }
        public string DefaultFile { get; set; }
        public string[] PhoneNumbers { get; set; }
    }
}
