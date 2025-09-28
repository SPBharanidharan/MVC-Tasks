using BankingApplication.Data;

namespace BankingApplication.Services
{
    public interface IAccountService
    {
        bool Register(string username, string password, string pin);
        User? Login(string username, string password);   
        bool Deposit(int userId, string pin, decimal amount);
        bool Withdraw(int userId, string pin, decimal amount);
        decimal GetBalance(int userId);
        bool ChangePin(int userId, string oldPin, string newPin);
    }
}
