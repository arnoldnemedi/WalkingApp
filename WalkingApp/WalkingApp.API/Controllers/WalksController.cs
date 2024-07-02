using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WalkingApp.API.Models.Domain;
using WalkingApp.API.Models.DTO;
using WalkingApp.API.Repositories;

namespace WalkingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper autoMapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper autoMapper, IWalkRepository walkRepository)
        {
            this.autoMapper = autoMapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkDto)
        {
            var walk = autoMapper.Map<Walk>(addWalkDto);
            await walkRepository.CreateAsync(walk);

            var walkDto = autoMapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        { 
            //throw new Exception("Error happened here");

            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var walksDto = autoMapper.Map<List<WalkDto>>(walks);

            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            var walkDto = autoMapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkDto)
        {
            var walk = autoMapper.Map<Walk>(updateWalkDto);
            walk = await walkRepository.UpdateAsync(id, walk);

            if(walk == null)
            {
                return NotFound();
            }

            var walkDto = autoMapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(walk);
        }
    }
}
