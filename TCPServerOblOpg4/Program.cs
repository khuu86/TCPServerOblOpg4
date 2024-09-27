// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;

Console.WriteLine("TCP Server OblOpg4");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);

    Task.Run(() => HandleClient(socket));
}

listener.Stop();

void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    int counter = 0;

    while (socket.Connected)
    {
        string message = reader.ReadLine().ToLower(); // Modtager besked fra client (i lowercase)
        Console.WriteLine("Besked modtaget:" + message);

        counter++;

        switch (message)
        {
            case "stop":
                writer.WriteLine("Goodbye: Closing down Client");
                writer.Flush();
                socket.Close();
                break;

            case "random":
                writer.WriteLine("Random number x and y");
                writer.Flush();

                string[] messageA = reader.ReadLine().Split(" ");
                int x = Int32.Parse(messageA[0]);
                int y = Int32.Parse(messageA[1]);
                Console.WriteLine(x);
                Console.WriteLine(y);

                int minValue = Math.Min(x, y);
                int maxValue = Math.Max(x, y);

                Random random = new Random();
                int n = random.Next(minValue, maxValue + 1); // +1 for at inkludere maxValue i det tilfældige interval: Random.Next()
                string sn = n.ToString();
                writer.WriteLine(sn);
                writer.Flush();
                break;

            case "add":
                writer.WriteLine("Add two numbers");
                writer.Flush();

                string[] messageB = reader.ReadLine().Split(" ");
                int a = Int32.Parse(messageB[0]);
                int b = Int32.Parse(messageB[1]);
                Console.WriteLine(a);
                Console.WriteLine(b);

                int sum = a + b;
                string samletSum = sum.ToString();
                writer.WriteLine(samletSum);
                writer.Flush();
                break;

            case "subtract":
                writer.WriteLine("Subtract two numbers");
                writer.Flush();

                string[] messageC = reader.ReadLine().Split(" ");
                int c = Int32.Parse(messageC[0]);
                int d = Int32.Parse(messageC[1]);
                Console.WriteLine(c);
                Console.WriteLine(d);

                int forskel = c - d;
                string samletForskel = forskel.ToString();
                writer.WriteLine(samletForskel);
                writer.Flush();
                break;

            default:
                writer.WriteLine("Unknown command");
                writer.Flush();
                break;
        }





        //if (message == "stop")
        //{

        //    writer.WriteLine("Goodbye World");
        //    writer.Flush();
        //    socket.Close();
        //}

        //if (message.Contains("Random"))
        //{

        //    writer.WriteLine("Random number x and y");
        //    writer.Flush();

        //    string[] messageA = message.Split(" ");
        //    int x = Int32.Parse(messageA[0]);
        //    int y = Int32.Parse(messageA[1]);
        //    Console.WriteLine(x);
        //    Console.WriteLine(y);

        //    Random random = new Random();
        //    int n = random.Next(x, y);
        //    string sn = n.ToString();
        //    writer.WriteLine(sn);
        //    writer.Flush();

        //}
    }
}