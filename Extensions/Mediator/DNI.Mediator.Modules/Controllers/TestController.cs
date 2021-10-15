using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DNI.Mediator.Modules.Controllers
{
    public class User
    {
        public int Id { get; set; }
        [EncryptionProfile("Personal", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string Username { get; set; }

        [EncryptionProfile("Personal", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string EmailAddress { get; set; }

        [EncryptionProfile("Common", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string FirstName { get; set; }

        [EncryptionProfile("Common", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string MiddleName { get; set; }

        [EncryptionProfile("Common", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string LastName { get; set; }

        [EncryptionProfile("Credential", DNI.Shared.Enumerations.EncryptionType.Encryption)]
        public string Password { get; set; }
    }

    [Route("{controller}/{action}")]
    public class TestController : ControllerBase
    {
        private readonly IModelEncryptor modelEncryptor;

        public TestController(IModelEncryptor modelEncryptor)
        {
            this.modelEncryptor = modelEncryptor;
        }

        [HttpGet]
        public ActionResult Index(User value)
        {
            var encrypted = modelEncryptor.Encrypt(value);
            return Ok(encrypted);
        }
    }
}
