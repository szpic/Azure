using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstAzureFunction.ErrorDto
{
    public class Errors
    {
        public List<Error> error { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
    }
}
