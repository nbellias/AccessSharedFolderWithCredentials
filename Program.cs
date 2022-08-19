using System;
using System.IO;
using System.Net;

namespace AccessSharedFolderWithCredentials
{
    internal class Program
    {
        private static string networkPath { get; set; } = @"\\NMPELLIAS-PC\MyFiles";
        private static NetworkCredential credentials { get; set; } = new NetworkCredential(@"NMPELLIAS-PC\nbellias", "mypassword");
        // CASE NO PASSWORD IS USED BY USER: = new NetworkCredential(@"NMPELLIAS-PC\nbellias", null);

        static void Main(string[] args)
        {
            Console.WriteLine("Reading shared folder...");
            ReadSharedFolder();
            Console.WriteLine("...Done!");
        }

        private static void ReadSharedFolder()
        {
            
            try
            {
                using (new ConnectToSharedFolder(networkPath, credentials))
                {
                    var fileList = Directory.GetFiles(networkPath);
                    foreach (var item in fileList)
                    {
                        Console.WriteLine(item);
                        var file = Path.GetFileName(item);
                        Console.WriteLine(file);
                        try
                        {
                            byte [] fileBytes = File.ReadAllBytes(item);
                            Console.WriteLine(fileBytes.Length);
                        }
                        catch (Exception ex)
                        {
                            string Message = ex.Message.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
