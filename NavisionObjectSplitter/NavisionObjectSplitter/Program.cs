﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NavisionObjectSplitter
{
    class Program
    {
        private static readonly string MATCHSTRING = @"(OBJECT \w* \d* .*
{)"; //[A-z 0-9]
        public static void Main(string[] args)
        {
            string fileName;
            if (args.Length == 1)
            {
                fileName = args[0];
            }
            else
            {
                Console.Write("Filename: ");
                fileName = Console.ReadLine();
            }
            var encoding = Encoding.Default;

            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open), encoding))
            {
                var fullFileContent = sr.ReadToEnd();
                var values = Regex.Split(fullFileContent, MATCHSTRING)
                    .AsEnumerable()
                    .Skip(1).ToArray();

                for (int i = 0; i < values.Length; i += 2)
                {
                    var name = values[i].Split(Environment.NewLine.ToCharArray())[0].Trim(' ', '"');
                    File.WriteAllText(name.Split(' ').Take(3).Aggregate((x, y) => x + " " + y) + ".txt", values[i] + values[i + 1], encoding);
                    Console.WriteLine(name);
                }
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
