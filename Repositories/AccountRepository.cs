using BankingApplication.Data;

namespace BankingApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingApplicationdbContext _context;

        public AccountRepository(BankingApplicationdbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public Account? GetAccountByUserId(int userId)
        {
            return _context.Accounts.FirstOrDefault(a => a.UserId == userId);
        }

        public void AddAccount(Account account)   
        {
            _context.Accounts.Add(account);
        }

        public void UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
