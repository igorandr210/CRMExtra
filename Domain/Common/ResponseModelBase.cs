namespace Domain.Common
{
    public static class ResponseHelper
    {
        public static ResponseModelBase<T> GetResponse<T>(this T response)
        {
            return new(response);
        }
    }

    public class ResponseModelBase<T>
    {
        public ResponseModelBase(T data)
        {
            Data = data;
        }
        
        public ResponseModelBase(T data, string errorMessage)
        {
            Data = data;
            ErrorMessage = errorMessage;
        }
        
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
