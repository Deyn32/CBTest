using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTest.models
{
    public class ParticipantInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Bic { get; set; }
        public DateTime DateIn { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
