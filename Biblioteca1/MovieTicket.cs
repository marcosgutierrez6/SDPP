using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca1
{
    public class MovieTicket : MarshalByRefObject, MovieTicketInterface
    {
        public string GetTicketStatus(string stringToPrint)
        {
            string returnStatus = "Ticket Confirmed";
            Console.WriteLine("Enquiry for {0}", stringToPrint);
            Console.WriteLine("Sending back status: {0}", returnStatus);
            return returnStatus;
        }
    }
}
