using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleHub_backend.Models
{
    public class TokenClaim
    {
        public string Email { get; set; }
        public string isDeletable { get; set; }
    }
}