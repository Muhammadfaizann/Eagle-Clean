using System;
//using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.CompilerServices;

using System.Collections.Generic;
using System.Text;

namespace Custodian.Helpers
{
    /// <summary>
    /// Helper class providing implementation of the various checksums used when validating barcodes, like MOD10, etc.
    /// </summary>
    class CheckSum
    {
        private class Conversion
        {
            public static string Str(object Number)
            {
                return Number.ToString();
            }
            public static double Val(string InputStr)
            {
                return double.Parse(InputStr);
            }
            public static double Int(double Number)
            {
                return ((int)Number);
            }
        }
        private class Conversions
        {
            public static string ToString(int value)
            {
                return value.ToString();
            }
            public static string ToString(double value)
            {
                return value.ToString();
            }
            public static double ToDouble(string value)
            {
                return double.Parse(value);
            }
        }
        private class Strings
        {
            public static int Len(string Expression)
            {
                return Expression.Length;
            }
            public static string Mid(string str, int Start, int Length)
            {
                if (Start > str.Length)
                    return "";

                Start--; //1 based
                Length = Math.Min(Length, str.Length - Start);
                return str.Substring(Start, Length);
            }
            public static int Asc(string String)
            {
                return (int)String[0];
            }
            public static string Format(object Expression, string Style)
            {
                return Style;
            }
        }



        private string BarcodeID;
        public string CheckDigit;
        private int DigitValue;
        private int Even;
        private int EvenSum;
        private int Length;
        private string LetterValue;
        private string ModBarcodeID;
        private int Multiplier;
        private int Odd;
        private int OddSum;
        private string Spaces;
        private int TotalSum;

      

        public void CalculateMod10CheckDigit(string Output, string Pattern)
        {
            int num5;
            this.Spaces = "                                  ";
            this.Even = 0;
            this.Odd = 0;
            this.EvenSum = 0;
            this.OddSum = 0;
            this.TotalSum = 0;
            this.CheckDigit = "1";
            this.Length = 0;
            if (Pattern == "91000") //??
            {
                string str2 = Conversions.ToString(Strings.Len(Output));
                string str = "";
                if (Conversions.ToDouble(str2) > 0.0)
                {
                    str = Strings.Mid(Output, 3, (int)Math.Round((double)(Conversions.ToDouble(str2) - 2.0)));
                }
                this.BarcodeID = str + this.CheckDigit;
            }
            else
            {
                this.BarcodeID = Output + this.CheckDigit;
            }
            this.BarcodeID = this.Spaces + this.BarcodeID;
            this.BarcodeID = this.BarcodeID.Substring(this.BarcodeID.Length - 0x22);
            this.ModBarcodeID = "";
            int start = 1;
            do
            {
                int num4 = Strings.Asc(Strings.Mid(this.BarcodeID, start, 1));
                if ((num4 >= 0x30) && (num4 <= 0x39))
                {
                    this.ModBarcodeID = this.ModBarcodeID + Strings.Mid(this.BarcodeID, start, 1);
                }
                else if ((num4 >= 0x41) && (num4 <= 90))
                {
                    //this.ModBarcodeID = this.ModBarcodeID;
                    this.LetterValue = Conversion.Str(Strings.Asc(Strings.Mid(this.BarcodeID, start, 1)) - 0x20);
                    this.LetterValue = this.LetterValue.Substring(this.LetterValue.Length - 2);
                    this.ModBarcodeID = this.ModBarcodeID + this.LetterValue;
                }
                else if ((num4 >= 0x61) && (num4 <= 0x7a))
                {
                    this.LetterValue = Conversion.Str(Strings.Asc(Strings.Mid(this.BarcodeID, start, 1)) - 0x20);
                    this.LetterValue = this.LetterValue.Substring(this.LetterValue.Length - 2);
                    this.ModBarcodeID = this.ModBarcodeID + this.LetterValue;
                }
                else
                {
                    this.ModBarcodeID = this.ModBarcodeID + "";
                }
                start++;
                num5 = 0x22;
            }
            while (start <= num5);
            this.Length = Strings.Len(this.ModBarcodeID);
            int num2 = this.Length - 1;
        Label_02CE:
            num5 = 1;
            if (num2 >= num5)
            {
                this.Even = (int)Math.Round(Conversion.Val(Strings.Mid(this.ModBarcodeID, num2, 1)));
                this.EvenSum += this.Even;
                num2 += -2;
                goto Label_02CE;
            }
            this.EvenSum *= 3;
            int num3 = this.Length - 2;
        Label_032A:
            num5 = 1;
            if (num3 >= num5)
            {
                this.Odd = (int)Math.Round(Conversion.Val(Strings.Mid(this.ModBarcodeID, num3, 1)));
                this.OddSum += this.Odd;
                num3 += -2;
                goto Label_032A;
            }
            this.TotalSum = this.EvenSum + this.OddSum;
            this.CheckDigit = Conversions.ToString((double)(this.TotalSum - (10.0 * Conversion.Int((double)(((double)this.TotalSum) / 10.0)))));
            this.CheckDigit = Conversions.ToString((double)(10.0 - Conversions.ToDouble(this.CheckDigit)));
            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {
                this.CheckDigit = Conversions.ToString(0);
            }
            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {
                this.CheckDigit = Conversions.ToString(0);
            }
        }

