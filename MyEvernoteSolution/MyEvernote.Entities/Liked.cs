using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Likes")] //tablo adı
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary key otomatik artan
        public int Id { get; set; }
        //çoka çok ilişki(1 notu çok kişi beğenebilir;bir beğeniyi çok kişi beğenir
        public Note Note { get; set; }
        public EvernoteUser LikedUser { get; set; }
    }
}
