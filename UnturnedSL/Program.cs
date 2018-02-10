﻿using System;
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
                /*Asks user to setup their own server, which later saves in a config file, TODO: Try looping this!*/
                Console.ForegroundColor = ConsoleColor.Yellow;

                string[] setvals = { "map", "name", "welcome", "port", "data", "extralo", "path" };
                var loopval = 0;
                string[] answ = new string[7];
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
                        } else
                        {
                            loopval++;
                        }
                    }
                }
                string map = answ[0];
                string name = answ[1];
                string welcome = answ[2];
                string port = answ[3];
                string data = answ[4];
                string extralo = answ[5];
                string path = answ[6];

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
                    Console.WriteLine(Environment.NewLine + "Your settings:" +
                        Environment.NewLine + Environment.NewLine + "Server Name:" + "\t" + "\t" + name + Environment.NewLine +
                        "Running Map:" + "\t" + "\t" + map + Environment.NewLine +
                        "Welcome Msg:" + "\t" + "\t" + welcome + Environment.NewLine +
                        "Data folder:" + "\t" + "\t" + data + Environment.NewLine +
                        "Extra options:" + "\t" + "\t" + extralo + Environment.NewLine +
                        "Path to Game:" + "\t" + "\t" + path + Environment.NewLine);
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
                try { Process.Start(path + @"\Unturned.exe" + launchop);} catch { }
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
                        Console.WriteLine(Environment.NewLine + "Your settings:" +
                        Environment.NewLine + Environment.NewLine + "Server Name:" + "\t" + "\t" + name + Environment.NewLine +
                        "Running Map:" + "\t" + "\t" + map + Environment.NewLine +
                        "Welcome Msg:" + "\t" + "\t" + welcome + Environment.NewLine +
                        "Data folder:" + "\t" + "\t" + data + Environment.NewLine +
                        "Extra options:" + "\t" + "\t" + extralo + Environment.NewLine +
                        "Path to Game:" + "\t" + "\t" + path + Environment.NewLine);
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
                    Environment.Exit(0);
                }
            }
        }
    }
}
