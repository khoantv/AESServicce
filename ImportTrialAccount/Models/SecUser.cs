using System;
using System.Collections.Generic;

#nullable disable

namespace ImportTrialAccount.Models
{
    public partial class SecUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Birthday { get; set; }
        public int? UserType { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public bool? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EmployeeId { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsFirstLogin { get; set; }
        public string CodeResetPasswordHash { get; set; }
        public DateTime? ExpiredCode { get; set; }
        public bool? IsFinishInfo { get; set; }
        public bool? IsAddEmail { get; set; }
        public string CodeAddEmail { get; set; }
        public DateTime? ExpiredCodeAddEmail { get; set; }
        public string AddEmail { get; set; }
        public bool? IsConfirmTeaching { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsSpecialTeacherUpdate { get; set; }
        public bool? IsCommentAccount { get; set; }
        public bool? IsTraining { get; set; }
        public string Position { get; set; }
    }
}
