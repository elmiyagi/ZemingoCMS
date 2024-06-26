using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZemingoCMS.API.CMS.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSItemsController : APIController
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
