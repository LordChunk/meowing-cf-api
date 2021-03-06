﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Models.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Data.Models
{
    [Index(nameof(Header), nameof(Value), IsUnique = true)]

    public class HttpHeader : EntityBase
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string Value { get; set; }

        [JsonIgnore]
        public virtual List<HttpRequest> HttpRequests { get; set; }
    }
}