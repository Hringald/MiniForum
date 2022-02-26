using MiniForum.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MiniForum.Models.Topics;

namespace MiniForum.Data
{
    public class MiniForumDbContext : IdentityDbContext
    {
        public MiniForumDbContext(DbContextOptions<MiniForumDbContext> options)
            : base(options)
        {
        }
        public DbSet<Like> Likes { get; set; }
        public DbSet<PM> PersonalMassages { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<MiniForum.Models.Topics.TopicsViewModel> TopicsViewModel { get; set; }
    }
}
