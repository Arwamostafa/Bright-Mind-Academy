using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Services.Implementation;

namespace Service.Services.Contract
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext db;

        public QuizService(AppDbContext _db)
        {
            db = _db;
        }
        public List<quizdto> getallquizzes()
        {
            var quizzes = db.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).ToList();
            List<quizdto> quizdtos = new List<quizdto>();
            foreach (var item in quizzes)
            {
                quizdto quizdto = new quizdto();
                quizdto.Id = item.Id;
                quizdto.AssignedBefore = item.AssignedBefore;
                quizdto.Description = item.Description;
                quizdto.TotalMarks = item.TotalMarks;
                quizdto.LessonId = item.LessonId;
                foreach (var question in item.Questions)
                {
                    questiondto questiondto = new questiondto();
                    questiondto.id = question.id;
                    questiondto.mark = question.mark;
                    questiondto.Content = question.Content;
                    foreach (var option in question.Options)
                    {
                        optiondto optiondto = new optiondto();
                        optiondto.id = option.id;
                        optiondto.Name = option.Name;
                        optiondto.IsCorrect = option.IsCorrect;
                        questiondto.Options.Add(optiondto);
                    }
                    quizdto.Questions.Add(questiondto);
                }
                quizdtos.Add(quizdto);
            }
            return quizdtos;
        }
        public int addquiz(quizdto q)
        {
            if (q == null) return 0;

            var found = db.Quizzes.FirstOrDefault(x => x.Id == q.Id);
            if (found != null) return -1;

            var quiz = new quiz
            {
                Description = q.Description,
                AssignedBefore = q.AssignedBefore,
                TotalMarks = q.TotalMarks,
                LessonId = q.LessonId,
                Questions = new List<question>()
            };

            foreach (var item in q.Questions)
            {
                var question = new question
                {
                    Content = item.Content,
                    mark = item.mark,
                    Options = new List<option>()
                };

                foreach (var opt in item.Options)
                {
                    var option = new option
                    {
                        Name = opt.Name,
                        IsCorrect = opt.IsCorrect
                    };
                    question.Options.Add(option);
                }

                quiz.Questions.Add(question);
            }

            db.Quizzes.Add(quiz);
            db.SaveChanges();

            return quiz.Id;
        }

        public quizdto getquiz(int id)
        {
            var q = db.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).FirstOrDefault(q => q.Id == id);
            if (q != null)
            {
                quizdto quizdto = new quizdto();
                quizdto.Id = q.Id;
                quizdto.AssignedBefore = q.AssignedBefore;
                quizdto.Description = q.Description;
                quizdto.TotalMarks = q.TotalMarks;
                quizdto.LessonId = q.LessonId;
                foreach (var item in q.Questions)
                {
                    questiondto questiondto = new questiondto();
                    questiondto.id = item.id;
                    questiondto.mark = item.mark;
                    questiondto.Content = item.Content;

                    foreach (var itemm in item.Options)
                    {
                        optiondto optiondto = new optiondto();
                        optiondto.id = itemm.id;
                        optiondto.Name = itemm.Name;
                        optiondto.IsCorrect = itemm.IsCorrect;
                        questiondto.Options.Add(optiondto);

                    }
                    quizdto.Questions.Add(questiondto);

                }
                return quizdto;
            }
            return null;
        }

        public int updatequiz(quizdto quizdto)
        {
            if (quizdto == null) return 0;

            var found = db.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(q => q.Id == quizdto.Id);

            if (found == null) return -1;

            found.AssignedBefore = quizdto.AssignedBefore;
            found.Description = quizdto.Description;
            found.TotalMarks = quizdto.TotalMarks;
            Console.WriteLine($"LessonId from DTO: {quizdto.LessonId}");

            found.LessonId = quizdto.LessonId;

            foreach (var item in found.Questions.ToList())
            {
                var check = quizdto.Questions.FirstOrDefault(q => q.id == item.id);
                if (check != null)
                {
                    item.mark = check.mark;
                    item.Content = check.Content;

                    foreach (var itemm in item.Options.ToList())
                    {
                        var checkoption = check.Options.FirstOrDefault(q => q.id == itemm.id);
                        if (checkoption != null)
                        {
                            itemm.IsCorrect = checkoption.IsCorrect;
                            itemm.Name = checkoption.Name;
                        }
                        else
                        {
                            db.Options.Remove(itemm);
                        }
                    }

                    foreach (var newOpt in check.Options.Where(o => o.id == 0))
                    {
                        item.Options.Add(new option
                        {
                            Name = newOpt.Name,
                            IsCorrect = newOpt.IsCorrect
                        });
                    }
                }
                else
                {
                    db.Questions.Remove(item);
                }
            }

            foreach (var newQuestion in quizdto.Questions.Where(q => q.id == 0))
            {
                var question = new question
                {
                    Content = newQuestion.Content,
                    mark = newQuestion.mark,
                    Options = newQuestion.Options.Select(opt => new option
                    {
                        Name = opt.Name,
                        IsCorrect = opt.IsCorrect
                    }).ToList()
                };

                found.Questions.Add(question);
            }

            db.Quizzes.Update(found);
            db.SaveChanges();

            return 1;
        }


        public int deletequiz(int id)
        {
            var found = db.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(q => q.Id == id);

            if (found == null) return 0;

            db.Quizzes.Remove(found);
            db.SaveChanges();
            return 1;
        }


        public quizdto GetQuizByLessonId(int lessonId)
        {
            var q = db.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(q => q.LessonId == lessonId);

            if (q != null)
            {
                quizdto quizdto = new quizdto();
                quizdto.Id = q.Id;
                quizdto.AssignedBefore = q.AssignedBefore;
                quizdto.Description = q.Description;
                quizdto.TotalMarks = q.TotalMarks;
                quizdto.LessonId = q.LessonId;

                foreach (var item in q.Questions)
                {
                    questiondto questiondto = new questiondto();
                    questiondto.id = item.id;
                    questiondto.mark = item.mark;
                    questiondto.Content = item.Content;

                    foreach (var opt in item.Options)
                    {
                        optiondto optiondto = new optiondto();
                        optiondto.id = opt.id;
                        optiondto.Name = opt.Name;
                        optiondto.IsCorrect = opt.IsCorrect;
                        questiondto.Options.Add(optiondto);
                    }

                    quizdto.Questions.Add(questiondto);
                }

                return quizdto;
            }

            return null;
        }
    }
}