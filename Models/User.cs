namespace BankingApplication.Data
{
    public partial class User
    {

        public int Id { get; set; }


        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? PinHash { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}