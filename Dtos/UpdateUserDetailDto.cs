using System;

namespace DevSpace_API.Dtos
{
    public class UpdateUserDetailDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }
        public string StackoverflowLink { get; set; }
        public string MediumLink { get; set; }
        public string ExternalWebsite { get; set; }
    }
}