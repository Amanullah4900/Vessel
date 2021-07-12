using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vessel.Models;
using Vessel.Models.DomainModels;

namespace Vessel.Controllers
{
    public class PostController : ApiController
    {
        private DataContext db = new DataContext();

        [HttpPost]
        [Route("api/Vessel/AddPost")]
        public HttpResponseMessage AddPost(Post post, HttpRequestMessage request)
        {
            if (ModelState.IsValid)
            {
                if (post != null)
                {
                    db.Posts.Add(post);
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully save"});
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Check Your Model State!" });
            }
        }

        [HttpPost]
        [Route("api/Vessel/SharePost")]
        public HttpResponseMessage SharePost(Post post, HttpRequestMessage request)
        {
            if (ModelState.IsValid)
            {
                if (post != null)
                {
                    db.Posts.Add(post);
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully save" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Check Your Model State!" });
            }
        }

        [HttpPost]
        [Route("api/Vessel/CategoryPost")]
        public HttpResponseMessage CategoryPost(int categoryId, HttpRequestMessage request)
        {
            if (ModelState.IsValid)
            {
                if (categoryId >= 0)
                {
                    var postList = db.Posts.Where(m=>m.Category == categoryId).Select(a => 
                    new 
                    {
                        id= a.Id,
                        PostedById = a.PostedById,
                        ShareById = a.ShareById,
                        Description = a.Description,
                        FileUrl = a.FileUrl,
                        FileType = a.FileType,
                        TimeStamp = a.TimeStamp,
                        Tags = a.Tags,
                        Category = a.Category,
                        Type = a.Type,
                        SharedByUser =  db.Users.FirstOrDefault(m => m.Id == a.ShareById),
                        PostedByUser = db.Users.Where(m=>m.Id == a.PostedById).FirstOrDefault(),
                        PostReviews = db.PostComments.Where(t =>t.PostId == a.Id).ToList(),
                        LikeCount = db.PostLiskes.Where(t =>t.PostId == a.Id).Count(),
                     }
                    ).ToList();
                    
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully", post = postList });
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Check Your Model State!" });
            }
        }

        [HttpPost]
        [Route("api/Vessel/AllPost")]
        public HttpResponseMessage AllPost(HttpRequestMessage request)
        {
                    var postList = db.Posts.Select(a =>
                      new
                      {
                          Id = a.Id,
                          PostedById = a.PostedById,
                          ShareById = a.ShareById,
                          Description = a.Description,
                          FileUrl = a.FileUrl,
                          FileType = a.FileType,
                          TimeStamp = a.TimeStamp,
                          Tags = a.Tags,
                          Category = a.Category,
                          Type = a.Type,
                          SharedByUser = db.Users.FirstOrDefault(m => m.Id == a.ShareById),
                          PostedByUser = db.Users.Where(m => m.Id == a.PostedById).FirstOrDefault(),
                          PostReviews = db.PostComments.Where(t => t.PostId == a.Id).ToList(),
                          LikeCount = db.PostLiskes.Where(t => t.PostId == a.Id).Count(),
                      }
                    ).ToList();

                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully", post = postList });
               
          
        }

        [HttpPost]
        [Route("api/Vessel/UserPost")]
        public HttpResponseMessage UserPost(int userId, HttpRequestMessage request)
        {
            if (userId >0)
            {
                var postList = db.Posts.Where(m => m.PostedById == userId || m.ShareById == userId).Select(a =>
                  new
                  {
                      Id = a.Id,
                      PostedById = a.PostedById,
                      ShareById = a.ShareById,
                      Description = a.Description,
                      FileUrl = a.FileUrl,
                      FileType = a.FileType,
                      TimeStamp = a.TimeStamp,
                      Tags = a.Tags,
                      Category = a.Category,
                      Type = a.Type,
                      SharedByUser = db.Users.FirstOrDefault(m => m.Id == a.ShareById),
                      PostedByUser = db.Users.Where(m => m.Id == a.PostedById).FirstOrDefault(),
                      PostReviews = db.PostComments.Where(t => t.PostId == a.Id).ToList(),
                      LikeCount = db.PostLiskes.Where(t => t.PostId == a.Id).Count(),
                  }
                ).ToList();

                return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully", post = postList });
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }
        }



        [HttpPost]
        [Route("api/Vessel/PostReviews")]
        public HttpResponseMessage PostReviews(int postId, HttpRequestMessage request)
        {
            if (postId > 0)
            {
                var postList = db.PostComments.Where(m => m.PostId == postId).Select(a =>
                  new
                  {
                      Id = a.Id,
                      PostId = a.PostId,
                      FileUrl = a.FileUrl,
                      Comment = a.Comment,
                      TimeStamp = a.TimeStamp,
                      UserId = a.UserId,
                      PostedByUser = db.Users.Where(m => m.Id == a.UserId).FirstOrDefault(),
                  }
                ).ToList();

                return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully", post = postList });
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }
        }

        [HttpPost]
        [Route("api/Vessel/AddPostReviews")]
        public HttpResponseMessage AddPostReviews(PostComment comment, HttpRequestMessage request)
        {
            if (comment != null)
            {
                db.PostComments.Add(comment);
                if (db.SaveChanges() > 0)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully" });
                }
                else {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Error to save data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }
        }


        [HttpPost]
        [Route("api/Vessel/LikePost")]
        public HttpResponseMessage AddPostLike(PostLike like, HttpRequestMessage request)
        {
            if (like != null)
            {
                db.PostLiskes.Add(like);
                if (db.SaveChanges() > 0)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Success" });
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Error to save data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }
        }

        [HttpPost]
        [Route("api/Vessel/UnLikePost")]
        public HttpResponseMessage RemovePostLike(int PostId,int UserId, HttpRequestMessage request)
        {
            if (PostId > 0 || UserId > 0)
            {
                var like  = db.PostLiskes.FirstOrDefault(m=>m.PostId == PostId && m.UserId == UserId);
                if (like!=null)
                {
                    db.PostLiskes.Remove(like);
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Success" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Error to save data" });
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }
        }

        //Upload Pic For User
        [HttpPost]
        [Route("api/Vessel/UploadPostFile")]
        public HttpResponseMessage Upload(HttpRequestMessage request)
        {
            string path = HttpContext.Current.Server.MapPath("~/PostFiles/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            //postedFile.SaveAs(path + fileName);

            string FileExtension = Path.GetExtension(postedFile.FileName);


            fileName = DateTime.Now.ToString("yyyymmddmmssfff") + "-" + FileExtension;

            postedFile.SaveAs(path + fileName);
            var completePath = ConfigurationManager.AppSettings["serverUrl"]+"/PostImages/" + fileName;

            return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Upload Image Successfully", data = completePath });


        }
    }
}
