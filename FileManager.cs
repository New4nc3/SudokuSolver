namespace InputOutput
{
    static class FileManager
    {
        private const string InputFolderName = "input";
        private const string OutputFolderName = "output";
        private const string OutSuffix = "_out";
        private const string TxtExt = ".txt";

        public static string[] ReadDataFromFile(string inputFileName)
        {
            using (var streamReader = new StreamReader(inputFileName))
                return streamReader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool WriteDataToFile(string outputFileName, string data)
        {
            try 
            {
                using (var streamWriter = new StreamWriter(outputFileName, false))
                    streamWriter.Write(data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateInputFilePath(string inputFileName) =>
            Path.Combine(InputFolderName, $"{inputFileName}{TxtExt}");

        public static string GenerateOutputFilePath(string outputFileName) =>
            Path.Combine(OutputFolderName, $"{outputFileName}{OutSuffix}{TxtExt}");
    }
}