using TeamHubServiceUser.Entities;
using TeamHubServiceUser.DTOs;

namespace TeamHubServiceUser.Gateways.Interfaces;

public interface IUserService
{
    public int AddStudent(StudentDTO newStudent);
    public int EditStudent(StudentDTO editStudent);
    public int DeleteStudent(int IdDeleteStudent);
}