namespace MiniForum.Controllers
{
    using MiniForum.Data;
    using MiniForum.Data.Models;
    using MiniForum.Infrastructure;
    using MiniForum.Models.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using MiniForum.Models.Replies;

    public class RepliesController : Controller
    {
        private readonly MiniForumDbContext data;
        public RepliesController(MiniForumDbContext data)
        {
            this.data = data;
        }
        public IActionResult Replies(string topicId)
        {
            var topic = this.data.Topics.FirstOrDefault(t => t.Id == topicId);
            var repliesViewModel =
                new TopicsReplyViewModel
                {
                    Name = this.data.Topics.FirstOrDefault(t => t.Id == topicId).Name,
                    Description = this.data.Topics.FirstOrDefault(t => t.Id == topicId).Description,
                    TopicId = topicId,
                    Replies = this.data.Replies.Where(rep => rep.TopicId == topicId)
                      .Select(replies => new ReplyViewModel
                      {
                          Id = replies.Id,
                          TopicId = replies.TopicId,
                          Description = replies.Description,
                          Username = this.data.Users.FirstOrDefault(u => u.Id == replies.OwnerId).UserName
                      }).ToList()
                };

            return View(repliesViewModel);
        }
        public IActionResult Create()
        {
            // var makes = this.parts.GetMakes();

            return View();
        }

       [HttpPost]
       [Authorize]
       public IActionResult Create(UsersViewModel replyModel, string topicId)
       {
           var ownerId = this.User.GetId();



            var reply = new Reply
            {
                Description = replyModel.Description,
                OwnerId = ownerId,
                TopicId = topicId
            };

            this.data.Replies.Add(reply);
            this.data.SaveChanges();
     
           return this.Redirect($"/Replies/Replies?topicId={topicId}");
       }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteReply(string topicId, string replyId)
        {
            var reply = this.data.Replies.FirstOrDefault(r => r.Id == replyId);
           
            if (reply.OwnerId == this.User.GetId())
            {
                this.data.Replies.Remove(reply);
                this.data.SaveChanges();
            }
           
           

            return this.Redirect($"/Replies/Replies?topicId={topicId}");
        }
    }
}
