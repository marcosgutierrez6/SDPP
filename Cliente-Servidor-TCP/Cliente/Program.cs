using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using Biblioteca1;

namespace Cliente
{
    internal class Program
    {
        public static void Main()
        {
            TcpChannel tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel);
            Type requiredType = typeof(MovieTicketInterface);
            MovieTicketInterface remoteObject =
            (MovieTicketInterface)Activator.GetObject(requiredType,
            "tcp://localhost:9998/MovieTicketBooking");
            Console.WriteLine(remoteObject.GetTicketStatus("Ticket No: 3344"));
        }

    }
}
