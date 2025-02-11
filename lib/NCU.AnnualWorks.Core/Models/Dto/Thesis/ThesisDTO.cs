﻿using NCU.AnnualWorks.Core.Models.Dto.Keywords;
using NCU.AnnualWorks.Core.Models.Dto.Reviews;
using NCU.AnnualWorks.Core.Models.Dto.Thesis;
using NCU.AnnualWorks.Core.Models.Dto.Users;
using System;
using System.Collections.Generic;

namespace NCU.AnnualWorks.Core.Models.Dto
{
    public class ThesisDTO
    {
        public Guid Guid { get; set; }
        public Guid? ReviewGuid { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Grade { get; set; }
        public List<string> AvailableGradeRange { get; set; }
        public bool Hidden { get; set; }
        public string TermId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO Promoter { get; set; }
        public UserDTO Reviewer { get; set; }
        public ReviewDTO PromoterReview { get; set; }
        public ReviewDTO ReviewerReview { get; set; }
        public List<UserDTO> ThesisAuthors { get; set; }
        public List<KeywordDTO> ThesisKeywords { get; set; }
        public ThesisActionsDTO Actions { get; set; }
        public Guid FileGuid { get; set; }
        public ThesisFileDTO File { get; set; }
        public List<ThesisFileDTO> ThesisAdditionalFiles { get; set; }
        public List<ThesisLogDTO> ThesisLogs { get; set; }
    }
}
