using System.Linq;
using VacationSolution.Web.Entities;
using VacationSolution.Web.Interfaces;
using VacationSolution.Web.QueryParameters;
using System.Linq.Dynamic.Core;
namespace VacationSolution.Web.Repositories
{
    public class VacationRequestRepository : IVacationRequestRepository
    {
        private readonly EntityContext _Context;
        public VacationRequestRepository(EntityContext context)

        {
            _Context = context;
        }

        public IQueryable<VacationRequest> GetAll(VacationQueryParamaters vacationQueryParrameter)
        {

            IQueryable<VacationRequest> _allVactionRequest = _Context.VacationRequest.OrderBy(vacationQueryParrameter.OrderBy, vacationQueryParrameter.Descending);

            if (vacationQueryParrameter.HasQuery)
            {
                // I am using request ID for the now
                _allVactionRequest = _allVactionRequest.Where(x => x.RequestID.ToString().ToLowerInvariant().Contains(vacationQueryParrameter.Query.ToLowerInvariant()) || x.ApprovalID.ToString().ToLowerInvariant().Contains(vacationQueryParrameter.Query.ToLowerInvariant()));
            }
            return _Context.VacationRequest.OrderBy(x => x.ApprovalID)
                .Skip(vacationQueryParrameter.PageCount * (vacationQueryParrameter.Page - 1))
                .Take(vacationQueryParrameter.PageCount);
        }

        public void Add(VacationRequest model)
        {
            _Context.VacationRequest.Add(model);
        }

        public VacationRequest GetVacationByID(int id)
        {
            return _Context.VacationRequest.FirstOrDefault(v => v.RequestID == id);
        }

        public void Update(VacationRequest model)
        {
            _Context.VacationRequest.Update(model);
        }

        public void Delete(int id)
        {
            var vacationRequest = _Context.VacationRequest.Find(id);
            _Context.VacationRequest.Remove(vacationRequest);
        }

        public int Count()
        {
            return _Context.VacationRequest.Count();
        }
        public bool Save()
        {
            return _Context.SaveChanges() >= 0;
        }
    }
}
