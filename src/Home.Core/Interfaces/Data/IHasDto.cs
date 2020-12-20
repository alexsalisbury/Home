namespace Home.Core.Interfaces.Models
{
    public interface IHasDto<T>
    {
        T DtoCache { get; }
        T ToDto();
    }
}
