﻿using Microsoft.Extensions.Options;
using NCU.AnnualWorks.Authentication.JWT.Core.Abstractions;
using NCU.AnnualWorks.Core.Extensions.Mapping;
using NCU.AnnualWorks.Core.Models.DbModels;
using NCU.AnnualWorks.Core.Models.Dto;
using NCU.AnnualWorks.Core.Models.Dto.Thesis;
using NCU.AnnualWorks.Core.Models.Enums;
using NCU.AnnualWorks.Core.Options;
using NCU.AnnualWorks.Core.Repositories;
using NCU.AnnualWorks.Core.Services;
using NCU.AnnualWorks.Integrations.Email.Core;
using NCU.AnnualWorks.Integrations.Email.Core.Models;
using NCU.AnnualWorks.Integrations.Usos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCU.AnnualWorks.Services
{
    public class ThesisService : IThesisService
    {
        private readonly ApplicationOptions _options;

        private readonly IThesisRepository _thesisRepository;
        private readonly IUsosService _usosService;
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ThesisService(IThesisRepository thesisRepository, IUsosService usosService,
            IUserContext userContext, IUserRepository userRepository,
            IOptions<ApplicationOptions> options, IEmailService emailService)
        {
            _thesisRepository = thesisRepository;
            _usosService = usosService;
            _userContext = userContext;
            _userRepository = userRepository;
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task<ThesisActionsDTO> GetAvailableActions(Thesis thesis, DateTime deadline)
        {
            var currentTerm = await _usosService.GetCurrentAcademicYear();
            var currentUser = _userContext.CurrentUser;
            var isAuthor = thesis.ThesisAuthors.Any(a => a.AuthorId == currentUser.Id);
            var isPromoter = thesis.Promoter.Id == currentUser.Id;
            var isReviewer = thesis.Reviewer.Id == currentUser.Id;
            var promoterReview = thesis.Reviews.FirstOrDefault(r => r.CreatedBy == thesis.Promoter);
            var reviewerReview = thesis.Reviews.FirstOrDefault(r => r.CreatedBy == thesis.Reviewer);
            var userReview = thesis.Reviews.FirstOrDefault(r => r.CreatedBy.Id == currentUser.Id);
            var userReviewExists = userReview != null;
            var isPastDeadline = deadline < DateTime.Now;
            var thesisTerm = thesis.TermId;

            return new ThesisActionsDTO
            {
                CanView = isAuthor || currentUser.IsEmployee,
                CanDownload = isAuthor || currentUser.IsEmployee,
                CanPrint = isAuthor || currentUser.IsEmployee,
                CanAddReview = currentUser.IsEmployee &&
                    (isPromoter || isReviewer) &&
                    !userReviewExists &&
                    !isPastDeadline &&
                    currentTerm.Id == thesisTerm,
                CanEditReview = currentUser.IsEmployee &&
                    (isPromoter || isReviewer) &&
                    userReviewExists &&
                    !userReview.IsConfirmed &&
                    !isPastDeadline &&
                    currentTerm.Id == thesisTerm,
                CanEdit = (!isPastDeadline && currentUser.IsAdmin && thesis.Grade == null && currentTerm.Id == thesisTerm) ||
                    (
                        currentUser.IsEmployee &&
                        !isPastDeadline &&
                        isPromoter &&
                        thesis.Grade == null &&
                        (promoterReview == null || !promoterReview.IsConfirmed) &&
                        (reviewerReview == null || !reviewerReview.IsConfirmed) &&
                        currentTerm.Id == thesisTerm
                    ),
                CanEditGrade = currentUser.IsEmployee &&
                    isPromoter &&
                    thesis.Grade == null &&
                    promoterReview != null &&
                    reviewerReview != null &&
                    promoterReview.IsConfirmed &&
                    reviewerReview.IsConfirmed &&
                    !isPastDeadline &&
                    currentTerm.Id == thesisTerm,
                CanHide = currentUser.IsAdmin && thesis.Grade == null && !thesis.Hidden,
                CanUnhide = currentUser.IsAdmin && thesis.Hidden,
                CanCancelGrade = currentUser.IsAdmin && !string.IsNullOrEmpty(thesis.Grade) && !isPastDeadline && currentTerm.Id == thesisTerm,
                CanCancelPromoterReview = currentUser.IsAdmin &&
                    promoterReview != null &&
                    promoterReview.IsConfirmed &&
                    !isPastDeadline &&
                    currentTerm.Id == thesisTerm,
                CanCancelReviewerReview = currentUser.IsAdmin &&
                    reviewerReview != null &&
                    reviewerReview.IsConfirmed &&
                    !isPastDeadline &&
                    currentTerm.Id == thesisTerm,
                CanAddAdditionalFiles = isPromoter && !isPastDeadline && string.IsNullOrEmpty(thesis.Grade)
            };
        }

        public async Task<ThesisActionsDTO> GetAvailableActions(Guid thesisGuid, DateTime deadline)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            return await GetAvailableActions(thesis, deadline);
        }

        public async Task IncludeAvailableActions(IEnumerable<ThesisDTO> theses, DateTime deadline)
        {
            foreach (var thesis in theses)
            {
                thesis.Actions = await GetAvailableActions(thesis.Guid, deadline);
            }
        }

        private IQueryable<Thesis> GetThesesForUser(string termId)
        {
            var query = _thesisRepository.GetAll().Where(t => t.TermId == termId);
            if (!_userContext.CurrentUser.IsAdmin)
            {
                query.Where(t => !t.Hidden);
            }
            return query;
        }


        public List<ThesisDTO> GetThesesByTerm(string termId) =>
            GetThesesForUser(termId).ToBasicDto();

        public List<ThesisDTO> GetPromotedTheses(long userId, string termId) =>
            GetThesesForUser(termId).Where(t => t.Promoter.Id == userId).ToBasicDto();

        public List<ThesisDTO> GetReviewedTheses(long userId, string termId) =>
            GetThesesForUser(termId).Where(t => t.Reviewer.Id == userId).ToBasicDto();

        public List<ThesisDTO> GetAuthoredTheses(long userId, string termId) =>
            GetThesesForUser(termId).Where(t => t.ThesisAuthors.Select(a => a.AuthorId).Contains(userId)).ToBasicDto();

        public async Task<Guid?> GetReviewGuid(Guid thesisGuid, long userId) =>
            (await _thesisRepository.GetAsync(thesisGuid))?.Reviews?.FirstOrDefault(r => r.CreatedBy.Id == userId)?.Guid;

        public bool ThesisExists(Guid thesisGuid) => _thesisRepository.GetAll().Any(p => p.Guid == thesisGuid);

        public async Task<List<ThesisLogDTO>> GetThesisLogs(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var usosUsersFromLogs = await _usosService.GetUsers(_userContext.GetCredentials(), thesis.ThesisLogs.Select(p => p.User.UsosId).Distinct());
            var usersFromLogs = usosUsersFromLogs.ToDto();

            return thesis.ThesisLogs.Select(p => new ThesisLogDTO
            {
                Timestamp = p.Timestamp,
                ModificationType = p.ModificationType,
                User = usersFromLogs.FirstOrDefault(u => u.UsosId == p.User.UsosId)
            }).ToList();
        }

        public async Task<List<string>> GetAvailableGrades(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var grades = new List<string>() { "2", "3", "3.5", "4", "4.5", "5" };
            var promoterGrade = thesis.Reviews.FirstOrDefault(r => r.CreatedBy == thesis.Promoter && r.IsConfirmed)?.Grade;
            var reviewerGrade = thesis.Reviews.FirstOrDefault(r => r.CreatedBy == thesis.Reviewer && r.IsConfirmed)?.Grade;

            if (string.IsNullOrEmpty(promoterGrade) || string.IsNullOrEmpty(reviewerGrade))
            {
                return null;
            }

            var promoterGradeIndex = grades.IndexOf(promoterGrade);
            var reviewerGradeIndex = grades.IndexOf(reviewerGrade);

            var start = promoterGradeIndex > reviewerGradeIndex ? reviewerGradeIndex : promoterGradeIndex;
            var end = promoterGradeIndex > reviewerGradeIndex ? promoterGradeIndex : reviewerGradeIndex;

            return grades.GetRange(start, end - start + 1);
        }

        public async Task CancelGrade(Guid thesisGuid)
        {
            var currentUser = await _userRepository.GetAsync(_userContext.CurrentUser.Id);
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            thesis.Grade = null;
            thesis.LogChange(currentUser, ModificationType.GradeCanceled);
            await _thesisRepository.UpdateAsync(thesis);
            await SendEmailGradeCanceled(thesisGuid);
        }

        public async Task<bool> HasGrade(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            return !string.IsNullOrEmpty(thesis.Grade);
        }

        public async Task<string> GetThesisTermId(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            return thesis.TermId;
        }

        public async Task SendEmailThesisCreated(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var reviewer = await _usosService.GetUser(_userContext.GetCredentials(), thesis.Reviewer.UsosId);
            var authors = await _usosService.GetUsers(_userContext.GetCredentials(), thesis.ThesisAuthors.Select(a => a.AuthorId.ToString()));
            await _emailService.SendEmailThesisCreated(new ThesisCreatedEmailModel
            {
                Url = $"{ _options.ApplicationUrl}/details/{thesisGuid}",
                ThesisTitle = thesis.Title,
                ReviewerId = thesis.Reviewer.Id,
                ReviewerEmail = reviewer.Email ?? thesis.Reviewer.Email,
                AuthorIds = thesis.ThesisAuthors.Select(a => a.AuthorId).ToList(),
                AuthorEmails = thesis.ThesisAuthors
                    .Select(a => authors.FirstOrDefault(ua => ua.Id == a.AuthorId.ToString())?.Email ?? a.Author.Email).ToList()
            });
        }

        public async Task SendEmailGradeConflict(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var user = await _usosService.GetUser(_userContext.GetCredentials(), thesis.Promoter.UsosId);
            await _emailService.SendEmailGradeConflict(new GradeConflictEmailModel
            {
                UserId = thesis.Promoter.Id,
                Email = user.Email ?? thesis.Promoter.Email,
                ThesisTitle = thesis.Title,
                Url = $"{ _options.ApplicationUrl}/details/{thesisGuid}",
            });
        }

        public async Task SendEmailGradeCanceled(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var user = await _usosService.GetUser(_userContext.GetCredentials(), thesis.Promoter.UsosId);
            await _emailService.SendEmailGradeCanceled(new GradeCanceledEmailModel
            {
                UserId = thesis.Promoter.Id,
                Email = user.Email ?? thesis.Promoter.Email,
                ThesisTitle = thesis.Title,
                Url = $"{_options.ApplicationUrl}/details/{thesisGuid}"
            });
        }

        public async Task SendEmailGradeConfirmed(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);
            var thesisUsers = new List<User>();
            thesisUsers.Add(thesis.Promoter);
            thesisUsers.Add(thesis.Reviewer);
            thesisUsers.AddRange(thesis.ThesisAuthors.Select(a => a.Author));

            var users = await _usosService.GetUsers(_userContext.GetCredentials(), thesisUsers.Select(u => u.UsosId));
            await _emailService.SendEmailGradeConfirmed(new GradeConfirmedEmailModel
            {
                UserIds = thesisUsers.Select(u => u.Id).ToList(),
                Emails = users.Select(u => u.Email ?? thesisUsers.FirstOrDefault(tu => tu.UsosId == u.Id)?.Email).ToList(),
                ThesisTitle = thesis.Title,
                Url = $"{_options.ApplicationUrl}/details/{thesisGuid}"
            });
        }

        public async Task<List<ThesisFileDTO>> GetThesisFiles(Guid thesisGuid)
        {
            var thesis = await _thesisRepository.GetAsync(thesisGuid);

            var result = new List<ThesisFileDTO>();
            result.AddRange(thesis.ThesisAdditionalFiles.Select(f => MapThesisFileToDto(f.Thesis, f.File)));
            result.Add(MapThesisFileToDto(thesis, thesis.File));

            return result;
        }

        private ThesisFileDTO MapThesisFileToDto(Thesis thesis, File file)
        {
            return new ThesisFileDTO
            {
                Guid = file.Guid,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Extension = file.Extension,
                Size = file.Size,
                Actions = new ThesisFileActionsDTO
                {
                    CanDelete = string.IsNullOrEmpty(thesis.Grade) &&
                        thesis.FileId != file.Id &&
                        (thesis.Promoter.Id == _userContext.CurrentUser.Id || _userContext.CurrentUser.IsAdmin),
                    CanDownload = thesis.ThesisAuthors.Select(a => a.AuthorId).Contains(_userContext.CurrentUser.Id) ||
                        _userContext.CurrentUser.IsEmployee
                }
            };
        }

        public bool IsAuthor(Guid thesisGuid)
        {
            return _thesisRepository.GetAll()
                .Any(t => t.Guid == thesisGuid &&
                    t.ThesisAuthors
                    .Select(a => a.AuthorId)
                    .Contains(_userContext.CurrentUser.Id));
        }

        public bool IsPromoter(Guid thesisGuid)
        {
            return _thesisRepository.GetAll()
                .Any(t => t.Guid == thesisGuid && t.Promoter.Id == _userContext.CurrentUser.Id);
        }

        public bool IsPromoter(User user)
        {
            return _thesisRepository.GetAll().Any(t => t.Promoter == user);
        }

        public bool IsAuthor(User user)
        {
            return _thesisRepository.GetAll().Any(t => t.ThesisAuthors.Select(a => a.Author).Contains(user));
        }
    }
}
