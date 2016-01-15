using System;
using System.IO;

namespace AutoType
{
    class InvalidArgumentsException : Exception { }
    class NoArgumentsException : InvalidArgumentsException { }

    enum ExitCode
    {
        OK                  =  0,
        NO_ARGUMENTS        =  1,
        INVALID_FILE_NAME   =  4,
        FILE_NOT_FOUND      =  5,
        ACCESS_DENIED       =  8,
        ERROR_READING       =  9,
    }

    class Program
    {
        static void Help()
        {
            Console.WriteLine("Help for AutoType");
            Console.WriteLine("Source Code:");
            Console.WriteLine("  http://www.github.com/benjidial/AutoType\n");
            Console.WriteLine("What it does:");
            Console.WriteLine("  Takes a text file and outputs a character from it each time you press a key.\n");
            Console.WriteLine("Command line:");
            Console.WriteLine("  AutoType --help");
            Console.WriteLine("  AutoType --version");
            Console.WriteLine("  AutoType --license");
            Console.WriteLine("  AutoType filename\n");
            Console.WriteLine("  --help      Prints this");
            Console.WriteLine("  --version   Prints the version");
            Console.WriteLine("  --license   Prints the license");
            Console.WriteLine("  filename    Specifies the filename\n");
            Console.WriteLine("Return values:");
            Console.WriteLine("   0 - OK");
            Console.WriteLine("   1 - No arguments");
            Console.WriteLine("   4 - Invalid file name");
            Console.WriteLine("   5 - File not found");
            Console.WriteLine("   8 - Access denied");
            Console.WriteLine("   9 - Error reading");
            Environment.Exit((int)ExitCode.OK);
        }

        static void Version()
        {
            Console.WriteLine("AutoType v1.0.0 https://github.com/benjidial/AutoType/releases/tag/v1.0.0");
            Environment.Exit((int)ExitCode.OK);
        }

        static void License()
        {
            Console.WriteLine("The MIT License (MIT)\n\nCopyright (c) 2016 Benji Dial\n\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\n\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\n\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");
            Environment.Exit((int)ExitCode.OK);
        }

        static void Main(string[ ] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new NoArgumentsException();
                if (args[0] == "--help")
                    Help();
                if (args[0] == "--version")
                    Version();
                if (args[0] == "--license")
                    License();
                FileStream file = new FileStream(args[0], FileMode.Open, FileAccess.Read);
                StreamReader fileAsText = new StreamReader(file);
                while (!fileAsText.EndOfStream)
                {
                    Console.ReadKey(true);
                    Console.Write((char)fileAsText.Read());
                }
                fileAsText.Close();
                Environment.Exit((int)ExitCode.OK);
            }
            catch (NoArgumentsException)
            {
                Console.WriteLine("No arguments were supplied!");
                Environment.Exit((int)ExitCode.NO_ARGUMENTS);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("That is not a valid filename.");
                Environment.Exit((int)ExitCode.INVALID_FILE_NAME);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Could not find that location.");
                Environment.Exit((int)ExitCode.FILE_NOT_FOUND);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find that file.");
                Environment.Exit((int)ExitCode.FILE_NOT_FOUND);
            }
            catch (IOException)
            {
                Console.WriteLine("An error occured trying to read the file");
                Environment.Exit((int)ExitCode.ERROR_READING);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Found the file, but did not have permission to open it.");
                Environment.Exit((int)ExitCode.ACCESS_DENIED);
            }
        }
    }
}
