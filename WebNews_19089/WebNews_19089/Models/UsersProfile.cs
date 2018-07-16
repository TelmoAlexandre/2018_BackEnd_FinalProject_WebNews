using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class UsersProfile {

        public UsersProfile() {
            CommentsList = new HashSet<Comments>();
            NewsList = new HashSet<News>();
        }

        [Key]
        public int ID { set; get; }

        [Required]
        [RegularExpression("^[A-Z][a-z]+(( ){1}[A-z]{1}[a-z]*)+$",
            ErrorMessage = "This field needs to start with a capital letter, needs at least two names and can't end with a space. Only letters are allowed.")]
        [StringLength(40)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string UserName { get; set; }

        // Comentários do utilizador
        public virtual ICollection<Comments> CommentsList { get; set; }

        // Notícias do utlizador (Jornalista)
        public virtual ICollection<News> NewsList { get; set; }
    }
}