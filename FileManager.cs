namespace InputOutput
{
    static class FileManager
    {
        public const string InputFolderName = "input";
        public const string OutputFolderName = "output";

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

        public static string GenerateInputFilePath(this string inputFileName) =>
            Path.Combine(InputFolderName, inputFileName);

        public static string GenerateOutputFilePath(this string outputFileName) =>
            Path.Combine(OutputFolderName, outputFileName);
    }
}