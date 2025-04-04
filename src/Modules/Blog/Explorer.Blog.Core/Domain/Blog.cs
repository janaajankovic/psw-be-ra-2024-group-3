﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed,
        Active,
        Famous
    }

    //[Table("Blogs", Schema = "blog")] 

    public class Blogs : Entity
    {
        //[ForeignKey("User")]
        public long AuthorId { get; private set; }
        //public List<Comment>? Comments { get; private set; }
        public List<Vote>? Votes { get; private set; }
        //public User User { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int ImageId { get; private set; }
        public BlogStatus Status { get; set; }

        public Blogs(long authorId, string title, string description, DateTime creationDate, BlogStatus status, int imageId = -1)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            AuthorId = authorId;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            ImageId = imageId;
            Status = status;
        }
        public Blogs(long authorId, List<Vote>? votes, string title, string description, DateTime creationDate, BlogStatus status, int imageId = -1)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            AuthorId = authorId;
            Votes = votes;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            ImageId = imageId;
            Status = status;
        }

        public void AddVote(Vote newVote)
        {
            if (Votes == null)
                Votes = new List<Vote>();

            var vote = Votes.FirstOrDefault(v => v?.AuthorId == newVote.AuthorId);

            if (vote == null)
                Votes.Add(newVote);
            else
            {
                vote.CreationDate = newVote.CreationDate;
                vote.Value = newVote.Value;
            }
        }

        public void RemoveVote(long authorId)
        {
            if (Votes == null || !Votes.Any())
                throw new InvalidOperationException("No votes available to remove.");

            var vote = Votes.FirstOrDefault(v => v?.AuthorId == authorId);

            if (vote == null)
                throw new ArgumentException($"Vote by author with ID {authorId} does not exist.");

            Votes.Remove(vote);
        }

        public List<Vote> GetAllVotes()
        {
            return Votes ?? new List<Vote>();
        }
    }
}
