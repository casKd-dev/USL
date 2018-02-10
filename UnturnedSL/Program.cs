using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace UnturnedSL
{
    class Launcher
    {
        static void Main(string[] args)
        {
            string lauversion = "1.1 rewrite";
            string title = "USL by casKd running on version " + lauversion;
            Console.Title = title;
            Console.SetWindowSize(100,20);
            /*Console.WriteLine("Currently running from:" + Environment.NewLine + Directory.GetCurrentDirectory() + Environment.NewLine +
             "Config exists: " + File.Exists("settings.cfg") + Environment.NewLine); DEBUGGING PURPOSES ONLY*/
            if (!File.Exists("settings.cfg")) { /*Checks for new users*/
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome!", Environment.NewLine);
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
                /*Asks user to setup their own server, which later saves in a config file*/
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(question[0] + Environment.NewLine + "Default value: " + defvalue[0]);
                string map = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(map))
                {
                    map = defvalue[0];
                }
                Console.Clear();
                Console.WriteLine(question[1] + Environment.NewLine + "Default value: " + defvalue[1]);
                string name = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(name))
                {
                    name = defvalue[1];
                }
                Console.Clear();
                Console.WriteLine(question[2] + Environment.NewLine + "Default value: " + defvalue[2]);
                string welcome = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(welcome))
                {
                    welcome = defvalue[2];
                }
                Console.Clear();
                Console.WriteLine(question[3] + Environment.NewLine + "Default value: " + defvalue[3]);
                string port = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(port))
                {
                    port = defvalue[3];
                }
                Console.Clear();
                Console.WriteLine(question[4] + Environment.NewLine + "Default value: " + defvalue[4]);
                string data = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(data))
                {
                    data = defvalue[4];
                }
                Console.Clear();
                Console.WriteLine(question[5] + Environment.NewLine + "Default value: " + defvalue[5]);
                string extralo = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(extralo))
                {
                    extralo = defvalue[5];
                }
                Console.Clear();
                Console.WriteLine(question[6] + Environment.NewLine + "Default value: " + defvalue[6]);
                string path = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(path))
                {
                    path = defvalue[6];
                }
                Console.Clear();
                TextWriter settings = new StreamWriter("settings.cfg", true);
                settings.WriteLine(name);
                settings.WriteLine(map);
                settings.WriteLine(welcome);
                settings.WriteLine(port);
                settings.WriteLine(data);
                settings.WriteLine(extralo);
                settings.WriteLine(path);
                settings.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                string launchop = "-nographics -batchmode -name " + "\"" + name + "\"" + " -map " + map + " -welcome " + "\"" + welcome + "\"" + " -port:" + port + " " + extralo + " +secureserver/" + data;
                string curdirargs = path + @"\Unturned.exe " + launchop;
                if (
                    String.IsNullOrWhiteSpace(name) ||
                    String.IsNullOrWhiteSpace(map) ||
                    String.IsNullOrWhiteSpace(welcome) ||
                    String.IsNullOrWhiteSpace(port) ||
                    String.IsNullOrWhiteSpace(data) ||
                    String.IsNullOrWhiteSpace(extralo) ||
                    String.IsNullOrWhiteSpace(path) ||
                    !File.Exists(path + @"\Unturned.exe")
                )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oh, noes! Seems like the settings file contains wrong info." + Environment.NewLine +
                        "Try deleting it or correcting it!" + Environment.NewLine + Environment.NewLine +
                        "If you still get the problem, report it on GitHub!");
                    Console.Beep(2300, 250);
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                string displaytext =
                    "Server info:" + Environment.NewLine + Environment.NewLine +
                    "Server Name:" + "\t" + "\t" + name + Environment.NewLine +
                    "Running Map:" + "\t" + "\t" + map + Environment.NewLine +
                    "Welcome Msg:" + "\t" + "\t" + welcome + Environment.NewLine +
                    "Data folder:" + "\t" + "\t" + data + Environment.NewLine +
                    "Extra options:" + "\t" + "\t" + extralo + Environment.NewLine +
                    "Path to Game:" + "\t" + "\t" + path + Environment.NewLine;
                Console.WriteLine(displaytext);
                try { Process.Start(path + @"\Unturned.exe");} catch { }
                Console.ReadKey();
            } else {
                TextReader settings = new StreamReader("settings.cfg", true);
                string name = settings.ReadLine();
                string map = settings.ReadLine();
                string welcome = settings.ReadLine();
                string port = settings.ReadLine();
                string data = settings.ReadLine();
                string extralo = settings.ReadLine();
                string path = settings.ReadLine();
                settings.Close(); /*Loads settings from file and checks if all lines are present*/
                if (
                    String.IsNullOrWhiteSpace(name) ||
                    String.IsNullOrWhiteSpace(map) ||
                    String.IsNullOrWhiteSpace(welcome) ||
                    String.IsNullOrWhiteSpace(port) ||
                    String.IsNullOrWhiteSpace(data) ||
                    String.IsNullOrWhiteSpace(extralo) ||
                    String.IsNullOrWhiteSpace(path) ||
                    !File.Exists(path + @"\Unturned.exe")
                    )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oh, noes! Seems like the settings file contains wrong info." + Environment.NewLine +
                        "Try deleting it or correcting it!" + Environment.NewLine + Environment.NewLine +
                        "If you still get the problem, report it on GitHub!" );
                    Console.Beep(2300, 250);
                    Console.ReadKey();
                    Environment.Exit(1);
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Welcome back!" + Environment.NewLine);
                    string displaytext =
                        "Server info:" + Environment.NewLine + Environment.NewLine +
                        "Server Name:" + "\t" + "\t" + name + Environment.NewLine +
                        "Running Map:" + "\t" + "\t" + map + Environment.NewLine +
                        "Welcome Msg:" + "\t" + "\t" + welcome + Environment.NewLine +
                        "Data folder:" + "\t" + "\t" + data + Environment.NewLine +
                        "Extra options:" + "\t" + "\t" + extralo + Environment.NewLine +
                        "Path to Game:" + "\t" + "\t" + path + Environment.NewLine;
                    Console.WriteLine(displaytext);
                    string launchop = "-nographics -batchmode -name " + "\"" + name + "\"" + " -map " + map + " -welcome " + "\"" + welcome + "\"" + " -port:" + port + " " + extralo + " +secureserver/" + data;
                    try { Process.Start(path + @"\Unturned.exe ", launchop);} catch { }
                    Console.ReadKey();
                }
            }
        }
    }
}
