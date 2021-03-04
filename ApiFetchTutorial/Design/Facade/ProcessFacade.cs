using ApiFetchTutorial.Design.Facade.SubProcessClasses;
using ApiFetchTutorial.Model.Entities.ProcessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiFetchTutorial.Model.Enums.EnumClass;

namespace ApiFetchTutorial.Design.Facade
{
    public class ProcessFacade
    {
        private static FetchProcess _fetchProcess;

        public ProcessFacade()
        {
            _fetchProcess = FetchProcess.GetInstance();
        }


        public async Task<Response> GetApiResponse(ApiCategoryEnum apiCategoryEnum, bool isList)
        {
            return await _fetchProcess.GetApiResponse(apiCategoryEnum, isList);
        }
    }
}
