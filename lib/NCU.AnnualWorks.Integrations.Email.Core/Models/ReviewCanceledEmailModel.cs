﻿namespace NCU.AnnualWorks.Integrations.Email.Core.Models
{
    public class ReviewCanceledEmailModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string ThesisTitle { get; set; }
        public string Url { get; set; }
    }
}
