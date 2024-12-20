﻿using System.Reflection;

namespace Skillshare.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public string FullName { get; set; }

        public string? Headline { get; set; }

        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }

        public string? WebsiteUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? YoutubeUrl { get; set; }

        public bool isValidProfile()
        {
            if (FullName is null || Headline is null || Biography is null || ImageUrl is null || LinkedInUrl is null)
                return false;

            return true;
        }
    }
}