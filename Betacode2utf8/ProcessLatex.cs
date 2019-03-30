using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Betacode2utf8
{
    class ProcessLatex
    {
        public static void Go(string infile, string outfile)
        {
            string sIn = File.ReadAllText(infile);
            string sOut = "";
            bool bBetacode=false;
            Convertor conv = new Convertor();

            //We make a loop for every chars
            int k = 0;

            do
            {
                // Detect if we are at start of betacode
                // We first test if we are not too close to end of file (last 10 chars).
                if ((k < sIn.Length - 10) && (sIn.Substring(k, 10) == "\\betacode{"))
                {
                    k += 10;
                    bBetacode = true; // we are in betacode
                }
                if ((k < sIn.Length - 7) && (sIn.Substring(k, 7) == "\\bcode{"))
                {
                    k += 7;
                    bBetacode = true; // we are in betacode
                }

                // Convert betacode and add unicode to output string
                if (bBetacode)
                {
                    int zavorka = sIn.Substring(k).IndexOf('}'); //Find closing }
                    string betacode = sIn.Substring(k, zavorka);
                    string unicode = conv.convertFromBetaCode(betacode);
                    sOut += unicode;
                    k += betacode.Length + 1; // Add 1 to skip closing }
                    bBetacode = false;
                    continue;
                }

                // Here we handle text outside the betacode
                int betaloc1 = sIn.Substring(k).IndexOf("\\betacode"); //Find next betacode
                int betaloc2 = sIn.Substring(k).IndexOf("\\bcode"); //Find next betacode
                if (betaloc1 == -1) { betaloc1 = sIn.Substring(k).Length; }
                if (betaloc2 == -1) { betaloc2 = sIn.Substring(k).Length; }
                int betaloc = System.Math.Min(betaloc1, betaloc2);

                string normaltext = sIn.Substring(k, betaloc);
                sOut += normaltext;
                k += betaloc;
                               
            } while (k < sIn.Length);

            File.WriteAllText(outfile, sOut, Encoding.UTF8);
        }
    }
}
