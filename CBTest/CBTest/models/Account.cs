using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTest.models
{
    public class Account
    {
        public int Id { get; set; }
        public Nullable<DateTime> DateIn { get; set; }
        public string Status { get; set; }
        public string CbrBic { get; set; }
        public string Number { get; set; }
        public string CK { get; set; }
        public string Type { get; set; }
        public int Participant { get; set; }
    }
}
