using Patika.Shared.Enums;
using System;

namespace Patika.Shared.DTO.Job
{
    public class JobDetailCreateDTO
    {
        public Guid JobId { get; set; }
        public string Module { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LogType LogType { get; set; }
        public string InputAsJson { get; set; }
        public string OutputAsJson { get; set; }
    }
}