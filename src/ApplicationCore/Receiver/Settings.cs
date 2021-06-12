using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver
{
    public class ReceiverSettings
    {
        public string Environment { get; set; }
        public string Provider { get; set; }
        public string IP { get; set; }
        public string SID { get; set; }
        public string Password { get; set; }
        public string LogFile { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
    }
}
