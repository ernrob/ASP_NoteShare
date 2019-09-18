using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShare.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string PostName { get; set; }

        public string PostCreator { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }

        public int GroupId { get; set; }

        public byte[] File { get; set; }

        [NotMapped]
        public IFormFile FileUpload { get; set; }

        public string FileType { get; set; }

    }
}
