using Comedy_Events.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comedy_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GigsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public GigsController(IEventRepository repository)
        {
            _repository = repository;
        }

    }
}
