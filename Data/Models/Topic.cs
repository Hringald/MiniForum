using MiniForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MiniForum.Data.Models
{
    public class Topic
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }
        public ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }
}
