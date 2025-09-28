using BankingApplication.Data;

namespace BankingApplication.Repositories
{
    public interface IAccountRepository
    {
        User? GetUserByUsername(string username);
        User? GetUserById(int id);
        void AddAccount(Account account); 
        void AddUser(User user);
        Account? GetAccountByUserId(int userId);
        void UpdateAccount(Account account);
        void Save();
    }
}
