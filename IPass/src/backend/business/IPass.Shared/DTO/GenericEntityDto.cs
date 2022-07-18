using Newtonsoft.Json;
using Patika.Shared.Interfaces;
using System;

namespace IPass.Shared.DTO
{
    public abstract class GenericDto<T> :IDTO, IHasCreated, IHasUpdated
    {
        public virtual T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string LogId { get; set;}
    }
}
