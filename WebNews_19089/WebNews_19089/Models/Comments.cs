using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class Comments {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Content { get; set; }

        public DateTime CommentDate { get; set; }

        //---------------------------------------
        //            Foreign Keys
        //---------------------------------------

        [ForeignKey("News")]
        public int NewsFK { get; set; }
        public virtual News News { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileFK { get; set; }
        public virtual UsersProfile UserProfile { get; set; }

    }
}