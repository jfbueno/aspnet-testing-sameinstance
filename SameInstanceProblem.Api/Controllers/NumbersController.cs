using Microsoft.AspNetCore.Mvc;

namespace SameInstanceProblem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private readonly INumberGenerator _numGenerator;

        public NumberController(INumberGenerator numGenerator)
        {
            _numGenerator = numGenerator;
        }

        [HttpGet]
        public int Get() => _numGenerator.Generate();
    }
}
