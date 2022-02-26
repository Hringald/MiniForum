using MiniForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MiniForum.Data.Models
{
    public class PM
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Message { get; set; }
        public string OwnerId { get; set; }

        public string RecieverId { get; set; }
    }
}
