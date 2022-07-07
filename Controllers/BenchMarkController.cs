using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Utilities_aspnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenchMarkController : BaseApiController {
	private readonly IProductRepository _benchMarkProductRepository;

	public BenchMarkController(IProductRepository productRepository) {
		_benchMarkProductRepository = productRepository;
	}

	[HttpGet]
	public IActionResult Read() {
		Summary? i = BenchmarkRunner.Run<ProductRepository>();
		return Result(new GenericResponse<string>(i.TotalTime.ToString()));
	}
}