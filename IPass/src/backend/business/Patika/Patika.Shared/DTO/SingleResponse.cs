namespace Patika.Shared.DTO
{
    public class SingleResponse<T> where T : class 
    {
        public T Data { get; set; }

    }
}
