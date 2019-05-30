using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace UFIGenerator
{
    /// <summary>
    /// Generate UFI code based on VAT and formulation number
    /// </summary>
    public class UFI
    {
        public static string GenerateUFI(string VAT, int formulation)
        {
            if (VAT.Length == 0) return "";

            //2.1.1 Step 1 – UFI payload numerical value
            string countryCodeIso3166 = GetCountryCodeISO3166(VAT);
            long vatLong = VatToNumber(VAT, countryCodeIso3166);

            BitArray group = GetCountryGroup(countryCodeIso3166);
            BitArray country = GetCountryCode(countryCodeIso3166);
            BitArray vatn = VatNumberBits(vatLong, countryCodeIso3166);
            BitArray formulationN = formulation.ToBinary(28);
            BitArray togetherStep1 = 0.ToBinary(1).Append(vatn).Append(country).Append(group).Append(formulationN);
            //Console.WriteLine(togetherStep1.ToDigitString());


            //2.1.2 Step 2 – UFI payload in base-31
            string decimalPayloadString = BinToDec(togetherStep1.ToDigitString());
            char[] tableChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };

            BigInteger payload = BigInteger.Parse(decimalPayloadString);
            string base31PayloadStep2 = BigIntegerToStringBaseX(payload, tableChars).PadLeft(15, '0');
            //Console.WriteLine(base31PayloadStep2);

            //2.1.3 Step 3 – Character reorganisation
            string reorganisedStep3 = CharacterReorganisation(base31PayloadStep2);
            //Console.WriteLine(reorganisedStep3);


            //2.1.4 Step 4 – Checksum calculation
            int weightedSum = 0;
            for (int i = 0; i < 15; i++) weightedSum += (i + 2) * Array.IndexOf(tableChars, reorganisedStep3[i]);
            int checkSum = ((31 - weightedSum % 31) % 31);
            string UFIPlainStep4 = $"{tableChars[checkSum]}{reorganisedStep3}";
            string UFI = Regex.Replace(UFIPlainStep4, @"^(....)(....)(....)(....)$", "$1-$2-$3-$4");

            return UFI;
        }


        /// <summary>
        /// Table 2-2: Rules for VAT number conversion to numerical value
        /// </summary>
        /// <param name="VAT"></param>
        /// <returns></returns>
        internal static long VatToNumber(string VAT, string Country)
        {
            switch (Country)
            {
                case "CY":
                    return VatToNumberCy(VAT);
                case "ES":
                    return VatToNumberEs(VAT);
                case "FR":
                    return VatToNumberFr(VAT);
                case "GB":
                    return VatToNumberGb(VAT);
                case "IE":
                    return VatToNumberIe(VAT);
                case "IS":
                    return VatToNumberIs(VAT);
                default:
                    string justNumbers = new string(VAT.Where(char.IsDigit).ToArray());
                    long.TryParse(justNumbers, out long n);
                    return n;
            }
        }

        private static long VatToNumberCy(string VAT)
        {
            string decimalPart = VAT.Substring(2, 8);
            char letter = VAT.Substring(10, 1)[0];
            int letterIndex = LetterNumberCyGb(letter);

            long letterPart = letterIndex * (long)Math.Pow(10, 8);
            return letterPart + Convert.ToInt64(decimalPart);
        }

        internal static long VatToNumberEs(string VAT)
        {
            string decimalPart = VAT.Substring(3, 7);
            int c1 = LetterNumberEsFrIs(VAT.Substring(2, 1)[0]);
            int c2 = LetterNumberEsFrIs(VAT.Substring(10, 1)[0]);

            long letterPart = (36 * c1 + c2) * (long)Math.Pow(10, 7);
            return letterPart + Convert.ToInt64(decimalPart);
        }


        internal static long VatToNumberFr(string VAT)
        {
            //FRAB123456789
            string decimalPart = VAT.Substring(4, 9);
            int c1 = LetterNumberEsFrIs(VAT.Substring(2, 1)[0]);
            int c2 = LetterNumberEsFrIs(VAT.Substring(3, 1)[0]);

            long letterPart = (36 * c1 + c2) * (long)Math.Pow(10, 9);
            return letterPart + Convert.ToInt64(decimalPart);
        }


        internal static long VatToNumberGb(string VAT)
        {
            //GB123456789012
            //GBAB123

            string justNumbers = new string(VAT.Where(char.IsDigit).ToArray());
            long.TryParse(justNumbers, out long n);

            if (VAT.Length == 7)
            {
                int c1 = LetterNumberCyGb(VAT.Substring(2, 1)[0]);
                int c2 = LetterNumberCyGb(VAT.Substring(3, 1)[0]);

                long letterPart = (26 * c1 + c2) * (long)Math.Pow(10, 3);
                return letterPart + n;
            }
            else
            {
                return (long)Math.Pow(2, 40) + n;
            }

        }



        internal static long VatToNumberIe(string VAT)
        {
            Regex regexA = new Regex("[0-9][A-Z*+][0-9]{5}[A-Z]");
            Match matchA = regexA.Match(VAT);

            if (matchA.Success)
            {
                //"IE9*54321Y"
                string d = VAT.Substring(2, 1) + VAT.Substring(VAT.Length - 6, 5);
                int c1 = LetterNumberIe(VAT.Substring(3, 1)[0]);
                int c2 = LetterNumberIe(VAT.Substring(VAT.Length - 1, 1)[0]);

                long letterPart = (26 * c1 + c2) * (long)Math.Pow(10, 6);
                return letterPart + Convert.ToInt64(d);
            }
            else
            {
                //IE9876543Z
                string justNumbers = new string(VAT.Where(char.IsDigit).ToArray());
                long.TryParse(justNumbers, out long N);


                int c1 = LetterNumberCyGb(VAT.Substring(9, 1)[0]);
                int c2 = 0;
                if (VAT.Length == 11)
                    c2 = LetterNumberCyGb(VAT.Substring(10, 1)[0]);

                return ((long)Math.Pow(2, 33) + ((26 * c2 + c1) * (long)Math.Pow(10, 7) + Convert.ToInt64(N)));
            }


        }


        internal static long VatToNumberIs(string VAT)
        {
            //ISAB3D5F
            long VN = 0;
            char l1 = VAT.Substring(7, 1)[0];
            char l2 = VAT.Substring(6, 1)[0];
            char l3 = VAT.Substring(5, 1)[0];
            char l4 = VAT.Substring(4, 1)[0];
            char l5 = VAT.Substring(3, 1)[0];
            char l6 = VAT.Substring(2, 1)[0];


            VN += 36 * 36 * 36 * 36 * 36 * LetterNumberEsFrIs(l6);
            VN += 36 * 36 * 36 * 36 * LetterNumberEsFrIs(l5);
            VN += 36 * 36 *36  * LetterNumberEsFrIs(l4);
            VN += 36 * 36 * LetterNumberEsFrIs(l3);
            VN += 36 * LetterNumberEsFrIs(l2);
            VN += LetterNumberEsFrIs(l1);
            return VN;
        }


        private static int LetterNumberIe(char c)
        {
            if (c == '+') return 26;
            if (c == '*') return 27;
            return char.ToUpper(c) - 65;
        }


        private static int LetterNumberCyGb(char c)
        {
            return char.ToUpper(c) - 65;
        }

        internal static int LetterNumberEsFrIs(char c)
        {
            if (char.IsDigit(c)) return (int)char.GetNumericValue(c);
            return char.ToUpper(c) - 55;
        }


        static BitArray VatNumberBits(long VAT, string countryCode)
        {
            return VAT.ToBinary(41 - GetBitsCount(countryCode));
        }

        private static string CharacterReorganisation(string b31)
        {
            return
                $"{b31[5]}{b31[4]}{b31[3]}{b31[7]}{b31[2]}{b31[8]}{b31[9]}{b31[10]}{b31[1]}{b31[0]}{b31[11]}{b31[6]}{b31[12]}{b31[13]}{b31[14]}";
        }


        /// <summary>
        /// Binary string to decimal string
        /// 0101 => 5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string BinToDec(string value)
        {
            BigInteger res = 0;

            foreach (char c in value)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res.ToString();
        }


        /// <summary>
        /// BigInteger to any base string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseChars"></param>
        /// <returns></returns>
        private static string BigIntegerToStringBaseX(BigInteger value, IReadOnlyList<char> baseChars)
        {
            string result = string.Empty;
            int targetBase = baseChars.Count;

            do
            {
                result = baseChars[(int)(value % targetBase)] + result;
                value /= targetBase;
            }
            while (value > 0);

            return result;
        }


        private static BitArray GetCountryGroup(string countryCode)
        {
            switch (countryCode)
            {
                case "":
                    return 0.ToBinary(4);
                case "FR":
                    return 1.ToBinary(4);
                case "GB":
                    return 2.ToBinary(4);
                case "LT":
                case "SE":
                    return 3.ToBinary(4);
                case "HR":
                case "IT":
                case "LV":
                case "NL":
                    return 4.ToBinary(4);
                default:
                    return 5.ToBinary(4);
            }
        }

        private static int GetBitsCount(string countryCode)
        {
            switch (countryCode)
            {
                case "":
                case "FR":
                case "GB":
                    return 0;
                case "LT":
                case "SE":
                    return 1;
                case "HR":
                case "IT":
                case "LV":
                case "NL":
                    return 4;
                default:
                    return 7;
            }
        }

        private static BitArray GetCountryCode(string countryCode)
        {
            switch (countryCode)
            {
                default:
                case "":
                case "FR":
                case "GB":
                    return new BitArray(0);
                case "LT":
                    return 0.ToBinary(1);
                case "SE":
                    return 1.ToBinary(1);
                case "HR":
                    return 0.ToBinary(4);
                case "IT":
                    return 1.ToBinary(4);
                case "LV":
                    return 2.ToBinary(4);
                case "NL":
                    return 3.ToBinary(4);
                case "BG":
                    return 0.ToBinary(7);
                case "CZ":
                    return 1.ToBinary(7);
                case "IE":
                    return 2.ToBinary(7);
                case "ES":
                    return 3.ToBinary(7);
                case "PL":
                    return 4.ToBinary(7);
                case "RO":
                    return 5.ToBinary(7);
                case "SK":
                    return 6.ToBinary(7);
                case "CY":
                    return 7.ToBinary(7);
                case "IS":
                    return 8.ToBinary(7);
                case "BE":
                    return 9.ToBinary(7);
                case "DE":
                    return 10.ToBinary(7);
                case "EE":
                    return 11.ToBinary(7);
                case "GR":
                    return 12.ToBinary(7);
                case "NO":
                    return 13.ToBinary(7);
                case "PT":
                    return 14.ToBinary(7);
                case "AT":
                    return 15.ToBinary(7);
                case "DK":
                    return 16.ToBinary(7);
                case "FI":
                    return 17.ToBinary(7);
                case "HU":
                    return 18.ToBinary(7);
                case "LU":
                    return 19.ToBinary(7);
                case "MT":
                    return 20.ToBinary(7);
                case "SI":
                    return 21.ToBinary(7);
                case "LI":
                    return 22.ToBinary(7);
            }
        }

        /// <summary>
        /// Uppercase ISO-3166-1 alpha-2 country codes from VAT
        /// </summary>
        /// <param name="VAT"></param>
        /// <returns></returns>
        private static string GetCountryCodeISO3166(string VAT)
        {
            if (VAT.Length == 0) return "";
            if (char.IsDigit(VAT[0])) return "";

            string code = VAT.Substring(0, 2).ToUpper();
            if (code == "EL") code = "GR";
            return code;
        }


    }
}
