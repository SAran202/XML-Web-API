using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace XML_Web_API.Controllers
{
    [ApiController]
    [Route("api")]
    [FormatFilter]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get list of data in json/xml format.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns list of data in json/xml format.</returns>
        [HttpGet("GetData.{format}")]
        public List<DataModel> Get(int id)
        {
            HttpContextAccessor context = new HttpContextAccessor();

            List<DataModel> dataModels = new List<DataModel>()
            {
                new DataModel() {Id =  1, Name = "First Data", Date = DateTime.Now},
                new DataModel() {Id =  2, Name = "Second Data", Date = DateTime.Now}
            };

            _logger.LogInformation(JsonConvert.SerializeObject(dataModels));

            return dataModels.Where(r => (id != 0 && r.Id == id) || id == 0).ToList();
        }

        /// <summary>
        /// Post data in json/xml format.
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns>Return inserted data in json/xml format.</returns>
        [HttpPost("PostData.{format}")]
        public DataModel Post(DataModel dataModel)
        {
            return dataModel;
        }
    }
}