        public void CalculateMod11WeightedCheckDigit(string Output)
        {
            int num3;
            this.Spaces = "        ";
            this.TotalSum = 0;
            this.CheckDigit = "1";
            this.Multiplier = 0;
            this.BarcodeID = Output + this.CheckDigit;
            this.BarcodeID = this.Spaces + this.BarcodeID;
            this.BarcodeID = this.BarcodeID.Substring(this.BarcodeID.Length - 9);
            int start = 1;
            do
            {
                this.DigitValue = (int)Math.Round(Conversion.Val(Strings.Mid(this.BarcodeID, start, 1)));
                switch (start)
                {
                    case 1:
                        this.Multiplier = 8;
                        break;

                    case 2:
                        this.Multiplier = 6;
                        break;

                    case 3:
                        this.Multiplier = 4;
                        break;

                    case 4:
                        this.Multiplier = 2;
                        break;

                    case 5:
                        this.Multiplier = 3;
                        break;

                    case 6:
                        this.Multiplier = 5;
                        break;

                    case 7:
                        this.Multiplier = 9;
                        break;

                    case 8:
                        this.Multiplier = 7;
                        break;
                }
                this.TotalSum += this.Multiplier * this.DigitValue;
                start++;
                num3 = 8;
            }
            while (start <= num3);
            this.CheckDigit = Conversions.ToString((int)(this.TotalSum % 11));
            this.CheckDigit = Conversions.ToString((double)(11.0 - Conversions.ToDouble(this.CheckDigit)));
            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {
                this.CheckDigit = Conversions.ToString(0);
            }
            if (Conversions.ToDouble(this.CheckDigit) == 11.0)
            {
                this.CheckDigit = Conversions.ToString(5);
            }
            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {
                this.CheckDigit = Conversions.ToString(0);
            }
        }

        public void CalculateMod7CheckDigit(string Output)
        {
            this.TotalSum = 0;
            this.TotalSum = (int)Math.Round(Conversion.Val(Output));
            this.CheckDigit = Conversions.ToString(Conversion.Int((double)(((double)this.TotalSum) / 7.0)));
            this.CheckDigit = Conversions.ToString((double)(this.TotalSum - (Conversions.ToDouble(this.CheckDigit) * 7.0)));
        }

        public void CalculateMod99(string InStrMod99, ref string OutStrMod99)
        {
            this.TotalSum = (int)Math.Round(Conversion.Val(InStrMod99));
            OutStrMod99 = Conversions.ToString((int)(this.TotalSum % 0x63));
            if (Conversions.ToDouble((string)OutStrMod99) == 99.0)
            {
                OutStrMod99 = "00";
            }
            else
            {
                OutStrMod99 = Strings.Format(OutStrMod99, "00");
            }
        }



        public void CalculateModifiedMod10CheckDigit(string Output)
        {

            int num6;

            this.Even = 0;

            this.Odd = 0;

            string str = Conversions.ToString(0);

            string str3 = Conversions.ToString(0);

            string str4 = Conversions.ToString(0);

            this.CheckDigit = "1";

            this.Length = 0;

            this.Length = Strings.Len(Output);

            string str2 = "";

            int length = this.Length;

            for (int i = 1; i <= length; i++)
            {

                switch (Strings.Asc(Strings.Mid(Output, i, 1)))
                {

                    case 0x41:

                    case 0x42:

                    case 0x43:

                    case 0x44:

                    case 0x45:

                    case 0x46:

                    case 0x47:

                    case 0x48:

                        this.LetterValue = Conversions.ToString((int)((Strings.Asc(Strings.Mid(Output, i, 1)) - 0x41) + 2));

                        str2 = str2 + this.LetterValue;

                        break;


                    case 0x49:

                    case 0x4a:

                    case 0x4b:

                    case 0x4c:

                    case 0x4d:

                    case 0x4e:

                    case 0x4f:

                    case 0x50:

                    case 0x51:

                    case 0x52:

                        this.LetterValue = Conversions.ToString((int)(Strings.Asc(Strings.Mid(Output, i, 1)) - 0x49));

                        str2 = str2 + this.LetterValue;

                        break;


                    case 0x53:

                    case 0x54:

                    case 0x55:

                    case 0x56:

                    case 0x57:

                    case 0x58:

                    case 0x59:

                    case 0x5a:

                        this.LetterValue = Conversions.ToString((int)(Strings.Asc(Strings.Mid(Output, i, 1)) - 0x53));

                        str2 = str2 + this.LetterValue;

                        break;


                    default:

                        str2 = str2 + Strings.Mid(Output, i, 1);

                        break;

                }

            }

            int start = this.Length;

        Label_01CC:

            num6 = 1;

            if (start >= num6)
            {

                this.Odd = (int)Math.Round(Conversion.Val(Strings.Mid(str2, start, 1)));

                str3 = Conversions.ToString((double)(Conversions.ToDouble(str3) + this.Odd));

                start += -2;

                goto Label_01CC;

            }

            int num3 = this.Length - 1;

        Label_0217:

            num6 = 1;

            if (num3 >= num6)
            {

                this.Even = (int)Math.Round(Conversion.Val(Strings.Mid(str2, num3, 1)));

                str = Conversions.ToString((double)(Conversions.ToDouble(str) + this.Even));

                num3 += -2;

                goto Label_0217;

            }

            str4 = Conversions.ToString((double)(Conversions.ToDouble(str) * 2.0) + Conversions.ToDouble(str3));

            this.CheckDigit = Conversions.ToString((double)(Conversions.ToDouble(str4) - (10.0 * Conversion.Int((double)(Conversions.ToDouble(str4) / 10.0)))));

            this.CheckDigit = Conversions.ToString((double)(10.0 - Conversions.ToDouble(this.CheckDigit)));

            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {

                this.CheckDigit = Conversions.ToString(0);

            }

            //Don't need twice?

            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {

                this.CheckDigit = Conversions.ToString(0);

            }

        }



