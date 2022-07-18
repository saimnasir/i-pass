using System;

namespace Patika.Shared.Entities.Liveness
{
    public class CheckResult
    {
        public string Error { get; set; } = string.Empty;
        public bool Success { get; set; }

        public static CheckResult Successful() => new() { Success = true };
        public static CheckResult Failure(string error) => new() { Success = false, Error = error };
        public static CheckResult Failure(Exception ex) => new() { Success = false, Error = ex.Message };

        public static implicit operator CheckResult(Exception ex) => Failure(ex);
    }
}