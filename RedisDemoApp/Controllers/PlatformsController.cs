using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisDemoApp.Dtos;
using RedisDemoApp.Models;
using RedisDemoApp.Repository;

namespace RedisDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatfomRepository _platfomRepository;

        public PlatformsController(IPlatfomRepository platfomRepository)
        {
            _platfomRepository = platfomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            return Ok(await _platfomRepository.GetAllPlatforms());
        }


        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<IActionResult> GetPlatformById(Guid id)
        {
            var platform = await _platfomRepository.GetPlatform(id);

            if (platform != null)
            {
                return Ok(platform);
            }

            return NotFound();
        }


        [HttpDelete("{id}", Name = "DeletePlatformById")]
        public async Task<IActionResult> DeletePlatformById(Guid id)
        {
            var result = await _platfomRepository.DeletePlatform(id);

            if(result)
                return Ok("The item was deleted");
           
            return NotFound();
        }

        [HttpPut("{id}", Name = "UpdatePlatformById")]
        public async Task<IActionResult> UpdatePlatformById(AddPlatformDto platformDto, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Platform platform = new()
            {
                Id = id,
                Name = platformDto.Name,
            };

            var result = await _platfomRepository.UpdatePlatform(platform, id);

            if(result)
                return Ok("The item was deleted");
           
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> CreatePlatform(AddPlatformDto platformDto)
        {
            Platform platform = new()
            {
                Id = Guid.NewGuid(),
                Name = platformDto.Name,
            };

            await _platfomRepository.AddPlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        }
    }
}
