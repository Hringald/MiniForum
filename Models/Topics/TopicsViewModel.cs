using Microsoft.AspNetCore.Identity;

namespace MiniForum.Models.Topics
{
    public class TopicsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public int Likes { get; set; }
    }
}
