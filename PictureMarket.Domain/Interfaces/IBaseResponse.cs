using PicturyMarket.Domain.Enum;

namespace PicturyMarket.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}