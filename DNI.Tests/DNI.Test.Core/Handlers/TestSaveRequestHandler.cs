using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Defaults;
using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
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

        protected override Task<bool> OnAdd(Customer request, CancellationToken cancellationToken)
        {
            return base.OnAdd(request, cancellationToken);
        }

        protected override Task<bool> OnUpdate(Customer request, CancellationToken cancellationToken)
        {
            return base.OnUpdate(request, cancellationToken);
        }

        protected override Task OnAddUpdateFailure(Customer request, EventType eventType, CancellationToken cancellationToken)
        {
            return base.OnAddUpdateFailure(request, eventType, cancellationToken);
        }

        protected override Task OnAddUpdateSuccessful(Customer request, EventType eventType, CancellationToken cancellationToken)
        {
            return base.OnAddUpdateSuccessful(request, eventType, cancellationToken);
        }

        protected override Task<bool> ValidateModel(Customer model, CancellationToken cancellationToken, out IEnumerable<IValidationFailure> validationFailures)
        {
            List<IValidationFailure> validationFailuresList = new();

            validationFailuresList.Add(ValidationFailure.Create(model, new ArgumentException("First name must be more than 2 characters long", "FirstName"), "FirstName"));
            validationFailuresList.Add(ValidationFailure.Create(model, new ArgumentException("Last name must be more than 2 characters long", "LastName"), "LastName"));
            validationFailuresList.Add(ValidationFailure.Create(model, new ArgumentException("Email Address must be a valid e-mail address", "EmailAddress"), "EmailAddress"));
            validationFailures = validationFailuresList;
            return Task.FromResult(false);
        }
    }
}
