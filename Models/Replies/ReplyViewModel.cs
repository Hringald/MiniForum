namespace MiniForum.Models.Replies
{
    public class ReplyViewModel
    {
        public string Id { get; set; }
        public string Username { get; init; }
        public string Description { get; init; }
        public string TopicId { get; set; }
    }
}