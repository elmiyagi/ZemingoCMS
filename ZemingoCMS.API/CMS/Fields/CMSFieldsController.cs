using Microsoft.AspNetCore.Mvc;
using ZemingoCMS.Application.CMS.Fields.Commands;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Fields.Queries;
using ZemingoCMS.Application.Models;

namespace ZemingoCMS.API.CMS.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSFieldsController : APIController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return null;
        }
    }
}
