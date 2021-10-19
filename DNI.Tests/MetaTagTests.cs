using DNI.Shared.Abstractions;
using DNI.Shared.Extensions;
using Moq;
using NUnit.Framework;
using System;

namespace DNI.Tests
{
    public class MetaTagTests
    {
        private Mock<IClockProvider> clockProvider;

        [SetUp]
        public void SetUp()
        {
            clockProvider = new Mock<IClockProvider>();
        }

        [Test] 
        public void UpdateMetaTags_OnAdd()
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

            var expectedDateTime = new DateTimeOffset(2021, 07, 01, 12, 30, 35, TimeSpan.FromHours(0));
            var expectedUtcDateTime = new DateTimeOffset(2021, 07, 01, 12, 30, 35, TimeSpan.FromHours(1));

            clockProvider.Setup(a => a.GetDateTime(true))
                .Returns(expectedUtcDateTime.DateTime);

            clockProvider.Setup(a => a.GetDateTimeOffset(true))
                .Returns(expectedUtcDateTime);

            clockProvider.Setup(a => a.GetDateTime(false))
                .Returns(expectedDateTime.DateTime);

            clockProvider.Setup(a => a.GetDateTimeOffset(false))
                .Returns(expectedDateTime);

            customer.UpdateMetaTags(Shared.Enumerations.MetaAction.Add, 
                clockProvider.Object.GetDateTimeOffset,
                clockProvider.Object.GetDateTime);

            Assert.False(customer.Created.IsDefault());
            Assert.False(customer.CreatedUtc.IsDefault());

            Assert.AreEqual(customer.Created, expectedUtcDateTime.DateTime);
            Assert.AreEqual(customer.CreatedUtc, expectedUtcDateTime);
        }

        [Test]
        public void UpdateMetaTags_OnUpdate()
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

            customer.UpdateMetaTags(Shared.Enumerations.MetaAction.Update);

            Assert.True(customer.Created.IsDefault());
            Assert.True(customer.CreatedUtc.IsDefault());
            Assert.False(customer.Modified.IsDefault());
            Assert.False(customer.ModifiedUtc.IsDefault());
        }
    }
}
