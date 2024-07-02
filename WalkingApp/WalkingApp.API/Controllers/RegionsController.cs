using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;
using WalkingApp.API.CustomActionFilters;
using WalkingApp.API.Data;
using WalkingApp.API.Models.Domain;
using WalkingApp.API.Models.DTO;
using WalkingApp.API.Repositories;

namespace WalkingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper autoMapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper autoMapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.autoMapper = autoMapper;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDto = autoMapper.Map<RegionDto>(region);
            
            return Ok(region);
        }

        [HttpGet]
        //[Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAllRegions Action Method was invoked!");

            var regions = await regionRepository.GetAllAsync();
            var regionsDto = autoMapper.Map<List<RegionDto>>(regions);

            logger.LogInformation($"Finsihed GetAllRegions request with dara: {JsonSerializer.Serialize(regions)}");

            return Ok(regionsDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
        {
            var region = autoMapper.Map<Region>(regionDto);
            await regionRepository.CreateAsync(region);
            var regionDto2 = autoMapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = region.Id}, regionDto2);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto regionDto)
        {
            var region = autoMapper.Map<Region>(regionDto);

            region = await regionRepository.UpdateAsync(id, region);

            if(region == null)
            {
                return NotFound();
            }

            var regionDto2 = autoMapper.Map<RegionDto>(region);

            return Ok(regionDto2);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
