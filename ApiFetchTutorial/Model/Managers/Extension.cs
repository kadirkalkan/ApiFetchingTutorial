using ApiFetchTutorial.Model.Entities.ProcessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static ApiFetchTutorial.Model.Enums.EnumClass;

namespace ApiFetchTutorial.Model.Managers
{
    public class Extension
    {
        private static string BASE_URL = "https://api.covid19api.com";
        public string GenerateApiUrl(ApiCategoryEnum apiCategoryEnum, bool isList)
        {
            string url = String.Empty;
            switch (apiCategoryEnum)
            {
                case ApiCategoryEnum.Summary:
                    url = String.Format("{0}/{1}", BASE_URL, "summary");
                    break;
            }
            return url;
        }


        public bool isEnumValid<T>(object data)
        {
            return GetEnum<T>(data) != null;
        }

        public T GetEnum<T>(object o)
        {
            return (T)Enum.Parse(typeof(T), o.ToString());
        }

        //URL'den json string türünde data çekilir.
        public async Task<Response> FetchJsonData(string url)
        {
            Response response = null;

            //Api'den Fetch işlemin ne kadar süreceğini gözlemlemek için bir kronometre başlatılıyor.
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                // using System.Net.Http
                using HttpClient client = new HttpClient();
                using HttpResponseMessage res = await client.GetAsync(url);
                using HttpContent content = res.Content;

                string data = await content.ReadAsStringAsync();
                if (data != null)
                    response = SetResponse(true, data);
                else
                    response = SetResponse(false, message: "Json Data is Null");
            }
            catch (Exception exception)
            {
                LogException(exception);
                response = SetResponse(false, message: getExceptionMessage(exception));
            }
            // Kronometre bitiş.
            watch.Stop();
            Console.WriteLine($"{url}  ==> Fetch Time: {watch.ElapsedMilliseconds} ms");
            return response;
        }


        //URL'den çekilen JSON string objeye dönüştürülür.
        public async Task<Response> ExtractJsonAsClass<T>(string json, bool isList = false)
        {
            Response response = null;
            // isList parametresine göre Liste türü veya obje türü dönüşümler gerçekleştirilir.
            await Task.Run(() =>
            {
                try
                {
                    if (!isList)
                    {
                        T jsonItem = JsonConvert.DeserializeObject<T>(json);
                        if (jsonItem != null)
                            response = SetResponse(true, jsonItem);
                    }
                    else
                    {
                        ICollection<T> jsonList = JsonConvert.DeserializeObject<ICollection<T>>(json);
                        if (jsonList != null)
                            response = SetResponse(true, jsonList);
                    }
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    response = SetResponse(false, message: getExceptionMessage(exception));
                }
            });
            return response;
        }



        // UI tarafında kullanırken işlem başarılımı, değilmi bunu kontrol etmek için success parametresi kullanılır. true ise data okunur false ise message içeriğindeki hata detayı,
        // veya mesaj gösterilir
        // Ör: Login ekranında kullanıcı adı şifre yanlış ise success: false, message: "Şifre Hatalı" olur;
        // Login başarılı ise success: true, data:UserObject, message:"Başarılı" olur.
        public Response SetResponse(bool success, object data = null, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                if (success)
                {
                    message = "İşlem Başarılı";
                }
                else
                    message = "İşlem Başarısız.";
            return new Response() { Data = data, Success = success, Message = message };
        }



        public string getExceptionMessage(Exception exception)
        {
            return (exception.InnerException != null) ? exception.InnerException.Message : exception.Message;
        }

        public void LogException(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string error = getExceptionMessage(exception);
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
