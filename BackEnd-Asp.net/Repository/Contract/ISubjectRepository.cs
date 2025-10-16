using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;

namespace Repository.Contract
{
    public interface ISubjectRepository
    {
        List<SubjectWithUnits> GetAll();

        Subject GetById(int id);

        Subject GetByName(string name);

        void Add(CreatedSubjectDTO addedSubjectDTO);

        void RemoveById(int id);
        void UpdateById(int id, CreatedSubjectDTO updatedSubjectDTO);

        void Save();
        //void AddPayment(Payment payment);
        //void UpdatePayment(Payment payment);
        //Subject GetByIdWithInstructorAndPayment(int id);


    }
}