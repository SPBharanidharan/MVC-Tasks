using System.ComponentModel.DataAnnotations;
namespace BankingApplication.Data
{
    public partial class User
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "PIN is required")]        
        public string PinHash { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}