﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class Categories {

        public Categories() {
            NewsList = new HashSet<News>();
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<News> NewsList { get; set; }

    }
}