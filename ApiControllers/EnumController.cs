using Microsoft.AspNetCore.Mvc;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.ApiControllers {
    [ApiController]
    [Route("api/enum")]
    public class EnumController : BaseApiController {
        private readonly IEnumRepository _enumRepository;

        public EnumController(IEnumRepository enumRepository) {
            _enumRepository = enumRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<GenericResponse<EnumDto>>> GetAll(bool showCategory = false,
            bool showGeo = false) {
            GenericResponse? i = await _enumRepository.GetAll(showCategory, showGeo);
            return Ok(i);
        }
    }
}