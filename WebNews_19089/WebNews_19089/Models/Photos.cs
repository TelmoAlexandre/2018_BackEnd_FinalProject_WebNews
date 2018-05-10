using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class Photos {
        

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [ForeignKey("News")]
        public int NewsFK { get; set; }
        public virtual News News { get; set; }
    }
}