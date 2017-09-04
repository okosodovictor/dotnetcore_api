using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using VacationSolution.Web.DTOS;
using VacationSolution.Web.Entities;
using VacationSolution.Web.Interfaces;
using VacationSolution.Web.QueryParameters;

namespace VacationSolution.Web.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/Vacations")]
    //[Authorize(Policy ="resourceuser")]
    public class VacationRequestController : Controller
    {
        private readonly ILogger<VacationRequestController> _logger;
        private IVacationRequestRepository _repo;
        public VacationRequestController(IVacationRequestRepository repo, ILogger<VacationRequestController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogInformation("VacationRequestController Started");
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<VacationRequest>), 200)]
        public IActionResult Get(VacationQueryParamaters vacationQueryParamater)
        {
            _logger.LogInformation("Get All Vacation Called");
            var allVacationRequest = _repo.GetAll(vacationQueryParamater).ToList();
            var allvactionRequestDTO = allVacationRequest.Select(x => Mapper.Map<VacationRequestDTO>(x));

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(new { totalCount = _repo.Count() }));

            return Ok(allvactionRequestDTO);
        }

        [HttpGet]
        [Route("{id}", Name = "GetVacationSingle")]
        public IActionResult Get(int id)
        {
            var vacationRequest = _repo.GetVacationByID(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<VacationRequestDTO>(vacationRequest));
        }

        [HttpPost]
        [ProducesResponseType(typeof(VacationRequestDTO), 201)]
        [ProducesResponseType(typeof(VacationRequestDTO), 400)]
        public IActionResult Post([FromBody] VacationRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest("Vacation object was null");
            }
            VacationRequest request = Mapper.Map<VacationRequest>(model);
            _repo.Add(request);

            bool result = _repo.Save();
            if (!result)
            {
                throw new Exception("Something Went wrong when saving vacation Request");
            }
            return CreatedAtRoute("GetVacationSingle", new { request.RequestID }, (Mapper.Map<VacationRequestDTO>(request)));
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] VacationRequestDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var existingVacation = _repo.GetVacationByID(id);

            if (existingVacation == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(model, existingVacation);
            bool result = _repo.Save();
            if (!result)
            {
                throw new Exception($"Something went wrong while updating vacaction request with Id:{id}");
            }
            return Ok(Mapper.Map<VacationRequestDTO>(existingVacation));
        }
        [HttpDelete]

        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var vacationRequest = _repo.GetVacationByID(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            _repo.Delete(id);

            bool result = _repo.Save();

            if (!result)
            {
                throw new Exception("Could not remove Vacation record");
            }
            return NoContent();
        }
    }
}