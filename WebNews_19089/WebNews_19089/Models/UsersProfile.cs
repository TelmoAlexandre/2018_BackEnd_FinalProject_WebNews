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

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Bio { get; set; }

        public string UserName { get; set; }

        // Comentários do utilizador
        public virtual ICollection<Comments> CommentsList { get; set; }

        // Notícias do utlizador (Jornalista)
        public virtual ICollection<News> NewsList { get; set; }
    }
}