        public void CalculateTMod10CheckDigit(string Output)
        {

            int num3;

            this.Even = 0;

            this.Odd = 0;

            string str = Conversions.ToString(0);

            string str3 = Conversions.ToString(0);

            string str4 = Conversions.ToString(0);

            this.CheckDigit = "1";

            this.Length = 0;

            string expression = Conversions.ToString(0);

            expression = Strings.Mid(Output, 8, 14);

            this.Length = Strings.Len(expression);

            int length = this.Length;

        Label_0093:

            num3 = 1;

            if (length >= num3)
            {

                this.Even = (int)Math.Round(Conversion.Val(Strings.Mid(expression, length, 1)));

                str = Conversions.ToString((double)(Conversions.ToDouble(str) + this.Even));

                length += -2;

                goto Label_0093;

            }

            str = Conversions.ToString((double)(Conversions.ToDouble(str) * 3.0));

            int start = this.Length - 1;

        Label_00F4:

            num3 = 1;

            if (start >= num3)
            {

                this.Odd = (int)Math.Round(Conversion.Val(Strings.Mid(expression, start, 1)));

                str3 = Conversions.ToString((double)(Conversions.ToDouble(str3) + this.Odd));

                start += -2;

                goto Label_00F4;

            }

            str4 = Conversions.ToString(Conversions.ToDouble(str) + Conversions.ToDouble(str3));

            this.CheckDigit = Conversions.ToString((double)(Conversions.ToDouble(str4) - (10.0 * Conversion.Int((double)(Conversions.ToDouble(str4) / 10.0)))));

            this.CheckDigit = Conversions.ToString((double)(10.0 - Conversions.ToDouble(this.CheckDigit)));

            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {

                this.CheckDigit = Conversions.ToString(0);

            }


        }



        public void CalculateWeightedMod10CheckDigit(string Output)
        {

            int num4;

            this.Even = 0;

            this.Odd = 0;

            string str = Conversions.ToString(0);

            string str2 = Conversions.ToString(0);

            string str3 = Conversions.ToString(0);

            this.CheckDigit = "1";

            this.Length = 0;

            this.Length = Strings.Len(Output);

            int length = this.Length;

        Label_0080:

            num4 = 1;

            if (length >= num4)
            {

                this.Odd = (int)Math.Round(Conversion.Val(Strings.Mid(Output, length, 1)));

                str2 = Conversions.ToString((double)(Conversions.ToDouble(str2) + (this.Odd * 1)));

                length += -3;

                goto Label_0080;

            }

            int start = this.Length - 1;

        Label_00CC:

            num4 = 1;

            if (start >= num4)
            {

                this.Odd = (int)Math.Round(Conversion.Val(Strings.Mid(Output, start, 1)));

                str2 = Conversions.ToString((double)(Conversions.ToDouble(str2) + (this.Odd * 3)));

                start += -3;

                goto Label_00CC;

            }

            int num3 = this.Length - 2;

        Label_0119:

            num4 = 1;

            if (num3 >= num4)
            {

                this.Even = (int)Math.Round(Conversion.Val(Strings.Mid(Output, num3, 1)));

                str = Conversions.ToString((double)(Conversions.ToDouble(str) + (this.Even * 7)));

                num3 += -3;

                goto Label_0119;

            }

            str3 = Conversions.ToString(Conversions.ToDouble(str) + Conversions.ToDouble(str2));

            this.CheckDigit = Conversions.ToString((double)(Conversions.ToDouble(str3) % 10.0));

            if (Conversions.ToDouble(this.CheckDigit) == 10.0)
            {

                this.CheckDigit = Conversions.ToString(0);

            }


        }

    }
}
