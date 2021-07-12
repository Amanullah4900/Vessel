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
    public class UsersController : ApiController
    {
        private DataContext db = new DataContext();

        //Register The User

        [HttpPost]
        [Route("api/Vessel/Register")]
        public HttpResponseMessage Register(User users, HttpRequestMessage request)
        {
            if (ModelState.IsValid)
            {
                var Chek = db.Users.Where(u => u.Email == users.Email).FirstOrDefault();

                if (Chek == null)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    //return Json(new { StatusCode = "True", message = "Your Data Successfully Added", Data = users });
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Register Successfully", data = new { id = users.Id, email = users.Email, fullname = users.FullName, phonenumber = users.Phone } });

                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "This Email IS Already In Use!" });
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Check Your Model State!" });
            }
        }

        //Login User
        [HttpPost]
        [Route("api/Vessel/Login")]
        public HttpResponseMessage Login(string Email,string Password, HttpRequestMessage request)
        {
            if (Email != null && Password != null)
            {
                var Chek = db.Users.Where(u => u.Email == Email).FirstOrDefault();
                //var check = db.Users.Where(string.Compare(y => y.Password, viewmodel.Password, true());
                if (Chek == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "InValid Email" });
                }
                else
                {
                    if (Chek.Password == Password)
                    {
                        //return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Successfully Login", data = new { Email=obj.Email, id=obj.Id, name=obj.Name , point = obj.TotalPoints } });
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Successfully Login", data = new { id = Chek.Id, email = Chek.Email, fullname = Chek.FullName, phonenumber = Chek.Phone } });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid Data Password Does't Match " });
                    }

                    //return Json(new { StatusCode = "True", message = "You Successfully Login", Data = obj });
                }

                //if (ab != null)
                //{
                //    Session["UserId"] = ab.UserId.ToString();
                //    Session["UserName"] = ab.UserName.ToString();
                //    return Json("Index");
                //}

            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data CAnnot Enter Emptiy String" });
        }


        [HttpPost]
        [Route("api/Vessel/UpdateUser")]
        public HttpResponseMessage UpdateUser(User user, HttpRequestMessage request)
        {
           
                var Chek = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                if (Chek != null)
                {
                    Chek.Phone = user.Phone;
                    Chek.Email = user.Email;
                    Chek.Profile = user.Profile;
                    Chek.Bio = user.Bio;
                    Chek.BannerImage = user.BannerImage;
                    Chek.FullName = user.FullName;
                    db.SaveChanges();
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Updated Successfully"});

                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
           
        }

        [HttpPost]
        [Route("api/Vessel/ChangePassword")]
        public HttpResponseMessage changePassword(int userId,string oldPassword,string newPassword, HttpRequestMessage request)
        {
            
                var user = db.Users.SingleOrDefault(u => u.Id == userId && u.Password == oldPassword);

                if (user != null)
                {
                    user.Password = newPassword;
                    db.SaveChanges();
                    //return Json(new { StatusCode = "True", message = "Your Data Successfully Added", Data = users });
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "updated Successfully"});

                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
                }
            
        }

        [HttpPost]
        [Route("api/Vessel/ResetPassword")]
        public HttpResponseMessage ResetPassword(string email, string newPassword, HttpRequestMessage request)
        {

            var user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.Password = newPassword;
                db.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Password reset Successfully" });

            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "Invalid data" });
            }

        }

        //User Details
        [HttpPost]
        [Route("api/Vessel/UserDetails")]
        public HttpResponseMessage UserDetails(int id, HttpRequestMessage request)
        {
            if (id > 0)
            {
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                //var check = db.Users.Where(string.Compare(y => y.Password, viewmodel.Password, true());
                if (user == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "InValid Email" });
                }
                else
                {
                   return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Successfully Login", data = user });
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data CAnnot Enter Emptiy String" });
        }

        //User friends
        [HttpPost]
        [Route("api/Vessel/AddFriend")]
        public HttpResponseMessage AddFriend(int id, int friendId, HttpRequestMessage request)
        {
            if ( id > 0)
            {
                if (db.Friends.Where(m => m.FriendId == friendId && m.UserId == id ).Count() < 1)
                {
                    db.Friends.Add(new Friends { FriendId = friendId,UserId = id,Status = 0});
                    //var check = db.Users.Where(string.Compare(y => y.Password, viewmodel.Password, true());
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "False", message = "Successfuly added" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Added to friend list" });
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Already added to friend list" });
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }


        [HttpPost]
        [Route("api/Vessel/UserFriends")]
        public HttpResponseMessage UserFriends(int id, HttpRequestMessage request)
        {
            if (id > 0)
            {
                var friends = db.Friends.Where(u => u.UserId == id && u.Status == 0).Select(a => a.FriendId).ToList();
                if (friends == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "No data found" });
                }
                else
                {
                    var friendList = db.Users.Where(m => friends.Contains(m.Id)).ToList();
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Success", data = friendList });
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }

        [HttpPost]
        [Route("api/Vessel/UserBlockFriends")]
        public HttpResponseMessage UserBlockFriends(int id, HttpRequestMessage request)
        {
            if (id > 0)
            {
                var friends = db.Friends.Where(u => u.UserId == id && u.Status == 3).Select(a => a.FriendId).ToList();
                if (friends == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "No data found" });
                }
                else
                {
                    var friendList = db.Users.Where(m => friends.Contains(m.Id)).ToList();
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Success", data = friendList });
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }


        [HttpPost]
        [Route("api/Vessel/UserFriendRequest")]
        public HttpResponseMessage UserFriendRequest(int id, HttpRequestMessage request)
        {
            if (id > 0)
            {
                var friends = db.Friends.Where(u => u.UserId == id && u.Status == 0).Select(a => a.FriendId).ToList();
                if (friends == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "No data found" });
                }
                else
                {
                    var friendList = db.Users.Where(m => friends.Contains(m.Id)).ToList();
                    return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Success", data = friendList });
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }

        [HttpPost]
        [Route("api/Vessel/BlockFriend")]
        public HttpResponseMessage BlockFriend(int id,int friendId, HttpRequestMessage request)
        {
            if (friendId > 0 && id > 0)
            {
                var friend = db.Friends.Where(u => u.UserId == id && u.FriendId == friendId ).FirstOrDefault();

                if (friend == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "InValid data" });
                }
                else
                {
                    friend.Status = 3;
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully block" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "False", message = "Error to save data" });
                    }
                }

            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }

        [HttpPost]
        [Route("api/Vessel/UnBlockFriend")]
        public HttpResponseMessage UnBlockFriend(int id, int friendId, HttpRequestMessage request)
        {
            if (id > 0 && friendId > 0 )
            {
                var friend = db.Friends.FirstOrDefault(u => u.UserId == id && u.FriendId == friendId);

                if (friend == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "InValid data" });
                }
                else
                {
                    friend.Status = 2;
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully unblock" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "False", message = "Error to save data" });
                    }
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }

        [HttpPost]
        [Route("api/Vessel/ChangeFriendStatus")]
        public HttpResponseMessage ChangeFriendStatus(int id, int friendId,int status, HttpRequestMessage request)
        {
            if (id > 0 && friendId > 0)
            {
                var friend = db.Friends.Where(u => u.UserId == id && u.FriendId == friendId).FirstOrDefault();

                if (friend == null)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { status = "False", message = "InValid data" });
                }
                else
                {
                    friend.Status = status;
                    if (db.SaveChanges() > 0)
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "Successfully unblock" });
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, new { status = "False", message = "Error to save data" });
                    }
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new { statusCode = "False", message = "Invalid Data" });
        }



        [HttpPost]
        [Route("api/Vessel/UploadProfileImage")]
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
            var completePath = ConfigurationManager.AppSettings["serverUrl"] + "/PostImages/" + fileName;

            return request.CreateResponse(HttpStatusCode.OK, new { status = "True", message = "You Upload Image Successfully", data = completePath });


        }

    }
}
