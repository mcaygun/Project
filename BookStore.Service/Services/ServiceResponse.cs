
namespace BookStore.Service.Services
{
    public class ServiceResponse
    {
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; }

        public void AddErrorMessage(string message)
        {
            HasError = true;
            ErrorMessage = message;
        }
    }
}
