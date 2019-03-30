using System;
using System.IO;
using System.Text;

namespace Betacode2utf8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: betacode2utf8 infile outfile [command]");
                Console.WriteLine("Command: L - process LaTeX source file (default).");
                Console.WriteLine("             Processes only content of \\betacode{} or \\bcode{}.");
                // Console.WriteLine("Command: T - process simple text file.");
                // Console.WriteLine("             Processes everything in file.");
                Console.WriteLine("Example: betacode2utf8.exe myinfile.tex myoutfile.tex L");
                return;
            }

            Console.WriteLine("Starting...");

            var cmd = "";
            var infile = args[0];
            var outfile = args[1];
            if (args.Length < 3) { cmd = "L"; } else { cmd = args[2]; }

            if (cmd == "L") { ProcessLatex.Go(infile, outfile); }

            return;
        }




    }
}
