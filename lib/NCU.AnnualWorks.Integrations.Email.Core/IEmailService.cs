﻿using NCU.AnnualWorks.Integrations.Email.Core.Models;
using System.Threading.Tasks;

namespace NCU.AnnualWorks.Integrations.Email.Core
{
    public interface IEmailService
    {
        Task SendEmailGradeConflict(GradeConflictEmailModel model);
        Task SendEmailThesisCreated(ThesisCreatedEmailModel model);
        Task SendEmailGradeCanceled(GradeCanceledEmailModel model);
        Task SendEmailReviewCanceled(ReviewCanceledEmailModel model);
        Task SendEmailGradeConfirmed(GradeConfirmedEmailModel model);
        Task SendEmailReminder(ReminderEmailModel model);
    }
}
