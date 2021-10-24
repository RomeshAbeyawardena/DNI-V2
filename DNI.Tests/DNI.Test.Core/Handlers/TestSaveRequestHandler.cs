using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Tests.Shared.Models;
using DNI.Tests.Shared.Request;

namespace DNI.Test.Core.Handlers
{
    public class TestSaveRequestHandler : EncryptedRepositorySaveHandlerBase<TestSaveRequest, Customer>
    {
        public TestSaveRequestHandler(IModelEncryptor encryptor, IAsyncRepository<Customer> modelRepository) 
            : base(encryptor, modelRepository)
        {
        }

        protected override Task<Guid> GetKey(Customer request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.Id);
        }

        protected override Customer GetModel(TestSaveRequest request)
        {
            return request.Customer;
        }

        protected override Task<Guid> SetKey(Customer request, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid();
            request.Id = key;
            return Task.FromResult(key);
        }

        protected override Task<bool> ValidateModel(Customer model, CancellationToken cancellationToken, out IEnumerable<IValidationFailure> validationFailures)
        {
            validationFailures = Array.Empty<IValidationFailure>();
            return Task.FromResult(false);
        }
    }
}
