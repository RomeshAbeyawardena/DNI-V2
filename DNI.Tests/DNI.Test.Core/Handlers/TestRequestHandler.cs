using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Tests.Shared.Request;
using DNI.Tests.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.Core.Handlers
{
    public class TestRequestHandler : EncryptedRepositoryRequestListHandlerBase<TestRequest, Customer>
    {
        private readonly IRepository<Customer> customerRepository;

        public TestRequestHandler(
            IModelEncryptor encryptor,
            IRepository<Customer> customerRepository)
            : base(encryptor, customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public override async Task<IEnumerable<Customer>> Get(TestRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new List<Customer>();
        }

    }
}
