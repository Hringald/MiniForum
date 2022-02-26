using System;

namespace MiniForum.Data.Models
{
    public class Reply
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public string TopicId { get; set; }

    }
}
