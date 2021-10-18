using DNI.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Tests
{
    public class Customer
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string Title { get; set; }

        public string NationalInsuranceNumber { get; set; }

        public string EmailAddress { get; set; }

        public string BusinessEmailAddress { get; set; }

        public string TelephoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ETag]
        public string ETag { get; set; }
    }
}
