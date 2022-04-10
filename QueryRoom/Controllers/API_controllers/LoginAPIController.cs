using QueryRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QueryRoom.Controllers.API_controllers
{
    public class LoginAPIController : ApiController
    {
        QueryRoomDBEntities db = new QueryRoomDBEntities();
        public IHttpActionResult PostLogin(LoginData obj)
        {
            var user = db.TBL_SIGNUP.ToList().Find(x=>x.USERNAME== obj.USERNAME);
            var userRole = db.TBL_USERROLE.ToList().Find(x=>x.USERNAME==user.USERNAME);
            if (user!=null && user.PASSWORD==Crypto.Hash(obj.PASSWORD))
            {
                return Ok(userRole.ROLE);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
