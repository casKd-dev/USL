using System;
using System.IO;
using System.Diagnostics;

namespace UnturnedSL
{
    class Launcher
    {
        static void Main(string[] args)
        {
            string lauversion = "1.14";
            string title = "USL by casKd running on version " + lauversion;
            Console.Title = title;
            Console.SetWindowSize(100,20);
            MkDirIfNotExist("config");
            if (Directory.GetFiles("config", "*.cfg").Length == 0) {FirstSetup(); } /*Checks for new users*/
            string[] oFiles = Directory.GetFiles("config", "*.cfg");
            /*Loads settings from file and checks if all lines are present and valid*/
            TextReader settings = new StreamReader(oFiles[0], true);
            string name = settings.ReadLine();
            string map = settings.ReadLine();
            string welcome = settings.ReadLine();
            string port = settings.ReadLine();
            string data = settings.ReadLine();
            string extralo = settings.ReadLine();
            string path = settings.ReadLine();
            settings.Close();
            Validation(name, map, welcome, port, data, extralo, path, out bool valid);
            Run(valid, name, map, welcome, port, data, extralo, path);
        }

        static void Validation(string name, string map, string welcome, string port, string data, string extralo, string path,out bool valid)
        /*Validate if all input is correct*/
        {
            /*Checks if every condition is true*/
            bool bNum = int.TryParse(port, out int i);
            if (
                bNum != true ||
                String.IsNullOrWhiteSpace(name) ||
                String.IsNullOrWhiteSpace(map) ||
                String.IsNullOrWhiteSpace(welcome) ||
                String.IsNullOrWhiteSpace(port) ||
                String.IsNullOrWhiteSpace(data) ||
                String.IsNullOrWhiteSpace(extralo) ||
                String.IsNullOrWhiteSpace(path) ||
                !File.Exists(path + @"\Unturned.exe")
            )
            { valid = true;} else { valid = false;}
        }

        static void FirstSetup()
        /*First setup*/
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome!" + Environment.NewLine + "Do you want to go through the setup? [N/y]" + Environment.NewLine);
            string[] defvalue = {
                    "PEI",
                    "USL-My server",
                    "Hey! Welcome to our server!",
                    "27015",
                    "Server_Data",
                    "-perspective both",
                    @"C:\Program Files\Steam\SteamApps\Common\Unturned" };
            string[] question = {
                    "Which map would you like to play?",
                    "What name do you wish your server to be called?",
                    "What welcome message would you want to have?",
                    "What port do you want your server to run on?",
                    "What folder name do you want to assign to your server?",
                    "Do you want any other parameters for your server?",
                    "Where is your Unturned installation located?"
            };
            string[] setvals = { "map", "name", "welcome", "port", "data", "extralo", "path" };
            var loopval = 0;
            string[] answ = new string[7];
            /*Asks user to setup their own server, which later saves in a config file*/
            if (String.Equals(Console.ReadLine(), "y", StringComparison.CurrentCultureIgnoreCase) == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                foreach (string val in setvals)
                {
                    while (loopval <= 6)
                    {
                        Console.WriteLine(question[loopval] + Environment.NewLine + "Default value: " + defvalue[loopval]);
                        answ[loopval] = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(answ[loopval]))
                        {
                            answ[loopval] = defvalue[loopval];
                            loopval++;
                        }
                        else
                        {
                            loopval++;
                        }
                        Console.Clear();
                    }
                }
            } else {
                foreach (string val in setvals)
                {
                    while (loopval <= 6)
                    {
                        answ[loopval] = defvalue[loopval];
                        loopval++;
                    }
                }
            }
            /*Transfers data from array to variables*/
            string map = answ[0];
            string name = answ[1];
            string welcome = answ[2];
            string port = answ[3];
            string data = answ[4];
            string extralo = answ[5];
            string path = answ[6];
            Console.Clear();
            /*Stores data into a file*/
            TextWriter settings = new StreamWriter("config/settings.cfg", true);
            settings.WriteLine(name);
            settings.WriteLine(map);
            settings.WriteLine(welcome);
            settings.WriteLine(port);
            settings.WriteLine(data);
            settings.WriteLine(extralo);
            settings.WriteLine(path);
            settings.Close();
        }

        static void DisplayText(string name, string map, string welcome, string port, string data, string extralo, string path) 
        /*Outputs info of the current configuration*/
        {
            string displaytext =
                    "Server info:" + Environment.NewLine + Environment.NewLine +
                    "Server Name:" + "\t" + "\t" + name + Environment.NewLine +
                    "Running Map:" + "\t" + "\t" + map + Environment.NewLine +
                    "Welcome Msg:" + "\t" + "\t" + welcome + Environment.NewLine +
                    "Data folder:" + "\t" + "\t" + data + Environment.NewLine +
                    "Running port:" + "\t" + "\t" + port + Environment.NewLine +
                    "Extra options:" + "\t" + "\t" + extralo + Environment.NewLine +
                    "Path to Game:" + "\t" + "\t" + path + Environment.NewLine;
            Console.WriteLine(displaytext);
        }

        static void DebugInfo()
        /*Helps at debugging, mostly needed when having problems*/
        {
            string debugtext = 
                "Currently running from:" + Environment.NewLine +
                Directory.GetCurrentDirectory() + Environment.NewLine +
                "Config exists: " + File.Exists("config/settings.cfg") + Environment.NewLine;
            Console.WriteLine(debugtext);
        }

        static void Run(bool valid, string name, string map, string welcome, string port, string data, string extralo, string path)
        {
            if (valid == true)
            /*If conditions have problems, output errors and variables, else, start the server*/
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh, noes! Seems like the settings file contains wrong info." + Environment.NewLine +
                    "Try deleting it or correcting it!" + Environment.NewLine + Environment.NewLine +
                    "If you still get the problem, report it on GitHub!");
                DisplayText(name, map, welcome, data, port, extralo, path);
                Console.Beep(2300, 250);
                Console.ReadKey();
                Environment.Exit(1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome!" + Environment.NewLine);
                DisplayText(name, map, welcome, data, port, extralo, path);
                string launchop = "-nographics -batchmode -name " + "\"" + name + "\"" + " -map " + map + " -welcome " + "\"" + welcome + "\"" + " -port:" + port + " " + extralo + " +secureserver/" + data;
                Process.Start(path + @"\Unturned.exe ", launchop);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        static void MkDirIfNotExist(string name)
            /*This method name explains itself*/
        {
            if (Directory.Exists(name)) { }
            else { Directory.CreateDirectory(name); }
        }
    }
}
