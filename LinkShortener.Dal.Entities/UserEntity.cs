using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Dal.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(name: "login", TypeName = "TEXT")]
        public string Login { get; set; }

        [Column(name: "password", TypeName = "TEXT")]
        public string Password { get; set; }

        [Required]
        public BalanceEntity Balance { get; set; }
    }
}
