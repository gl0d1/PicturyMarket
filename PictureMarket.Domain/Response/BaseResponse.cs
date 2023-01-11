using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Interfaces;

namespace PicturyMarket.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Date { get; set; }
    }
}
