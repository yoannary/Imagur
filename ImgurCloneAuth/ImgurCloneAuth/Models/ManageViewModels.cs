using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ImgurCloneAuth.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class AddPhotoViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title must be between 3 and 50 characters.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [DataType(DataType.Upload)]
        public string Image { get; set; }
        /*public HttpPostedFileBase Image { get; set; }*/
    }

    public class PhotoViewModel
    {
        public int PhotoID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public VoteType? UserVoteType { get; set; }

        public PhotoViewModel(int PhotoId, string Title, string ImagePath, int upvotes = 0, int downvotes = 0, VoteType? userVoteType = null)
        {
            this.PhotoID = PhotoId;
            this.Title = Title;
            this.ImagePath = ImagePath;
            this.Upvotes = upvotes;
            this.Downvotes = downvotes;
            this.UserVoteType = userVoteType;
        }
    }

    public class PhotoListingItemViewModel
    {
        public int PhotoID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public PhotoListingItemViewModel(int PhotoId, string Title, string ImagePath, int upvotes = 0, int downvotes = 0)
        {
            this.PhotoID = PhotoId;
            this.Title = Title;
            this.ImagePath = ImagePath;
            this.Upvotes = upvotes;
            this.Downvotes = downvotes;
        }
    }
}