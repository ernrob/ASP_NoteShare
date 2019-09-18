using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteShare.Models;

namespace NoteShare.Data
{
    public class Repository : IRepository
    {
        ApplicationDbContext db;

        public Repository(ApplicationDbContext db)
        {
            this.db=db;
        }
        public void AddGroup(Group g)
        {
            db.Groups.Add(g);
            db.SaveChanges();
        }

        public void AddPost(Post p)
        {
            var gr = db.Groups.Where(t => t.GroupId == p.GroupId).FirstOrDefault();
            if (gr!=null)
            {
                db.Posts.Add(p);
                db.SaveChanges();
            }
        }
        
        public void AddUserToGroup(UserGroupConn u)
        {
            if ((db.Users.Where(x=> x.GroupId==u.GroupId && x.UserId==u.UserId).FirstOrDefault())==null)
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return db.Groups; ;
        }

        public IEnumerable<Group> GetGroups(string userid)
        {
            var groupsid = db.Users.Where(g => g.UserId == userid).ToList();
            List<Group> groups = new List<Group>();            
            foreach (var item in groupsid)
            {
                var group = db.Groups.Where(t => t.GroupId == item.GroupId).FirstOrDefault();
                if (group != null)
                    groups.Add(group);
            }
            var owned=db.Groups.Where(t => t.GroupOwnerId == userid).ToList();
            if (owned.Count != 0)
                owned.ForEach(k => groups.Add(k));
            return groups;
        }

        public IEnumerable<Post> GettAllPosts(int groupid)
        {

            return db.Posts.Where(y => y.GroupId == groupid).ToList();
        }

        public byte[] GetFile(int postid)
        {
            return db.Posts.Where(x => x.PostId == postid).FirstOrDefault().File;
        }

        public string GetFileType(int postid)
        {
            return db.Posts.Where(x => x.PostId == postid).FirstOrDefault().FileType;
        }

        public void DeletePost(int postid)
        {
            db.Posts.Remove(db.Posts.Where(x => x.PostId == postid).FirstOrDefault());
            db.SaveChanges();
        }

        public void AddRequest(int gid, string uid, string groupname, string uname)
        {
            if ((db.UserRequests.Where(x => x.GroupId == gid && x.UserId == uid).FirstOrDefault()) == null)
            {
                db.UserRequests.Add(new UserRequest() { GroupId = gid, UserId = uid, Groupname=groupname , UserName= uname});
                db.SaveChanges();
            }
        }

        public void DeleteRequest(int gid, string uid)
        {
            var d = db.UserRequests.Where(x => x.GroupId == gid && x.UserId == uid).FirstOrDefault();
            if (d != null)
            {
                db.UserRequests.Remove(d);
                db.SaveChanges();
            }
        }

        public IEnumerable<UserRequest> GetRequests(string userid)
        {
            var ownedGroups = db.Groups.Where(g => g.GroupOwnerId == userid).ToList();
            List<UserRequest> req = new List<UserRequest>();
            foreach (var item in ownedGroups)
            {
                var t = db.UserRequests.Where(x => x.GroupId == item.GroupId).FirstOrDefault();
                if (t != null)
                    req.Add(t);
            }
            return req;
        }
    }
}
