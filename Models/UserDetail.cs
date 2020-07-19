using System;
using System.ComponentModel.DataAnnotations;

namespace DevSpace_API.Models
{
    public class UserDetail
    {
        [Key]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Location { get; set; }
        public DateTime JoinedOn { get; set; }
        public string Bio { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }
        public string StackoverflowLink { get; set; }
        public string MediumLink { get; set; }
        public string ExternalWebsite { get; set; }
    }
}