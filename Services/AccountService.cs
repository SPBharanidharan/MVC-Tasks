using BankingApplication.Helpers;
using BankingApplication.Data;
using BankingApplication.Repositories;


namespace BankingApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public bool Register(string username, string password, string pin)
        {
            if (_repo.GetUserByUsername(username) != null) return false;

            var user = new User
            {
                Username = username,
                PasswordHash = EncryptionHelper.HashPassword(password),
                PinHash = EncryptionHelper.HashPassword(pin)
            };

            _repo.AddUser(user);
            _repo.Save();

            var account = new Account { UserId = user.Id, Balance = 0 };
            _repo.AddAccount(account);   
            _repo.Save();

            return true;
        }

        public User? Login(string username, string password)  
        {
            var user = _repo.GetUserByUsername(username);
            if (user != null && EncryptionHelper.VerifyPassword(password, user.PasswordHash))
                return user;

            return null;
        }

        public bool Deposit(int userId, string pin, decimal amount)
        {
            var user = _repo.GetUserById(userId);
            var account = _repo.GetAccountByUserId(userId);

            if (user == null || account == null) return false;
            if (!EncryptionHelper.VerifyPassword(pin, user.PinHash)) return false;

            account.Balance += amount;
            _repo.UpdateAccount(account);
            _repo.Save();
            return true;
        }

        public bool Withdraw(int userId, string pin, decimal amount)
        {
            var user = _repo.GetUserById(userId);
            var account = _repo.GetAccountByUserId(userId);

            if (user == null || account == null) return false;
            if (!EncryptionHelper.VerifyPassword(pin, user.PinHash)) return false;
            if (account.Balance < amount) return false;

            account.Balance -= amount;
            _repo.UpdateAccount(account);
            _repo.Save();
            return true;
        }

        public decimal GetBalance(int userId)
        {
            var account = _repo.GetAccountByUserId(userId);
            return account?.Balance ?? 0;
        }

        public bool ChangePin(int userId, string oldPin, string newPin)
        {
            var user = _repo.GetUserById(userId);
            if (user == null) return false;
            if (!EncryptionHelper.VerifyPassword(oldPin, user.PinHash)) return false;

            user.PinHash = EncryptionHelper.HashPassword(newPin);
            _repo.Save();
            return true;
        }
    }
}
