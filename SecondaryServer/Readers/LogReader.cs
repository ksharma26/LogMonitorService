namespace LogMonitorService.Readers
{
    public class LogReader
    {
        //for testing on windows
        //private readonly string logDirectory = "C:\\Users\\karasha\\Desktop"; 

        private readonly string logDirectory = "/var/log";

        public async Task<List<string>> GetLogLinesAsync(string filename, int page, int pageSize, string keyword)
        {
            string filePath = Path.Combine(logDirectory, filename);

            Console.WriteLine("Karan log reader: " + filePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Log file not found.");
            }

            // First pass: count the total number of matching lines
            int totalMatchingLines = await CountMatchingLinesAsync(filePath, keyword);
            if (totalMatchingLines == 0)
            {
                return new List<string>();
            }

            // Calculate the starting line number for the requested page
            int startLine = totalMatchingLines - (page * pageSize);
            if (startLine < 0)
            {
                startLine = 0;
            }

            // Second pass: read only the required lines
            var lines = await ReadLogLinesAsync(filePath, startLine, pageSize, keyword);
            return lines;
        }

        private async Task<int> CountMatchingLinesAsync(string filePath, string keyword)
        {
            int count = 0;
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(keyword) || line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            return count;
        }

        private async Task<List<string>> ReadLogLinesAsync(string filePath, int startLine, int pageSize, string keyword)
        {
            var lines = new List<string>();
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);

            int currentLine = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(keyword) || line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    if (currentLine >= startLine && lines.Count < pageSize)
                    {
                        lines.Add(line);
                    }
                    currentLine++;
                }
            }
            lines.Reverse();
            return lines;
        }
    }
}
