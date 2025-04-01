namespace VanRental.Web.Services.AI
{
    public interface IDeepSeekService
    {
        Task<string> GetResponse(string prompt);
    }
}
