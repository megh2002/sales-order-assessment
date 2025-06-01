namespace SalesOrderAssessment.Services
{
    public interface ICommonService
    {
        Task ReadCSVFile(IFormFile file);
    }
}
