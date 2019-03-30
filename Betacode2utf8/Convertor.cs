using System;
using System.Collections.Generic;
using System.Text;

namespace Betacode2utf8
{
    class Convertor
    {
        Dictionary<string, string> letters = new Dictionary<string, string>();
        Dictionary<string, string> diacritics = new Dictionary<string, string>();
        Dictionary<string, string> puncts = new Dictionary<string, string>();

        public Convertor()
        {
            //Conversions array : "BETACODE" => "UNICODE-HEX"
            //I made three different arrays for now : $letters, $diacritics and $puncts
            letters.Add("*A", "0391");
            letters.Add("A" , "03B1");
            letters.Add("*B" , "0392");
            letters.Add("B" , "03B2");
            letters.Add("*C" , "039E");
            letters.Add("C" , "03BE");
            letters.Add("*D" , "0394");
            letters.Add("D" , "03B4");
            letters.Add("*E" , "0395");
            letters.Add("E" , "03B5");
            letters.Add("*F" , "03A6");
            letters.Add("F" , "03C6");
            letters.Add("*G" , "0393");
            letters.Add("G" , "03B3");
            letters.Add("*H" , "0397");
            letters.Add("H" , "03B7");
            letters.Add("*I" , "0399");
            letters.Add("I" , "03B9");
            letters.Add("*K" , "039A");
            letters.Add("K" , "03BA");
            letters.Add("*L" , "039B");
            letters.Add("L" , "03BB");
            letters.Add("*M" , "039C");
            letters.Add("M" , "03BC");
            letters.Add("*N" , "039D");
            letters.Add("N" , "03BD");
            letters.Add("*O" , "039F");
            letters.Add("O" , "03BF");
            letters.Add("*P" , "03A0");
            letters.Add("P" , "03C0");
            letters.Add("*Q" , "0398");
            letters.Add("Q" , "03B8");
            letters.Add("*R" , "03A1");
            letters.Add("R" , "03C1");
            letters.Add("*S" , "03A3");
            letters.Add("S" , "03C3");
            letters.Add("S1" , "03C3");
            letters.Add("S2" , "03C2");
            letters.Add("*S3" , "03F9");
            letters.Add("S3" , "03F2");
            letters.Add("*T" , "03A4");
            letters.Add("T" , "03C4");
            letters.Add("*U" , "03A5");
            letters.Add("U" , "03C5");
            letters.Add("*V" , "03DC");
            letters.Add("V" , "03DD");
            letters.Add("*W" , "03A9");
            letters.Add("W" , "03C9");
            letters.Add("*X" , "03A7");
            letters.Add("X" , "03C7");
            letters.Add("*Y" , "03A8");
            letters.Add("Y" , "03C8");
            letters.Add("*Z" , "0396");
            letters.Add("Z" , "03B6");

            diacritics.Add(")", "0313");
            diacritics.Add("(" , "0314");
            diacritics.Add("/" , "0301");
            diacritics.Add("=" , "0342");
            diacritics.Add("\\" , "0300");
            diacritics.Add("!", "0300");
            diacritics.Add("+" , "0308");
            diacritics.Add("|" , "0345");
            diacritics.Add("?" , "0323");

            puncts.Add("." , "002E");
            puncts.Add("," , "002C");
            puncts.Add(":" , "00B7");
            puncts.Add(";" , "003B");
            puncts.Add("'" , "2019");
            puncts.Add("-" , "2010");
            puncts.Add("_" , "2014");
        }
 
        //Return the UTF8 char from the Hex Code
        public static char utf8(string hex)
        {
            byte[] bytes = HexToBytes(hex);
            char c;
            if (bytes.Length == 1)
            {
                c = (char)bytes[0];
            }
            else if (bytes.Length == 2)
            {
                c = (char)((bytes[0] << 8) + bytes[1]);
            }
            else
            {
                throw new Exception(hex);
            }

            return c;
        }

        public static byte[] HexToBytes(string hex)
        {
            hex = hex.Trim();

            byte[] bytes = new byte[hex.Length / 2];

            for (int index = 0; index < bytes.Length; index++)
            {
                bytes[index] = byte.Parse(hex.Substring(index * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }

         public string convertFromBetaCode(string s) // $s as string to convert from BETACODE to HEX
        {
            // Problems where e\n is understood as ~ "e<br />"
            //s.Replace("\n", "\\n");
            //s.Replace("\r", "\\r");

            string end = "";//This var is used for Majs : diacritics are written before majs so we create a var to add after the maj. 
            string rstring = "";//Set the return string
            string temp, velkepismeno;

            for (int k = 0; k < s.Length; k++)//We make a loop for every chars
            {
                //We go to upper because BETACODE is written for uppercase
                string v = (s.Substring(k, 1)).ToUpper();

                //If there's a *, it's a maj. * can't be treated alone
                if (v == "*")
                {
                    int next = k + 1; //Next char ID
                                      //If next char is a letter
                    string vi = (s.Substring(next, 1)).ToUpper();

                    if (letters.TryGetValue(vi, out temp))
                    {
                        rstring = rstring + utf8(temp);
                        k = next; //We go to next char
                    }
                    //If next char is diacritics, as diac are written before letter when it's uppercase
                    else if (diacritics.TryGetValue(vi, out temp))
                    {
                        //We make a new loop until we find an uppercase char
                        for (int k2 = k; k2 < s.Length; k2++)
                        {
                            string v2 = s.Substring(k2, 1).ToUpper();
                            if (diacritics.TryGetValue(v2, out temp))
                            {   // Yes it's diacritic
                                //We add $end the diacritics char
                                end = end + utf8(temp);
                            }
                            else if (letters.TryGetValue(v2, out temp))
                            {   // If it's a letter
                                // We print the maj with $end var which contain 
                                // every diacritics for this letter
                                if (letters.TryGetValue("*" + v2, out velkepismeno))
                                {
                                    rstring = rstring + utf8(velkepismeno) + end;
                                    k = k2;
                                    end = "";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        rstring = string.Concat(rstring, v, s.Substring(k + 1, 1));
                    }
                }
                //If it's not a maj => Putting $k!=0 to be sure that $s will return something
                else if ((k==0) || (k != 0) && (s.Substring(k - 1, 1) != "*"))
                {
                    //If we have a latter
                    if (letters.TryGetValue(v, out temp))
                    {
                        rstring = rstring + utf8(temp); //We add to the string
                    }
                    //Diacritics
                    else if (diacritics.TryGetValue(v, out temp))
                    {
                        rstring = rstring + utf8(temp);
                    }
                    //Punctuation
                    else if (puncts.TryGetValue(v, out temp))
                    {
                        rstring = rstring + utf8(temp);
                    }
                    else
                    //If we don't know the char, lets print it for debug
                    {
                        rstring = rstring + v;
                    }
                }
            }
            //End of Loop
            //We return the value

            return rstring;
            
        }
    }
}
