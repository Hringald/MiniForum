using System.Collections.Generic;

namespace MiniForum.Models.Replies
{
    public class TopicsReplyViewModel
    {
        public string TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ReplyViewModel> Replies { get; set; }
    }
}