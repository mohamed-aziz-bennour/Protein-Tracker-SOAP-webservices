using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace ProteinTrackerWebServices
{
    /// <summary>
    /// Summary description for ProteinTrackingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProteinTrackingService : System.Web.Services.WebService
    {
        private UserRepository repository = new UserRepository(); 

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "Add an amunt to the total. " , EnableSession = true )]
        public int AddProtein (int amount, int userId)
        {
            Thread.Sleep(3000);
            var user = repository.GetById(userId);
            if (user == null)
                return -1;
            user.Total += amount;
            repository.Save(user);
            return user.Total; 

            //******** work with sessions 
            //if (Session["user" + userId] == null)
            //    return -1;
            //var user = (User)Session["user" + userId];
            //user.Total += amount;
            //Session["user" + userId] = user;
            //return user.Total; 
        }


        [WebMethod(EnableSession = true)] 
        public int AddUser(String name, int goal)
        {
            var user = new User {Goal = goal, Name = name, Total =0 };
            repository.AddUser(user);
            return user.UserId;

            //***** using session
            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["userId"];
            //Session["user" + userId] = new User { Goal = goal, Name = name, Total = 0, UserId = userId };
            //Session["userId"] = userId + 1; 
            //return userId; 
        }

        [WebMethod(EnableSession = true)]
        public List<User> ListUsers()
        {
            return new List<User>(repository.GetAll());

            // ****** using session
            //var users = new List<User>();
            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["UserId"]; 
            //for (var i = 0; i< userId; i++)
            //{
            //    users.Add((User)Session["user" + i]); 
            //}
            //return users; 
        }

        
    }

}
