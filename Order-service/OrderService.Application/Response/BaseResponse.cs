
namespace OrderService.Application.Response
{
    public class BaseResponse
    {
        public bool IsSuccess {get; set;} 
        public bool IsError {get; set;}
        public string Message {get; set;}
    }
}