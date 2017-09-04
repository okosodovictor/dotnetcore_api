using VacationSolution.Web.Entities;

namespace SecurityServer.UserServices
{
    public interface IUserRepository
    {
        bool ValidateCredential(string email, string password);
        User FindByEmail(string email);
        User FindBySubjectId(string v);
    }
}