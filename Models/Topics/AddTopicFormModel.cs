using Microsoft.AspNetCore.Identity;

namespace MiniForum.Models.Topics
{
    public class AddTopicFormModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
    }
}
