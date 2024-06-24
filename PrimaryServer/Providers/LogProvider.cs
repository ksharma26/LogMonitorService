using Newtonsoft.Json;

namespace LogMonitorService.Providers
{
    public class LogProvider
    {
        public async Task<List<string>> GetLogsAsync(string ip, string filename, int page, int pageSize, string keyword)
        {
            var logs = await GetLogsFromSecondary(ip, filename, page, pageSize, keyword);
            return logs;
        }

        private async Task<List<string>> GetLogsFromSecondary(string ip, string filename, int page, int pageSize, string keyword)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"http://{ip}/Log?filename={filename}&page={page}&pageSize={pageSize}&keyword={keyword}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var logs = JsonConvert.DeserializeObject<List<string>>(jsonString);
                    return logs ?? new List<string> { $"No logs found from {ip}" };
                }
                else
                {
                    return new List<string> { $"Failed to get logs from {ip}: {response.ReasonPhrase}" };
                }
            }
            catch (HttpRequestException ex)
            {
                return new List<string> { $"Failed to get logs from {ip}: {ex.Message}" };
            }
        }
    }
}
