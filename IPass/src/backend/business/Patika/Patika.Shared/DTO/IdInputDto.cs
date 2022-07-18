namespace Patika.Shared.DTO
{
    public class IdInputDto<T> : DTO
    {
        public T Id { get; set; }
 
        public IdInputDto(T id)
        {
            Id = id;
        }
    }
}