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

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketServer();
        }
        static void TicketServer()
        {
            Console.WriteLine("Ticket Server started...");
            TcpChannel tcpChannel = new TcpChannel(9998);
            ChannelServices.RegisterChannel(tcpChannel);
            Type commonInterfaceType = typeof(MovieTicket);
            RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
            "MovieTicketBooking", WellKnownObjectMode.SingleCall);
            System.Console.WriteLine("Press ENTER to quitnn");
            System.Console.ReadLine();
        }
    }
}
