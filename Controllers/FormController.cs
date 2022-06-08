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
    public async Task<ActionResult<GenericResponse<List<FormFieldDto>>>> CreateFormField(FormFieldDto dto) {
        GenericResponse<List<FormFieldDto>?> i = await _formRepository.CreateFormFields(dto);
        return Result(i);
    }
    
    [HttpPut("UpdateFormField")]
    public async Task<ActionResult<GenericResponse<List<FormFieldDto>>>> UpdateFormField(FormFieldDto dto) {
        GenericResponse<List<FormFieldDto>?> i = await _formRepository.UpdateFormFields(dto);
        return Result(i);
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<ActionResult<GenericResponse<List<FormFieldDto>>>> ReadFormFieldById(Guid categoryId) {
        GenericResponse<List<FormFieldDto>> i = await _formRepository.ReadFormFields(categoryId);
        return Result(i);
    }

    [HttpPost]
    public async Task<ActionResult<GenericResponse<List<FormFieldDto>>>> CreateForm(FormCreateDto model) {
        GenericResponse<List<FormFieldDto>> i = await _formRepository.UpdateFormBuilder(model);
        return Result(i);
    }
}