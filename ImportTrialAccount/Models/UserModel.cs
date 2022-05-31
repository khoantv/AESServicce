using System;

namespace ImportTrialAccount.Models
{
    public partial class UserModel
    {
        #region Properties
        //
        public long UserId { get; set; }
        //
        public string UserName { get; set; }
        //

        public string NormalizedUserName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Birthday { get; set; }

        public int UserType { get; set; }
        //
        public string Email { get; set; }
        //
        public string NormalizedEmail { get; set; }
        //
        public bool? EmailConfirmed { get; set; }
        //
        public string PasswordHash { get; set; }
        //
        public string SecurityStamp { get; set; }
        //
        public string ConcurrencyStamp { get; set; }
        //
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        //
        public bool? PhoneNumberConfirmed { get; set; }
        //
        public bool? TwoFactorEnabled { get; set; }
        //
        public DateTimeOffset? LockoutEnd { get; set; }
        //
        public bool? LockoutEnabled { get; set; }
        //
        public int? AccessFailedCount { get; set; }
        public int? RoleId { get; set; }
        public int? RoleLevel { get; set; }

        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string avatar { get; set; }
        public string Address { get; set; }
        public bool? IsFirstLogin { get; set; }
        public bool? isTraining { get; set; }
        public bool? IsNotFirstLogin { get; set; }

        public bool? IsFinishInfo { set; get; }

        public bool? IsAddEmail { set; get; }
        //check comment account
        public bool? IsCommentAccount { set; get; }
        public bool? IsLock { set; get; }
        //check teacher transfer select subject
        public bool? IsCheckSubject { set; get; }
        //check teacher finish info
        public bool? IsFinishInfoTeacher { set; get; }
        //check school denied teacher info, teacher must change school
        public bool? IsMustChangeSchool { set; get; }
        //confirm have account or not have(true is confirm, false not yet confirm)
        public bool? IsConfirmAccount { set; get; }
        //check have noti not yet(role school)
        public bool? IsCheckHaveNoti { set; get; }
        //check school is choose subject yet?(role school)
        public bool? IsCheckSubjectSchool { set; get; }
        // check teacher in class 2, 6
        public bool? IsSpecialTeacher { set; get; }
        public string Position { get; set; }

        #endregion Properties        
        #region Methods
        public bool Validate()
        {
            bool isValid = true;

            return isValid;
        }
        #endregion
    }
}
