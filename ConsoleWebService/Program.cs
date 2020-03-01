using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleWebService
{
    class Program
    {
        HttpClient client = new HttpClient();       

        static async Task Main(string[] args)
        {
            try
            {
                Program program = new Program();
                await program.getItens();

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao consimuir a api " + e.Message);
            }
        }

        private async Task getItens()
        {
            try
            {
                //Faço uma chamada do tipo GET para retornar o json
                HttpWebRequest req = WebRequest.CreateHttp("https://swapi.co/api/films/");
                req.Method = "GET";
                req.Timeout = 10000;
                req.KeepAlive = true;

                string content = null;
                HttpStatusCode code = HttpStatusCode.OK;

                //Retorno o Status da chamada
                using (HttpWebResponse _resp = (HttpWebResponse)await req.GetResponseAsync())
                {
                    using (StreamReader sr = new StreamReader(_resp.GetResponseStream()))
                        content = await sr.ReadToEndAsync();

                    code = _resp.StatusCode;

                    Console.WriteLine("Status Code: " + code);
                    
                    //Insiro o resultado da chamada em uma lista
                    Rootobject myProduct = JsonConvert.DeserializeObject<Rootobject>(content);
                       
                    //Listo os itens da estrutura Results
                    foreach (var item in myProduct.results)
                    {                      
                      if (item.director.ToString() == "George Lucas" && item.producer.Contains("Rick McCallum"))
                        {
                            Console.WriteLine(item.title);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
      
}
