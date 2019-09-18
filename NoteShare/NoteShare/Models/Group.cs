using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShare.Models
{
    public class Group
    {
        public string GroupName{get; set;}

        [Key]
        public int GroupId { get; set; }

        public string GroupOwner { get; set; }

        public string GroupOwnerId { get; set; }
        public string Text { get; set; }

        public List<Post> Posts { get; set; }

        public Group()
        {            
            Posts = new List<Post>();
        }
    }
}
