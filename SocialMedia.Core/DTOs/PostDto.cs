﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public DateTime? Date { get; set; }
        //[Required] -- se agrega para utilizar dataanotations, para validaciones
        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
