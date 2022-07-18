using Patika.Shared.Exceptions;

namespace Patika.Shared.DTO.Validators
{
    public class RegexValidatorInput
    {
        public string Input { get; set; }
        public string Pattern { get; set; }
        public BaseException Exception { get; set; }
        public bool MustMatch { get; set; } = false;
    }
}
