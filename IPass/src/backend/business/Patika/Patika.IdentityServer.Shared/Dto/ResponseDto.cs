using Patika.Shared.Exceptions;

namespace Patika.IdentityServer.Shared.Dto
{
    public class ResponseDto
    {
        public bool Result { get; set; }
        public ErrorDto Error { get; set; } = new ErrorDto();

        public static ResponseDto Ok() => new() { Result = true };
        public static ResponseDto Cancel(string code, string msg) => new() { Result = false, Error = new ErrorDto { Code = code, Message = msg } };

        public static implicit operator ResponseDto(BaseApplicationException ex)=> Cancel(ex.Code, ex.Message);
        public static implicit operator ResponseDto(Exception ex) => Cancel("", ex.Message);
    }
}