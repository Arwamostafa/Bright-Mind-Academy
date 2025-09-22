using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.DTO;
using Microsoft.AspNetCore;

namespace Service.Services.Implementation
{
    public interface IQuizService
    {


        public int addquiz(quizdto q);

        public quizdto getquiz(int id);

        public int updatequiz(quizdto quizdto);

        public int deletequiz(int id);
        public List<quizdto> getallquizzes();

        public quizdto GetQuizByLessonId(int lessonId);

    }

}