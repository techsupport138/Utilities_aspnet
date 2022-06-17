namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FormController : BaseApiController {
	private readonly IFormRepository _formRepository;

	public FormController(IFormRepository formRepository) {
		_formRepository = formRepository;
	}

	[HttpPost("CreateFormField")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> CreateFormField(FormFieldDto dto) {
		GenericResponse<IEnumerable<FormFieldDto>?> i = await _formRepository.CreateFormFields(dto);
		return Result(i);
	}

	[HttpPut("UpdateFormField")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> UpdateFormField(FormFieldDto dto) {
		GenericResponse<IEnumerable<FormFieldDto>?> i = await _formRepository.UpdateFormFields(dto);
		return Result(i);
	}

	[AllowAnonymous]
	[HttpGet("{categoryId:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> ReadFormFieldById(Guid categoryId) {
		GenericResponse<IEnumerable<FormFieldDto>> i = await _formRepository.ReadFormFields(categoryId);
		return Result(i);
	}

	[HttpPost]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> CreateForm(FormCreateDto model) {
		GenericResponse<IEnumerable<FormFieldDto>> i = await _formRepository.UpdateFormBuilder(model);
		return Result(i);
	}

	[HttpDelete("DeleteFormField/{id:guid}")]
	public async Task<IActionResult> DeleteFormField(Guid id) {
		await _formRepository.DeleteFormField(id);
		return Ok();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteFormBuilder(Guid id) {
		await _formRepository.DeleteFormBuilder(id);
		return Ok();
	}
}