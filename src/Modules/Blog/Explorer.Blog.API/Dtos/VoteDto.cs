﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class VoteDto
    {
        public bool Value { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
    }
}