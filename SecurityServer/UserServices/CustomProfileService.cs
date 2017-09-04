using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VacationSolution.Web.Entities;

namespace SecurityServer.UserServices
{
    public class CustomProfileService : IProfileService
    {
        protected readonly ILogger _logger;
        protected readonly IUserRepository _userRepository;
        public CustomProfileService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            _logger.LogDebug("Get profile called for Subject {Subject} from Client {Client} with claim Type{ClaimTypes} with cller{caller}",
            context.Subject.GetSubjectId(),
            context.Client.ClientName ?? context.Client.ClientId,
            context.RequestedClaimTypes,
            context.Caller);
            User user = _userRepository.FindBySubjectId(context.Subject.GetSubjectId());

            var cliams = new List<Claim>
            {
                new Claim("role","resources.user"),
                new Claim("username", user.Email),
                new Claim("email", user.Email)
            };

            //it suppose to be username . just for learning purpose
            if (user.Email == "postmanadmin")
            {
                cliams.Add(new Claim("roles", "resource.admin"));
            }
            context.IssuedClaims = cliams;

            return Task.FromResult(0);
        }


        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
