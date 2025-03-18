using Microsoft.AspNetCore.Mvc;

namespace Asp.netCoreWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    /// <summary>
    /// Lấy danh sách tất cả sinh viên
    /// </summary>
    /// <returns>Mảng tên của học sinh</returns>
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var studentNames = new string[] { "Doan", "Minh", "Truong" };
        return Ok(studentNames);
    }
}