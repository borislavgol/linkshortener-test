using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Dal.Entities
{
    [Table("balance")]
    public class BalanceEntity
    {
        [Column(name: "balance", TypeName = "DECIMAL")]
        public decimal Balance { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public UserEntity Owner { get; set; }
    }
}
