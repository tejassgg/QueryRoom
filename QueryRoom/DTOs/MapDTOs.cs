using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoom.Models.DTOs
{
    public class MapDTOs
    {
        public static Questions MapQuestionDTO(TBL_QUESTIONS toBeMapped)
        {
            Questions toMap = new Questions();
            toMap.QID = toBeMapped.QID;
            toMap.QUESTION = toBeMapped.QUESTION;
            toMap.TIMESTAMP = (DateTime)toBeMapped.TIMESTAMP;
            toMap.USERNAME = toBeMapped.USERNAME;
            return toMap;

        }
        public static List<Questions> MapQuestionsListDTO(IEnumerable<TBL_QUESTIONS> toBeMapped)
        {
            List<Questions> toMap = new List<Questions>();
            foreach (var question in toBeMapped)
            {
                var _question = new Questions();
                _question.QID = question.QID;
                _question.QUESTION = question.QUESTION;
                _question.TIMESTAMP = (DateTime)question.TIMESTAMP;
                _question.USERNAME = question.USERNAME;
                toMap.Add(_question);
            }
            return toMap;
            
        }
    }
}