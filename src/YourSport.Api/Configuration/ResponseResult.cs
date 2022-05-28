namespace YourSport.Api.Configuration;

public class ResponseResult<T>
{
    public bool Error { get; set; }
    public List<string> ErrorMessage { get; set; }
    public T Result { get; set; }
}