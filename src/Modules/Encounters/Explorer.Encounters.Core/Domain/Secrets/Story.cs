﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Secrets
{
    public enum StoryStatus
    {
        Pending,
        Accepted,
        Declined
    }

    public class Story : Entity
    {

        public string Content { get; private set; }
        public int AuthorId { get; private set; }
        public int BookId { get; private set; }

        public string Title { get; private set; }
        public int ImageId { get; private set; }

        public StoryStatus StoryStatus { get; private set; }
        public Story()
        {
        }

        public Story(string content, int authorId, int bookId, int imageId, StoryStatus storyStatus, string title)
        {
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Invalid Title.");
            Content = content;
            AuthorId = authorId;
            BookId = bookId;
            ImageId = imageId;
            StoryStatus = storyStatus;
            Title = title;
        }

        public void Decline()
        {
            StoryStatus = StoryStatus.Declined;
        }
        public void Accept()
        {
            StoryStatus = StoryStatus.Accepted;
        }

    }
}
