using System;
using Patika.Shared.Enums;


namespace Patika.Shared.Entities.Loggy.Entities
{
    public class LogDetail : GenericEntity<Guid>
    {
        public Guid LogId { get; set; }
        public virtual Log Log { get; set; }
        public string Module { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LogType LogType { get; set; }
        public string InputAsJson { get; set; } = string.Empty;
        public string OutputAsJson { get; set; } = string.Empty;
    }
}