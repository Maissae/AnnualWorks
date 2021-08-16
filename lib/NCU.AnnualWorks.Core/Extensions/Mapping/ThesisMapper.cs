﻿using NCU.AnnualWorks.Core.Models.DbModels;
using NCU.AnnualWorks.Core.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace NCU.AnnualWorks.Core.Extensions.Mapping
{
    public static class ThesisMapper
    {
        public static ThesisDTO ToDto(this Thesis thesis)
        {
            return new ThesisDTO
            {
                Guid = thesis.Guid,
                Title = thesis.Title,
                Abstract = thesis.Abstract,
                ThesisKeywords = thesis.ThesisKeywords.ToDto().ToList(),
                FileGuid = thesis.File.Guid,
                CreatedAt = thesis.CreatedAt,
                Grade = thesis.Grade
            };
        }

        public static ThesisDTO ToBasicDto(this Thesis thesis)
        {
            return new ThesisDTO
            {
                Guid = thesis.Guid,
                Title = thesis.Title,
                FileGuid = thesis.File.Guid,
            };
        }

        public static List<ThesisDTO> ToBasicDto(this IEnumerable<Thesis> theses)
        {
            return theses.Select(t => t.ToBasicDto()).ToList();
        }
    }
}
