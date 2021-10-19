using DNI.Shared.Extensions;
using NUnit.Framework;
using System;

namespace DNI.Tests
{
    public class ETagTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateETag()
        {
            Customer customer = new() 
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
            Customer customer2 = new()
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

            var expected = customer.CalculateETag();
            var actual = customer2.CalculateETag();

            Assert.AreEqual(expected, actual);

            customer2.LastName = "Harrison";
            customer2.Title = "Mrs";

            expected = customer.CalculateETag();
            actual = customer2.CalculateETag();

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void UpdateETag()
        {
            Customer customer = new()
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

            customer.UpdateETag();

            Assert.IsNotEmpty(customer.ETag);
            Assert.AreEqual(customer.CalculateETag(), customer.ETag);
        }

        [Test]
        public void ValidateETag()
        {
            Customer customer = new()
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
            Customer customer2 = new()
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

            var actual = customer2.CalculateETag();

            Assert.True(customer.ValidateETag(actual));

            customer2.LastName = "Harrison";
            customer2.Title = "Mrs";

            actual = customer2.CalculateETag();

            Assert.False(customer.ValidateETag(actual));
        }

        public void UpdateETag_comparison()
        {
            Customer customer = new()
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

            customer.UpdateETag();

            Assert.IsNotEmpty(customer.ETag);

            Assert.AreEqual(customer.CalculateETag(), customer.ETag);
        }
    }
}