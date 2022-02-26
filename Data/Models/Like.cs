using Microsoft.AspNetCore.Identity;
using System;

namespace MiniForum.Data.Models
{
    public class Like
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string OwnerId { get; set; }

        public string TopicId { get; set; }
    }
}
