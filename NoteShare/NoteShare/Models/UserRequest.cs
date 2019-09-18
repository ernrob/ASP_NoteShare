using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShare.Models
{
    public class UserRequest
    {
        [Key]
        public int ConnId { get; set; }

        public string UserId { get; set; }

        public int GroupId { get; set; }

        public string Groupname { get; set; }

        public string UserName { get; set; }
    }
}
