using Microsoft.AspNetCore.Identity;

namespace Shared.Domain.Entities.Identity
{
    public class IdentityBase : IdentityUser
    {
        public IdentityBase()
        {

        }
        public IdentityBase(string salt)
        {
            Salt = salt;
            IsActive = true;
        }
        public string? Salt { get; set; }
        public bool IsActive { get; set; }
        public bool IsFaceBookAcc { get; set; }
        public bool IsGmailAcc { get; set; }
    }
}
