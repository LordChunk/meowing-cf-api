using System;
using System.ComponentModel.DataAnnotations;
using Data.Models.Common;

namespace Data.Models
{
    public abstract class HttpRequestNoHeader : EntityBase
    {
        [Required]
        public string Method { get; set; }
        public string Body { get; set; }
        public string Fetchers { get; set; }
        public bool BodyUsed { get; set; }
        public string Redirect { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int ContentLength { get; set; }
    }
}