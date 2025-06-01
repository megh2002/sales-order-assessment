using Microsoft.AspNetCore.Mvc;
using SalesOrderAssessment.Services;

namespace SalesOrderAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public SalesOrderController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        public async Task<ActionResult> UploadSalesOrderHistoricalData([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("CSV file is required.");
            try
            {
                await _commonService.ReadCSVFile(file);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
