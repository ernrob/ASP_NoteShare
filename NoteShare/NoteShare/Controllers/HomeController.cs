using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Data;
using NoteShare.Models;

namespace NoteShare.Controllers
{   
    //TODO A posts oldalról új bejegyzés hozzáadásánál nem megy át a Groupid
    public class HomeController : Controller
    {        
        UserManager<IdentityUser> usermanager;
        IRepository repo;

        public HomeController(UserManager<IdentityUser> usermanager, ApplicationDbContext db)
        {
            this.usermanager = usermanager;            
            repo = new Repository(db);          
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddGroup()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddGroup(Group g)
        {
            g.GroupOwnerId = usermanager.GetUserId(this.User);
            g.GroupOwner = usermanager.GetUserName(this.User);
            repo.AddGroup(g);
            return RedirectToAction("Group");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AllGroups()
        {
            var groups = repo.GetAllGroups();
            ViewBag.UserId =usermanager.GetUserId(this.User);
            ViewBag.Name = usermanager.GetUserName(this.User);
            return View(groups);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Group()
        {
            var myself = this.User;
            var myid = usermanager.GetUserId(myself);
            var groups = repo.GetGroups(myid);
            ViewBag.UserId = myid;
            return View(groups);
        }

        [Authorize]        
        public IActionResult UserRequest(int id,string uid, string groupname,string uname)
        {
            repo.AddRequest(id,uid,groupname,uname);
            return RedirectToAction("AllGroups");
        }

        [Authorize]
        public IActionResult Requests()
        {
            string uid = usermanager.GetUserId(this.User);
            ViewBag.UserId = uid;
            var req=repo.GetRequests(uid);
            return View(req);
        }

        public IActionResult Posts(int id, bool owner)
        {
            ViewBag.Groupid = id;
            ViewBag.Owner =owner;
            var list = repo.GettAllPosts(id);
            if (list != null)
                return View(list);
            else
                return RedirectToAction("Index");
            
        }

        [HttpGet]
        public IActionResult DeletePost(int id, int gid)
        {
            repo.DeletePost(id);
            return RedirectToAction("Posts",gid);
        }

        [HttpGet]
        public IActionResult AddPost(int id)
        {
            ViewBag.Groupid = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            if (post.FileUpload != null)
            {
                byte[] data = new byte[post.FileUpload.Length];
                using (var stream = post.FileUpload.OpenReadStream())
                {
                    stream.Read(data, 0, (int)post.FileUpload.Length);
                }                
                post.File = data;
                post.FileType = post.FileUpload.ContentType;
            }
            post.CreationDate = DateTime.Now;
            post.PostCreator= usermanager.GetUserName(this.User);
            repo.AddPost(post);

            return RedirectToAction("Group");
        }

        [HttpGet]
        public FileResult DonwloadFile(int id)
        {
            var content = repo.GetFile(id);
            var type = repo.GetFileType(id);
            return File(content,type);
        }

        public IActionResult UsersList(int id)
        {
            ViewBag.Groupid = id;
            Dictionary<string, string> users = new Dictionary<string, string>();
            usermanager.Users.ToList().ForEach(i=> users.Add(i.Id,i.UserName));
           
            return View(users);
        }

        public IActionResult AddUserToGroup(string usid, int grid)
        {
            UserGroupConn u = new UserGroupConn() { UserId = usid,GroupId=grid };
            repo.AddUserToGroup(u);
            repo.DeleteRequest(grid, usid);
            return RedirectToAction("UsersList", grid);
        }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
