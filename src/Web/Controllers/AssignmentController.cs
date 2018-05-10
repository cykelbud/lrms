using System;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("")]
        public async Task<AssignmentDto[]> GetAllAssignments()
        {
            return await _assignmentService.GetAll();
        }

        [HttpGet("{invoiceId}")]
        public async Task<AssignmentDto> GetAssignment(Guid invoiceId)
        {
            return await _assignmentService.GetAssignment(invoiceId);
        }

    }
}
