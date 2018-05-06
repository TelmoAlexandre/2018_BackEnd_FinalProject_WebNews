using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class Photos {

        public Photos() {
            NewsList = new HashSet<News>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        // Lista das Noticias a que a foto está associada
        public virtual ICollection<News> NewsList { get; set; }
    }
}