using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace mv_twilio
{
    class Program
    {
        private static Options Options { get; set; }

        static void Main(string[] args)
        {
            // init Configuration
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false)
             .AddJsonFile("appsettings.dev.json", true)
             .Build();

            Options = new Options();
            config.Bind(Options);

            TwilioClient.Init(Options.AccountSid, Options.AuthToken);

            var filePath = args.Any() ? args[0] : Options.DefaultFile;

            var counter = 0;
            using (var newfile = File.Create(Options.FailFile))
            {
                using (var logWriter = new System.IO.StreamWriter(newfile))
                using (var reader = File.OpenText(filePath))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        var data = line.Split('\t');

                        var observer = new Observer(data);

                        var success = SendSms(observer, counter);

                        if (!success)
                            logWriter.WriteLine(line);

                        counter++;
                        Thread.Sleep(3000);
                    }
                }
            }
        }

        private static bool SendSms(Observer observer, int counter)
        {
            try
            {
                var message = MessageResource.Create(
                    body: string.Format(Options.Template, observer.Code),
                    from: Options.PhoneNumbers[counter % Options.PhoneNumbers.Length],
                    to: new PhoneNumber(observer.FullPhoneNumber)
                );

                Console.WriteLine(message.Sid);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}
