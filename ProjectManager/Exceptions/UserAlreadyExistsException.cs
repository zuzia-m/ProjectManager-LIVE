namespace ProjectManager.Middleware
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message): base(message)
        {
            
        }
    }
}
