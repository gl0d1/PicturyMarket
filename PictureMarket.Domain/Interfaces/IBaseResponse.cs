using PicturyMarket.Domain.Enum;

namespace PicturyMarket.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        StatusCode StatusCode { get; }
        T Date { get; }
    }
}