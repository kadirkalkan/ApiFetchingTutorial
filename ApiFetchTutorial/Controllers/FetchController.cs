using ApiFetchTutorial.Design.Facade;
using ApiFetchTutorial.Model.Entities.ProcessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static ApiFetchTutorial.Model.Enums.EnumClass;

namespace ApiFetchTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchController : ControllerBase
    {
        private static ProcessFacade _facade = new ProcessFacade();

        [HttpGet("[action]/{apiCategoryEnum}/{isList}")]
        public async Task<Response> GetApiResponse(ApiCategoryEnum apiCategoryEnum, bool isList)
        {
            return await _facade.GetApiResponse(apiCategoryEnum, isList);
        }
    }
}
