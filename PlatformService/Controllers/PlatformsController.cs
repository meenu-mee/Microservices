using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformsService.Data;
using PlatformsService.Dtos;
using PlatformsService.Models;
using PlatformsService.SyncDataServices.Http;

namespace PlatformsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        // Constructor with dependency injection for the repository and AutoMapper
        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        // GET api/platforms
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");
            var platformItems = _repository.GetAllPlatforms();

            // Map the list of Platform models to a list of PlatformReadDto objects and return it in the response
            Console.WriteLine($"--> Returning {platformItems.Count()} platforms.");
 
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        // GET api/platforms/{id}
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }

        // POST api/platforms
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            // Map the incoming PlatformCreateDto object to a Platform model, create the new platform in the repository, and save the changes to the database
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            // Map the newly created Platform model to a PlatformReadDto object to return in the response
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                // Send the platform data to the CommandService
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            // Return a 201 Created response with the location of the newly created platform and the platform data in the response body 
            // (e.g., location header will be /api/platforms/{id} where {id} is the ID of the newly created platform)
            // nameof(GetPlatformById) : ensures the response includes a Location header pointing to the URL for fetching the platform by its ID
            // new { Id = platformReadDto.Id } : supplies the route values—the ID of the created platform—to construct the full URL dynamically. 
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}