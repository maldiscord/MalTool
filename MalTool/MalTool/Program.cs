using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace MalTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File
            .ReadAllLines(@"config.txt")
            .Select(x => x.Split('='))
            .Where(x => x.Length > 1)
            .ToDictionary(x => x[0].Trim(), x => x[1]);

            string defaultClient = data["defaultclient"];

            string email = "";
            string password = "";
            string queue = "";

           

            if (!File.Exists("config.txt"))
            {
                File.Create("config.txt");
                String[] defaultLines = { "defaultclient=", "targetfolder=" };
                File.WriteAllLines("config.txt", defaultLines);
            }



            Console.WriteLine("oooo     oooo            o888 ooooooooooo                      o888");
            Console.WriteLine(" 8888o   888   ooooooo    888 88  888  88 ooooooo     ooooooo   888");
            Console.WriteLine(" 8888o   888   ooooooo    888 88  888  88 ooooooo     ooooooo   888");
            Console.WriteLine(" 88  888  88 888    888   888     888   888     888 888     888 888");
            Console.WriteLine("o88o  8  o88o 88ooo88 8o o888o   o888o    88ooo88     88ooo88  o888o");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Email:");
                email = Console.ReadLine();

                Console.WriteLine("Password:");
                password = Console.ReadLine();

                Console.WriteLine("Queue:");
                queue = Console.ReadLine();

           



            if (defaultClient == "")
            {
                Console.WriteLine(@"You havent specified the default console client folder location please do so now example: C:\Users\joe\Desktop\ConsoleClientDefault");
                defaultClient = Console.ReadLine();
                string configline = File.ReadAllText(@"config.txt");
                configline = configline.Replace("defaultclient=", "defaultclient=" + defaultClient);
                File.WriteAllText(@"config.txt", configline);
            }

            void Copy(string sourceDir, string targetDir)
            {
                Directory.CreateDirectory(targetDir);

                foreach (var file in Directory.GetFiles(sourceDir))
                    File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

                foreach (var directory in Directory.GetDirectories(sourceDir))
          
                        Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
              

            }
            string targetFolder = data["targetfolder"] + @"\" + email;
            Copy(defaultClient, targetFolder);
            string MinecraftClient = File.ReadAllText(targetFolder + @"\MinecraftClient.ini");
            Console.WriteLine(targetFolder + @"\MinecraftClient.ini");
            MinecraftClient = MinecraftClient.Replace("[[EMAIL]]", email).Replace("[[PASSWORD]]", password).Replace("[[QUEUE]]", queue).Replace("QUEUENAME", "QUEUE - " + queue);
            File.WriteAllText(targetFolder + @"\MinecraftClient.ini", MinecraftClient);
            Console.WriteLine("Done....");


          

        }
    }
}
