using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlockks.Exceptions
{
    public class InvalidServerException : Exception
    {
        public InvalidServerException(string message) : base(message)
        {
            
        }
        public InvalidServerException(string message, string details): base(message)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
