using ApiFetchTutorial.Model.Entities.DataObjects;
using ApiFetchTutorial.Model.Entities.ProcessObjects;
using ApiFetchTutorial.Model.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiFetchTutorial.Model.Enums.EnumClass;

namespace ApiFetchTutorial.Design.Facade.SubProcessClasses
{
    public class FetchProcess
    {
        private static FetchProcess _fetchProcess;
        private static Extension _extension;

        private FetchProcess()
        {
            _extension = new Extension();
        }


        public static FetchProcess GetInstance()
        {
            if (_fetchProcess == null)
                _fetchProcess = new FetchProcess();
            return _fetchProcess;
        }



        public async Task<Response> GetApiResponse(ApiCategoryEnum apiCategoryEnum, bool isList)
        {
            Response response = null;
            await Task.Run(async () =>
            {
                try
                {
                    if (!_extension.isEnumValid<ApiCategoryEnum>(apiCategoryEnum) || apiCategoryEnum == ApiCategoryEnum.NotAvailable)
                        response = _extension.SetResponse(false, message: "Data is Not Valid");

                    // Data Hatalı Değilse Aşağıdaki Kod Çalışsın.
                    if (response == null)
                    {
                        string url = _extension.GenerateApiUrl(apiCategoryEnum, isList);
                        var fetchedResponse = await _extension.FetchJsonData(url);

                        switch (apiCategoryEnum)
                        {
                            case ApiCategoryEnum.Summary:
                                response = _extension.ExtractJsonAsClass<Summary>((string)fetchedResponse.Data, isList).Result;
                                break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    _extension.LogException(exception);
                    response = _extension.SetResponse(false, message: _extension.getExceptionMessage(exception));
                }
            });
            return response;
        }
    }
}
