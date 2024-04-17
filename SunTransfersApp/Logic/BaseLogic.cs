using System.Text.Json;

namespace SunTransfersApp.Logic
{
    public class BaseLogic
    {
        protected readonly IConfiguration _configuration;
        public BaseLogic(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<T> GetInFileAsync<T>(string file)
        {
            try
            {
                string json = File.ReadAllText($"data/{file}");
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception e)
            {

                throw e;
            }        
        }
        public async Task<bool> UpdateInFileAsync(string file, object model)
        {
            try
            {
                string output = JsonSerializer.Serialize(model);
                File.WriteAllText($"data/{file}", output);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
