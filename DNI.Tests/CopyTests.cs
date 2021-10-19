using DNI.Shared.Attributes;
using DNI.Shared.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Tests
{
    public class CopyTests
    {
        [Test]
        public void Test1()
        {
            var itemType = typeof(Customer);
            Customer item = new()
            {
                BusinessEmailAddress = "lisa.wednesbury@business.com",
                DateOfBirth = DateTime.Parse("1984-11-12"),
                EmailAddress = "lisa.w23823@gmail.com",
                FirstName = "Lisa",
                Id = Guid.Parse("9ea1a22a-bd64-4aab-820a-c7092ee31720"),
                LastName = "Jordan",
                MiddleName = "Harrison",
                MobileNumber = "078876543211",
                NationalInsuranceNumber = "012345678910",
                TelephoneNumber = "012345678910",
                Title = "Mrs"
            };
            var keys = item.GetDictionary<KeyAttribute>();
            Customer existingItem = new()
            {
                BusinessEmailAddress = "lisa.wednesbury@business.com",
                DateOfBirth = DateTime.Parse("1984-11-12"),
                EmailAddress = "lisa.w23823@gmail.com",
                FirstName = "Lisa",
                Id = Guid.Parse("9ea1a22a-bd64-4aab-820a-c7092ee31720"),
                LastName = "Wednesbury",
                MiddleName = "Harrison",
                MobileNumber = "078876543211",
                NationalInsuranceNumber = "012345678910",
                TelephoneNumber = "012345678910",
                Title = "Ms"
            };

            if (existingItem != null)
            {
                //copy meta values to entry
                existingItem.Copy(item, properties: itemType
                    .GetPropertiesWithAttribute<MetaPropertyAttribute>().Select(a => a.Key));
            }

        }
    }
}
