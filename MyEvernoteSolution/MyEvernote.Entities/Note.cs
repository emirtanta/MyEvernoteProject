using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Notes")] //tablo adı
    public class Note:MyEntityBase
    {
        [Required,StringLength(60)]
        public string Title { get; set; }

        [Required,StringLength(2000)]
        public string Text { get; set; }

        //taslak olup olmadığı kontrolü
        public bool IsDraft { get; set; }

        //like için tanımlandı
        public int LikeCount { get; set; }

        public int CategoryId { get; set; }

        //user-not ilişkisi(1-1 ilişki
        public virtual EvernoteUser Owner { get; set; }

        //not-kategori ilişkisi (her notun bir kategorisi olduğu için bire-bir ilişki
        public virtual Category Category { get; set; }

        //not-yorum ilişkisi(bire çok ilişki
        public virtual List<Comment> Comments { get; set; }

        public List<Liked> Likes { get; set; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
