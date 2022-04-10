using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using QueryRoom.Models;

namespace QueryRoom.Controllers.API_controllers
{
    public class AnswersAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();

        //Answering a question
        //todo - USERNAME 
        public IHttpActionResult PostAnswer(Answers tanswer)                                            //OKK for DTOs
        {
            TBL_ANSWERS tbObj = new TBL_ANSWERS();
            if(ModelState.IsValid)
            {
                tbObj.AID = Guid.NewGuid();
                tbObj.ANSWER = tanswer.ANSWER;
                tbObj.QID = tanswer.QID;
                tbObj.USERNAME = tanswer.USERNAME;
                tbObj.LIKES = 0;
                tbObj.DISLIKES = 0;
                tbObj.ISVALIDATED = false;
                tbObj.DATE = DateTime.Now;

                dbObj.TBL_ANSWERS.Add(tbObj);
                var user = dbObj.TBL_SIGNUP.ToList().Find(x=>x.USERNAME == tanswer.USERNAME);
                user.POINTS += 5;
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
        }

        [HttpGet]
        //View Answers API
        public IHttpActionResult GetAnswers(Guid qid)                    //OKK for DTOs
        {
            var ans = dbObj.TBL_ANSWERS.Where(x=>x.QID == qid);
            if (ans==null)
            {
                return NotFound();
            }
            return Ok(ans.ToList().Select(Mapper.Map<TBL_ANSWERS,Answers>));
        }
        [HttpGet]
        public IHttpActionResult InsertLikeOrDislike(Guid aid, int func)            //OKK for DTOs
        {
            var answer = dbObj.TBL_ANSWERS.ToList().Find(x=>x.AID == aid);
            var question = dbObj.TBL_QUESTIONS.ToList().Find(x=>x.QID == answer.QID);           
            var answerPostedBy = dbObj.TBL_SIGNUP.ToList().Find(x=>x.USERNAME == answer.USERNAME);
            if (answer==null)
            {
                return NotFound();
            }
            if (func == 1 )
            {
                answer.LIKES +=1;
                answerPostedBy.POINTS +=3;
            }
            else if(func ==0)
            {
                answer.DISLIKES +=1;
                answerPostedBy.POINTS -=3;
            }
            else
            {
                return BadRequest();
            }
            dbObj.SaveChanges();
            return Ok(Mapper.Map<TBL_QUESTIONS,Questions>(question));
        }
        [HttpGet]
        public IHttpActionResult ValidateAnswer(Guid aid)                                                                //OKK for dto
        {
            var res = dbObj.TBL_ANSWERS.ToList().Find(x => x.AID == aid);
            if (res==null)
            {
                return NotFound();
            }
            var question = dbObj.TBL_QUESTIONS.ToList().Find(x => x.QID == res.QID);
            
            res.ISVALIDATED = true;

            var user = dbObj.TBL_SIGNUP.ToList().Find(x => x.USERNAME == res.USERNAME);
            user.POINTS += 5;
            dbObj.SaveChanges();
            return Ok(Mapper.Map<TBL_QUESTIONS,Questions>(question));
        }
    }

}
