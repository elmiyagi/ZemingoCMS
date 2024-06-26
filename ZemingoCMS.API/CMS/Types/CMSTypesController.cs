using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZemingoCMS.Application.CMS.Types.Commands;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Queries;
using ZemingoCMS.Application.Models;

namespace ZemingoCMS.API.CMS.Types
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSTypesController : APIController
    {
        [HttpGet]
        [ProducesResponseType<List<CMSTypeDTO>>(200)]
        public async Task<IActionResult> Get([FromServices] IGetCMSTypesQueryHandler handler,
            [FromQuery] PagedParams pagedParams)
        {
            return Ok(await handler.Handle(new GetCMSTypesQuery(pagedParams)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<CMSTypeDTO>(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromServices] IGetCMSTypeByIdQueryHandler handler,
            [FromRoute] Guid id)
        {
            var cmsType = await handler.Handle(new GetCMSTypeByIdQuery(id));

            if (cmsType == null)
                return NotFound();

            return Ok(cmsType);
        }

        [HttpPost]
        [ProducesResponseType<ObjectCommandResult<CMSTypeDTO>>(200)]
        [ProducesResponseType<CommandResult>(400)]
        public async Task<IActionResult> Post([FromServices] IAddCMSTypeCommandHandler handler,
            [FromBody] AddCMSTypeDTO dto)
        {
            return GetCommandResponse<CMSTypeDTO>(await handler.Handle(new AddCMSTypeCommand(dto)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType<ObjectCommandResult<CMSTypeDTO>>(200)]
        [ProducesResponseType<CommandResult>(400)]
        [ProducesResponseType<CommandResult>(404)]
        [ProducesResponseType<CommandResult>(409)]
        public async Task<IActionResult> Put([FromServices] IUpdateCMSTypeCommandHandler handler,
            [FromBody] UpdateCMSTypeDTO dto,
            [FromRoute] Guid id)
        {
            return GetCommandResponse<CMSTypeDTO>(await handler.Handle(new UpdateCMSTypeCommand(id, dto)));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType<CommandResult>(404)]
        public async Task<IActionResult> Delete([FromServices] IDeleteCMSTypeCommandHandler handler,
            [FromRoute] Guid id)
        {
            return GetCommandResponse(await handler.Handle(new DeleteCMSTypeCommand(id)));
        }
    }
}
