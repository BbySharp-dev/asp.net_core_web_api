using Asp.netCoreWeb.Data;
using Asp.netCoreWeb.Models.Domain;
using Asp.netCoreWeb.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NzWalksDbContext _nzWalksDbContext;

    public RegionsController(NzWalksDbContext nzWalksDbContext)
    {
        // Dependency Injection: Tiêm đối tượng DbContext vào controller để sử dụng
        _nzWalksDbContext = nzWalksDbContext ?? throw new ArgumentNullException(nameof(nzWalksDbContext));
    }

    [HttpGet]
    public IActionResult GetAllRegions()
    {
        // Lấy tất cả các vùng từ cơ sở dữ liệu
        var regionsDomains = _nzWalksDbContext.Regions.ToList();

        // Chuyển đổi các domain model thành DTOs
        var regionsDto = regionsDomains.Select(MapToRegionDto).ToList();

        // Trả về danh sách DTOs cho client
        return Ok(regionsDto);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetRegionById(Guid id)
    {
        // Lấy một vùng theo ID từ cơ sở dữ liệu
        var regionsDomain = _nzWalksDbContext.Regions.Find(id);
        if (regionsDomain == null)
        {
            // Trả về 404 Not Found nếu vùng không tồn tại
            return NotFound($"Region with ID {id} not found.");
        }

        // Chuyển đổi domain model thành DTO
        var regionDto = MapToRegionDto(regionsDomain);

        // Trả về DTO cho client
        return Ok(regionDto);
    }

    [HttpPost]
    public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Chuyển đổi DTO đầu vào thành domain model
        var regionDomainModel = new Region
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl,
        };

        // Thêm vùng mới vào cơ sở dữ liệu
        _nzWalksDbContext.Regions.Add(regionDomainModel);
        _nzWalksDbContext.SaveChanges();

        // Chuyển đổi domain model trở lại thành DTO
        var regionDto = MapToRegionDto(regionDomainModel);

        // Trả về DTO đã tạo với trạng thái 201 Created
        return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateRegionById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // Kiểm tra xem region có tồn tại hay ko
        var regionDomainModel= _nzWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
        if (regionDomainModel == null) return NotFound($"Region with ID {id} not found.");
        
        regionDomainModel.Code = updateRegionRequestDto.Code;
        regionDomainModel.Name = updateRegionRequestDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
        
        _nzWalksDbContext.SaveChanges();
        
        
        // Chuyển Domain Model to DTO

        var regionDto = MapToRegionDto(regionDomainModel);
        
        return Ok(regionDto);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteRegionById([FromRoute] Guid id)
    {
        var regionDomainModel = _nzWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
        if (regionDomainModel == null) return NotFound($"Region with ID {id} not found.");
        
        // Xoá region
        _nzWalksDbContext.Regions.Remove(regionDomainModel);
        _nzWalksDbContext.SaveChanges();
        
        // Chuyển Domain Model thành DTO
        var regionDto = MapToRegionDto(regionDomainModel);
        
        return Ok(regionDto);
    }
    
    
    // Phương thức private để chuyển đổi domain model thành DTO
    private static RegionDto MapToRegionDto(Region region)
    {
        return new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl,
        };
    }
}