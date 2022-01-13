using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public ActionResult<PagedResponseModel<AssignmentDto>> GetAssignments(
            [FromQuery] AssignmentQueryCriteriaDto assignmentCriteriaDto,
            CancellationToken cancellationToken)
        {
            var assignmentsResponse = _assignmentService.GetByPage(
                assignmentCriteriaDto,
                cancellationToken);

            return Ok(assignmentsResponse);
        }

    }
}
