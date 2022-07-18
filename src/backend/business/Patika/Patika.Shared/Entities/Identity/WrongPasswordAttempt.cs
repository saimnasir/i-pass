using System;

namespace Patika.Shared.Entities.Identity
{
    public class WrongPasswordAttempt : Entity
    {
        public string UserId { get; set; }
        public DateTime AttemptTime { get; set; }
    }
}