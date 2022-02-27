namespace MiniForum.Controllers
{
    using MiniForum.Data;
    using MiniForum.Data.Models;
    using MiniForum.Infrastructure;
    using MiniForum.Models.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class TopicsController : Controller
    {
        private readonly MiniForumDbContext data;
        public TopicsController(MiniForumDbContext data)
        {
            this.data = data;
        }
        public IActionResult Topics()
        {
            var topicsViewModel = this.data.Topics
                .Select(t => new TopicsViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    OwnerName = this.data.Users.FirstOrDefault(u => u.Id == t.OwnerId).UserName,
                    Likes = t.Likes.Count()
                }).ToList();

            return View(topicsViewModel);
        }
        public IActionResult Create()
        {
            // var makes = this.parts.GetMakes();

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddTopicFormModel topicModel)
        {
            var ownerId = this.User.GetId();

            var topic = new Topic
            {
                Name = topicModel.Name,
                Description = topicModel.Description,
                OwnerId = ownerId
            };

            this.data.Topics.Add(topic);
            this.data.SaveChanges();

            return this.Redirect("/Topics/Topics");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Like(string topicId)
        {
            var topic = this.data.Topics.FirstOrDefault(t => t.Id == topicId);
            if (topic.OwnerId == this.User.GetId() || this.data.Likes.Any(t => t.TopicId == topicId && t.OwnerId == this.User.GetId()))
            {
                return this.Redirect("/Topics/Topics");
            }

            var like = new Like
            {
                OwnerId = this.User.GetId(),
                TopicId = topicId
            };
            this.data.Likes.Add(like);
            this.data.Topics.FirstOrDefault(t => t.Id == topicId).Likes.Add(like);
            this.data.SaveChanges();

            return this.Redirect("/Topics/Topics");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Dislike(string topicId)
        {
            var topic = this.data.Topics.FirstOrDefault(t => t.Id == topicId);
            if (topic.OwnerId == this.User.GetId())
            {
                return this.Redirect("/Topics/Topics");
            }

            if (this.data.Likes.FirstOrDefault(l => l.OwnerId == this.User.GetId()) == null)
            {
                return this.Redirect("/Topics/Topics");
            }

            var like = this.data.Likes.FirstOrDefault(l => l.OwnerId == this.User.GetId());

            this.data.Likes.Remove(like);
            this.data.Topics.FirstOrDefault(t => t.Id == topicId).Likes.Remove(like);
            this.data.SaveChanges();

            return this.Redirect("/Topics/Topics");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteTopic(string topicId)
        {
            var topic = this.data.Topics.FirstOrDefault(t => t.Id == topicId);

            if (topic.OwnerId == this.User.GetId())
            {
                var topicReplies = this.data.Replies.Where(r=>r.TopicId==topicId);

                foreach (var reply in topicReplies)
                {
                    this.data.Replies.Remove(reply);
                }
                this.data.SaveChanges();

                this.data.Topics.Remove(topic);
                this.data.SaveChanges();

                return this.Redirect("/Topics/Topics");
            }

            return this.Redirect("/Topics/Topics");
        }
    }
}
