using Patika.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Patika.Shared.Entities.Loggy.Entities
{
    public class Log : GenericEntity<Guid>
    {
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime? EndDateTime { get; set; }
        public LogStatus Status { get; set; } = LogStatus.Started;
        public string ApplicationName { get; set; } = string.Empty;
        public virtual ICollection<LogDetail> Details { get; set; }
    }
}
