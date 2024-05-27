using TeamHubServiceUser.Entities;
using TeamHubServiceUser.Gateways.Interfaces;
using TeamHubServiceUser.DTOs;

namespace TeamHubServiceUser.Gateways.Providers;

public class UserService : IUserService
{

    private TeamHubContext dbContext;

    public UserService(TeamHubContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public int AddStudent(StudentDTO newStudent)
    {
        int result = 0;
        try
        {
            var dbStudent = new student
            {
                IdStudent = 0,
                Name = newStudent.Name,
                MiddleName = newStudent.MiddleName,
                LastName = newStudent.LastName,
                SurName = newStudent.SurName,
                Email = newStudent.Email,
                Password = newStudent.Password,
                ProDocumentImage = newStudent.ProDocumentImage
            };

            dbContext.student.Add(dbStudent);
            result = dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }

    public int DeleteStudent(int IdDeleteStudent)
    {
        int result = 0;
        try
        {
            var dbStudent = dbContext.student.Find(IdDeleteStudent);
            if (dbStudent != null)
            {
                dbContext.student.Remove(dbStudent);
                result = dbContext.SaveChanges();
            }
            else
            {
                result = -1;
            }
            result = dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }

    public int EditStudent(StudentDTO editStudent)
    {
        int result = 0;
        try
        {

            var dbStudent = dbContext.student.Find(editStudent.IdStudent);
            if (dbStudent != null)
            {
                dbStudent.IdStudent = editStudent.IdStudent;
                dbStudent.Name = editStudent.Name;
                dbStudent.MiddleName = editStudent.MiddleName;
                dbStudent.LastName = editStudent.LastName;
                dbStudent.SurName = editStudent.SurName;
                dbStudent.Email = editStudent.Email;
                dbStudent.Password = editStudent.Password;
                dbStudent.ProDocumentImage = editStudent.ProDocumentImage;
                dbContext.student.Update(dbStudent);
                result = dbContext.SaveChanges();
            }
            else
            {
                result = -1;
            }
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }
}