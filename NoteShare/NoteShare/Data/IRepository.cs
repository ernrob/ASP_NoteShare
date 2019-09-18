using NoteShare.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShare.Data
{
    public interface IRepository
    {
        void AddGroup(Group g);

        void AddPost(Post p);       

        void AddUserToGroup(UserGroupConn u);

        void AddRequest(int gid, string uid,string groupname, string uname);

        void DeleteRequest(int gid, string uid);

        IEnumerable<Group> GetAllGroups();

        IEnumerable<Group> GetGroups(string userid);

        IEnumerable<Post> GettAllPosts(int groupid);
        byte[] GetFile(int postid);
        string GetFileType(int postid);

        void DeletePost(int postid);

        IEnumerable<UserRequest> GetRequests(string userid);
    }
}
