using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace WebApplication1.DAO
{
    public class Database : DataContext
    {
        public Database(): base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString())
        {
        }

        public Database(string name):base(ConfigurationManager.ConnectionStrings[name].ToString())
        {
        }
        public MEIDataDataContext data = new MEIDataDataContext();
        public List<Report> getList(){
            return(data.GetTable<Report>().ToList());
        }

        public List<Type> getTypeList()
        {
            return (data.Type.ToList());
        }

        public Boolean userInRole(String username, String role)
        {
            String userid = data.GetTable<AspNetUsers>().SingleOrDefault(item => item.UserName == username).Id;
            String roleid = data.GetTable<AspNetRoles>().SingleOrDefault(item => item.Name == role).Id;
            AspNetUserRoles asproles = data.GetTable<AspNetUserRoles>().SingleOrDefault(item => item.UserId.ToString() == userid && item.RoleId.ToString() == roleid);
            if (asproles != null)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }


    }
}