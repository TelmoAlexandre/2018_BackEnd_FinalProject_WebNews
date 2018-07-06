using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models
{
    public class NewsWithPageModelView
    {
        public virtual ICollection<News> News { get; set; }
        public int pageNum { get; set; }
        public bool lastPage { get; set; }
        public string category { get; set; }
        public bool firstPage { get; set; }
    }
}