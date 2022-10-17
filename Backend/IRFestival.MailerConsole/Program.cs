using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Net;

var connectionString = "Endpoint=sb://servicebusaahmedahmed.servicebus.windows.net/;SharedAccessKeyName=listener;SharedAccessKey=wIm9DOTWDoW+f1GqSwvdkzI2R7rMuSzSpUsCgbnoc2E=";
var queueName = "mails";
await using (var client = new ServiceBusClient(connectionString))
{
    var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;
    await processor.StartProcessingAsync();
    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();



    Console.WriteLine("\n Stopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}




static async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    // Mail test = JsonConvert.DeserializeObject<Mail>(body);
    // Console.WriteLine($"Mail to send : {test.Message}\n Receiver : {test.EmailAddress}");
    Console.WriteLine(body);


    await args.CompleteMessageAsync(args.Message);
}



static Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}