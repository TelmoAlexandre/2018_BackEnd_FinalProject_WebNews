using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class News {

        public News() {
            CommentsList = new HashSet<Comments>();
            PhotosList = new HashSet<Photos>();
            UsersProfileList = new HashSet<UsersProfile>();
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        
        public DateTime NewsDate { get; set; }

        [ForeignKey("Category")]
        public int CategoryFK { get; set; }
        public virtual Categories Category { get; set; }

        // Lista dos comentários feitos na notícia
        public virtual ICollection<Comments> CommentsList { get; set; }

        // Lista das fotos associadas com a notícia
        public virtual ICollection<Photos> PhotosList { get; set; }

        // Lista dos users que criaram a notícia
        public virtual ICollection<UsersProfile> UsersProfileList { get; set; }
    }
}