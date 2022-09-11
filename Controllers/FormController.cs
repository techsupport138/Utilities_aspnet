namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FormController : BaseApiController {
	private readonly IFormRepository _formRepository;

	public FormController(IFormRepository formRepository) => _formRepository = formRepository;

	[HttpPost("CreateFormField")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> CreateFormField(FormFieldDto dto)
		=> Result(await _formRepository.CreateFormFields(dto));

	[HttpPut("UpdateFormField")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> UpdateFormField(FormFieldDto dto)
		=> Result(await _formRepository.UpdateFormFields(dto));

	[AllowAnonymous]
	[HttpGet("{categoryId:guid}")]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> ReadFormFieldById(Guid categoryId)
		=> Result(await _formRepository.ReadFormFields(categoryId));

	[HttpPost]
	public async Task<ActionResult<GenericResponse<IEnumerable<FormFieldDto>>>> CreateForm(FormCreateDto model)
		=> Result(await _formRepository.UpdateForm(model));

	[HttpDelete("DeleteFormField/{id:guid}")]
	public async Task<IActionResult> DeleteFormField(Guid id) => Ok(await _formRepository.DeleteFormField(id));

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteFormBuilder(Guid id) => Ok(await _formRepository.DeleteFormBuilder(id));
}