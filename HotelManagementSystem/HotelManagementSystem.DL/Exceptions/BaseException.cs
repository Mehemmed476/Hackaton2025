namespace HotelManagementSystem.DL.Exceptions;

public class BaseException : Exception
{
    public BaseException() : base("Something went wrong.")
    {
        
    }
    public BaseException(string mes) : base(mes)
    {
        
    }
}
