using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class watsapp
{
    public static void send(string otp,string to)
    {
        var accountSid = "AC2692b732e87ef6e3e6f3da4a635f5d1d";
        var authToken = "2d30bfd56505ded768a64c6470e562e5";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
            new PhoneNumber("whatsapp:"+to));
        messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
        messageOptions.Body = "Your reset pin is "+otp+".";

        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);
    }
}
