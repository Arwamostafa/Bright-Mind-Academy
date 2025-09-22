using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;

namespace Service.Services.Contract
{
    public interface ISubjectService
    {
        List<SubjectWithUnits> GetAllSubjects();

        Subject GetSubjectById(int id);

        Subject GetSubjectByName(string name);

        CreatedSubjectDTO AddSubject(CreatedSubjectDTO addedSubject);

        string RemoveSubjectById(int id);
        string UpdateSubjectById(int id, CreatedSubjectDTO upSubjectDTO);

    }
}