using AutoMapper;
using QueryRoom.DTOs;
using QueryRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QueryRoom.Controllers.API_controllers
{
    public class AdminAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();
        public IHttpActionResult GetAllUSers()                                                               //OKK for dto
        {
            var users = dbObj.TBL_SIGNUP.ToList().Select(Mapper.Map<TBL_SIGNUP,UserDetails>);
            
            if (users.Count() >0)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }
        // LeaderBoard API
        [HttpGet]
        [Route("fetchnames")]
        public IEnumerable<UserDetails> FetchNames()
        {
            TBL_SIGNUP tbObj = new TBL_SIGNUP();

            var result = (
                from obj in dbObj.TBL_SIGNUP
                orderby obj.POINTS descending
                select obj).Take(8);                        //obj.USERNAME

            //dbObj.TBL_SIGNUP.Select(obj => obj.USERNAME).OrderByDescending(obj => obj.POINTS).Take(5);

            return result.ToList().Select(Mapper.Map<TBL_SIGNUP,UserDetails>);
        }
    }
}
