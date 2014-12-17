using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DAO
{
    public class DAO
    {
        public Type[] getArrayOfTypes()
        {
            return (new Database().GetTable<Type>().ToArray());
        }

        public Status[] getArrayOfStatus()
        {
            return (new Database().GetTable<Status>().ToArray());
        }

        public AspNetRoles[] getArrayOfRoles()
        {
            return (new Database().GetTable<AspNetRoles>().ToArray());
        }

        public School[] getSchoolList()
        {
            return (new Database().GetTable<School>().ToArray());
        }

        public AspNetUsers[] getUserList()
        {
            return (new Database().GetTable<AspNetUsers>().ToArray());
        }

        public AspNetRoles[] getRoleList()
        {
            return (new Database().GetTable<AspNetRoles>().ToArray());
        }

        public IEnumerable<AspUserInSchool> getUserSchoolList(string p)
        {
            return (new Database().GetTable<AspUserInSchool>().Where(x => x.User_id == p));
        }

        public IEnumerable<AspNetUserRoles> getUserRoleList(string p)
        {
            return (new Database().GetTable<AspNetUserRoles>().Where(x => x.UserId == p));
        }
    }
}