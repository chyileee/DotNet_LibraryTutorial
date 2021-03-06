﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace socket_prog
{
    class Client
    {
        private static string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private static void Main(String[] args)
        {
            byte[] data = new byte[10];

            IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAdress = IPAddress.Parse(GetLocalIP());
            IPEndPoint ipEndpoint = new IPEndPoint(ipAdress, 8080);// 32000);

            IPAddress localIPAddress = ipAdress;
            IPEndPoint localEndPoint = new IPEndPoint(localIPAddress, 9000);

            Socket client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Bind(localEndPoint);
            //client.Listen(5);

            try
            {
                client.Connect(ipEndpoint);

                Console.WriteLine("Tcp Client => Tcp_sock_client_A.cs");

                Console.WriteLine("LocalEndPoint = {0}", client.LocalEndPoint.ToString());
                Console.WriteLine("\nConnected to Server = {0}", client.RemoteEndPoint.ToString());

                byte[] sendmsg = Encoding.ASCII.GetBytes("This is from Client\n");

                int n = client.Send(sendmsg);

                int m = client.Receive(data);

                Console.WriteLine("" + Encoding.ASCII.GetString(data));
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Transmission end.");
            Console.ReadKey();

        }
    }
}