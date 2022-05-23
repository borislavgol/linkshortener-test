using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Dal.Entities
{
    [Table("shorted_links")]
    public class ShortedLinkEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public UserEntity Owner { get; set; }

        [Column(name: "original_link", TypeName = "TEXT")]
        public string OriginalLink { get; set; }

        [Column(name: "short_link", TypeName = "TEXT")]
        public string ShortLink { get; set; }
    }
}
