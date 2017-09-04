using System.Linq;
using VacationSolution.Web.Entities;
using VacationSolution.Web.QueryParameters;

namespace VacationSolution.Web.Interfaces
{
    public interface IVacationRequestRepository
    {
        void Add(VacationRequest model);
        void Delete(int id);
        IQueryable<VacationRequest> GetAll(VacationQueryParamaters vacationQueryParrameter);
        VacationRequest GetVacationByID(int id);
        bool Save();
        int Count();
        void Update(VacationRequest model);
    }
}