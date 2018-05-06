﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime NewsDate { get; set; }

        // Lista dos comentários feitos na notícia
        public ICollection<Comments> CommentsList { get; set; }

        // Lista das fotos associadas com a notícia
        public ICollection<Photos> PhotosList { get; set; }

        // Lista dos users que criaram a notícia
        public ICollection<UsersProfile> UsersProfileList { get; set; }
    }
}