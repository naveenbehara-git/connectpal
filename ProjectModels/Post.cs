using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModels
{
    public class Post
    {
        [Key]
        public int PostId { get;set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public byte[] Image { get; set; }
        public string ImageUrl { get; set; }
        public DateTime TimeOfPost { get; set; }


    }
}
