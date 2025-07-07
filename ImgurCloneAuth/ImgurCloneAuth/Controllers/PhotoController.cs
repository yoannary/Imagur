using ImgurCloneAuth.Migrations;
using ImgurCloneAuth.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgurCloneAuth.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        // inject the PhotoDBContext into the controller
        private PhotoDBContext db = new PhotoDBContext();
        private VoteDBContext dbVote = new VoteDBContext();
        private ApplicationDbContext dbUser = new ApplicationDbContext();
        private ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));


        [AllowAnonymous]
        public ActionResult Index()
        {
            IList<Photo> photos = db.Photos.ToList();

            IList<PhotoListingItemViewModel> viewModels = new List<PhotoListingItemViewModel>();

            photos.ForEach(photo => { 
                int upvotesCount = photo.Votes.Where(vote => vote.VoteType == VoteType.Upvote).Count();
                int downvotesCount = photo.Votes.Where(vote => vote.VoteType == VoteType.Downvote).Count();

                viewModels.Add(new PhotoListingItemViewModel
                (
                    photo.Id,
                    photo.Title,
                    photo.ImagePath,
                    upvotesCount,
                    downvotesCount
                ));
            });

            // reverse the list so that the newest photos are first
            viewModels.Reverse();

            return View(viewModels);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Photo")]
        public ActionResult Create(AddPhotoViewModel photo)
        {
            // check if model has image path
            if (photo.Image == null)
            {
                ViewBag.Error = "Image is required!";
                return View(photo);
            }
            // check if the model is valid
            if (ModelState.IsValid)
            {
                Photo newPhoto = Photo.create(
                    photo.Title,
                    photo.Image
                    );
                db.Photos.Add(newPhoto);
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = newPhoto.Id });
            }
            else
            {
                ViewBag.Error = "Something went wrong!";
            }

            return View(photo);
        }

        private string SavePhoto(HttpPostedFileBase Image)
        {
            string fileName = Path.GetFileName(Image.FileName);
            string extension = Path.GetExtension(fileName);

            string uniqueFileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            string filePath = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);

            Image.SaveAs(filePath);

            return uniqueFileName;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Photo/Upload")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = SavePhoto(file);
                return Json(new { success = true, name = fileName });
            }

            // If no file was uploaded or an error occurred, return an error response
            return Json(new { success = false, errorMessage = "Invalid file or file not found" });
        }

        // nullable id
        [AllowAnonymous]
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Photo photo = db.Photos.Find(id);

            if (photo == null)
            {
                return HttpNotFound();
            }

            // map votes which are upvotes and downvotes
            int upvotes = photo.Votes.Where(v => v.VoteType == VoteType.Upvote).Count();
            int downvotes = photo.Votes.Where(v => v.VoteType == VoteType.Downvote).Count();

            // filter all votes and check if the current logged in user has voted
            var user = this._userManager.FindById(User.Identity.GetUserId());
            
            // first or null
            VoteType? userVote = null;
            try
            {
                if(user != null)
                {
                    userVote = photo.Votes.Where(v => v.UserId == user.Id).Select(v => v.VoteType).First();
                }
               
            } catch (Exception)
            {
            }
            
            PhotoViewModel photoViewModel = new PhotoViewModel(photo.Id, photo.Title, photo.ImagePath, upvotes, downvotes, userVote);

            return View(photoViewModel);
        }

        [HttpPost]
        [Route("Photo/View/{id}/Upvote", Name = "PhotoUpvote")]
        public ActionResult Upvote(int? id)
        {
            // check if id is null
            if (id == null)
            {
                return HttpNotFound();
            }
            Photo photo = db.Photos.Find(id);
            // check if photo is null
            if (photo == null)
            {
                return HttpNotFound();
            }
            // get the current user
            ApplicationUser user = this._userManager.FindById(User.Identity.GetUserId());
            // check if user is null
            if (user == null)
            {
                return HttpNotFound();
            }
            // check if user has already voted
            Vote vote = db.Photos.Find(photo.Id).Votes.Where(v => v.UserId == user.Id).FirstOrDefault();
            // if user has already voted and the vote is an upvote, remove the vote
            if (vote != null && vote.VoteType == VoteType.Upvote)
            {
                db.Votes.Remove(vote);
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }
            // if user has already voted and the vote is a downvote, change the vote to an upvote
            else if (vote != null && vote.VoteType == VoteType.Downvote)
            {
                vote.VoteType = VoteType.Upvote;
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }
            // if user has not voted, create a new vote
            else
            {
                Vote newVote = Vote.create(photo.Id, user.Id, VoteType.Upvote);
                db.Votes.Add(newVote);
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }

        }

        [HttpPost]
        [Route("Photo/View/{id}/Downvote", Name = "PhotoDownvote")]
        public ActionResult Downvote(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Photo photo = db.Photos.Find(id);
            // check if photo is null
            if (photo == null)
            {
                return HttpNotFound();
            }
            // get the current user
            ApplicationUser user = this._userManager.FindById(User.Identity.GetUserId());
            // check if user is null
            if (user == null)
            {
                return HttpNotFound();
            }
            

            Vote vote = db.Photos.Find(photo.Id).Votes.Where(v => v.UserId == user.Id).FirstOrDefault();
            // if user has already voted and the vote is a downvote, remove the vote
            if (vote != null && vote.VoteType == VoteType.Downvote)
            {
                db.Votes.Remove(vote);
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }
            // if user has already voted and the vote is an upvote, change the vote to a downvote
            else if (vote != null && vote.VoteType == VoteType.Upvote)
            {
                vote.VoteType = VoteType.Downvote;
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }
            // if user has not voted, create a new vote
            else
            {
                Vote newVote = Vote.create(photo.Id, user.Id, VoteType.Downvote);
                db.Votes.Add(newVote);
                db.SaveChanges();
                return RedirectToAction("View", "Photo", new { id = photo.Id });
            }
        }
    }
}