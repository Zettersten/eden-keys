// See https://aka.ms/new-console-template for more information
using EdenKeys.Server;

var server = new KeyBroadcasterServer();

server.StartServer();

Console.ReadLine();