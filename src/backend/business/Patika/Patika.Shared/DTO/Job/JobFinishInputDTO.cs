using Patika.Shared.Enums;
using System;

namespace Patika.Shared.DTO.Job
{
    public class JobFinishInputDTO
    {
        public Guid JobId { get; set; }
        public LogStatus FinalStatus { get; set; } = LogStatus.Success;
    }
}