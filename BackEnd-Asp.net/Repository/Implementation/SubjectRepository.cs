using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contract;

namespace Repository.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext context;

        public SubjectRepository(AppDbContext _context)
        {
            context = _context;
        }
        public void Add(CreatedSubjectDTO addedSubjectDTO)
        {
            Subject addedSubject = new Subject()
            {
                SubjectName = addedSubjectDTO.SubjectName,
                SubjectDescription = addedSubjectDTO.SubjectDescription,
                InstructorID = addedSubjectDTO.InstructorID,
                Price = addedSubjectDTO.Price
            };

            context.Subjects.Add(addedSubject);

            int affectedRows = context.SaveChanges();
            Console.WriteLine("Rows affected: " + affectedRows);



            StudentClassSubject studentClassSubject = new StudentClassSubject()
            {
                SubjectID = addedSubject.SubjectID,
                InstructorID = addedSubjectDTO.InstructorID,
                ClassID = addedSubjectDTO.ClassID,
                TrackID = addedSubjectDTO.TrackID,


            };

            context.StudentClassSubjects.Add(studentClassSubject);
            int affectedRows2 = context.SaveChanges();
            Console.WriteLine("Rows affected: " + affectedRows);

            //context.Subjects.Add(addedSubject);
        }

        public List<SubjectWithUnits> GetAll()
        {
            List<SubjectWithUnits> subjectDTOs = new List<SubjectWithUnits>();

            var classSubjects = context.Subjects
                        .Include(s => s.Instructor)               // load Instructor
                            .ThenInclude(i => i.User)              // load ApplicationUser
                        .Include(s => s.StudentClassSubject)
                            .ThenInclude(scs => scs.Class)
                        .Include(s => s.StudentClassSubject)
                            .ThenInclude(scs => scs.Track)
                        .ToList();

            foreach (var subject in classSubjects)
            {
                var scs = subject.StudentClassSubject;
                var user = subject.Instructor?.User;

                SubjectWithUnits subjectDTO = new SubjectWithUnits()
                {
                    SubjectID = subject.SubjectID,
                    SubjectName = subject.SubjectName,
                    SubjectDescription = subject.SubjectDescription,
                    InstructorID = subject.InstructorID,
                    InstructorName = user != null
                        ? $"{user.FirstName} {user.LastName}"
                        : string.Empty,
                    ClassName = scs?.Class?.ClassName ?? string.Empty,
                    TrackName = scs?.Track?.TrackName ?? string.Empty,
                    Price = subject.Price,
                    ClassID = scs.ClassID,
                    TrackID = scs.TrackID
                };

                subjectDTOs.Add(subjectDTO);
            }



            return subjectDTOs;
        }

        public Subject GetById(int id)
        {

            return context.Subjects.FirstOrDefault(s => s.SubjectID == id);

        }

        public Subject GetByName(string name)
        {
            return context.Subjects.SingleOrDefault(c => c.SubjectName == name);
        }

        public void RemoveById(int id)
        {
            var removedSubject = context.Subjects.Find(id);
            if (removedSubject != null)
            {
                context.Subjects.Remove(removedSubject);
            }
        }

        public void UpdateById(int id, CreatedSubjectDTO updatedSubjectDTO)
        {
            var upSubject = context.Subjects.Find(id);
            var oldClassSubject = context.StudentClassSubjects.FirstOrDefault(sc => sc.SubjectID == id);

            if (upSubject != null && oldClassSubject != null)
            {


                upSubject.SubjectName = updatedSubjectDTO.SubjectName;
                upSubject.SubjectDescription = updatedSubjectDTO.SubjectDescription;
                upSubject.InstructorID = updatedSubjectDTO.InstructorID;
                upSubject.Price = updatedSubjectDTO.Price;


                context.SaveChanges();



                context.StudentClassSubjects.Remove(oldClassSubject);
                context.SaveChanges();


                // Add new class-subject relationship
                var newClassSubject = new StudentClassSubject
                {
                    SubjectID = id,
                    InstructorID = updatedSubjectDTO.InstructorID,
                    ClassID = updatedSubjectDTO.ClassID,
                    TrackID = updatedSubjectDTO.TrackID
                };

                context.StudentClassSubjects.Add(newClassSubject);
                context.SaveChanges();

                //context.Subjects.Update(updatedSubject);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void AddPayment(Payment payment)
        {
            context.Payments.Add(payment);
            context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            context.Payments.Update(payment);
            context.SaveChanges();
        }

        public Subject GetByIdWithInstructorAndPayment(int id)
        {
            return context.Subjects
            .Include(s => s.Instructor).Include(s => s.Payments)
             .FirstOrDefault(s => s.SubjectID == id);
        }


    }
}