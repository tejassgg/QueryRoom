using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using QueryRoom.DTOs;
using System.Web.Http;
using QueryRoom.Models;

namespace QueryRoom.Controllers
{
    public class AccountAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();
        //Registration of user 
        public IHttpActionResult accRegForm(accountClass obj)                   //Okk for DTOs
        {
            TBL_SIGNUP tbObj = new TBL_SIGNUP();
            if(ModelState.IsValid)
            {
                tbObj.NAME = obj.NAME;
                tbObj.EMAIL = obj.EMAIL;
                tbObj.USERNAME = obj.USERNAME;
                tbObj.PASSWORD = Crypto.Hash(obj.PASSWORD);
                tbObj.PHONE_NO = obj.PHONE_NO;
                tbObj.POINTS =5;
                dbObj.TBL_SIGNUP.Add(tbObj);
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
            
        }

        //List of all users
        [HttpGet]
        public IEnumerable<accountClass> GetData()                                  //OKK for DTO
        {
            return dbObj.TBL_SIGNUP.ToList().Select(AutoMapper.Mapper.Map<TBL_SIGNUP,accountClass>);
        }

        [HttpGet]
        [Route("GetRole/{username}")]
        public IHttpActionResult GetUserRole(string username)                   //OKK for DTO
        {
            if (String.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var userRole = dbObj.TBL_USERROLE.ToList().Find(x=>x.USERNAME==username);
            var role = AutoMapper.Mapper.Map<TBL_USERROLE,UserRoleDTO>(userRole);
            if (role==null)
            {
                return NotFound();
            }
            return Ok(role.ROLE);
        }
    }
}
