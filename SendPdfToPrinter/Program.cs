using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestPdfToPrinter
{
    class Program
    {
        //Send data to local IP (Raw printer port)
        static void SendPdf(byte[] text, string ipAddress)
        {

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipAddress), 9100);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ip);
            }
            catch (SocketException e)
            {
                Console.WriteLine($"Error connecting to {ipAddress}");
                return;
            }

            server.Send(text);
            Thread.Sleep(1000); //Some printers need a bit late before shutdown the connection
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Use: TestPdfToPrinter pdfFileName.pdf ipAddress");
                return;
            }

            if (!ValidateIPv4(args[1]))
            {
                Console.WriteLine("Invalid IPv.4 Address");
                return;
            }

            if (File.Exists(args[0]))
            {
                var bytes = File.ReadAllBytes(args[0]);
                SendPdf(bytes, args[1]);
            }
            else
            {
                Console.WriteLine("PDF file not found");
                return;
            }
        }

        //https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
