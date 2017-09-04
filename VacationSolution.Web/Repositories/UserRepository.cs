using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationSolution.Web.Entities;

namespace VacationSolution.Web.Repositories
{
    public class UserRepository
    {
        private readonly EntityContext _Context;
        public UserRepository(EntityContext context)
        {
            _Context = context;
        }
    }
}
