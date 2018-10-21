using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ApprenticeAddress
    {
        public Guid Id { get; set; }
        public Guid ApprenticeId { get; set; }
        public virtual Apprentice Apprentice { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public Guid CountyId { get; set; }
        // public virtual County County { get; set; }
        public string PostCode { get; set; }
        public bool IsPrimaryAddress { get; set; }
        public Guid AddressTypeId { get; set; }
        // public virtual AddressType AddressType { get; set; }
    }
}
