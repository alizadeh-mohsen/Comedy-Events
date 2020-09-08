using Comedy_Events.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comedy_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComediansController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public ComediansController(IEventRepository repository)
        {
            _repository = repository;
        }

      



    }
}
