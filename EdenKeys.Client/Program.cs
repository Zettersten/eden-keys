// See https://aka.ms/new-console-template for more information
using EdenKeys.Client;

try
{
    var client = new KeyBroadcasterClient("192.168.1.77", 3000);
    client.ConnectToServer();
}
finally
{
}

Console.WriteLine("No connection! Press any key to exit.");
Console.ReadLine();