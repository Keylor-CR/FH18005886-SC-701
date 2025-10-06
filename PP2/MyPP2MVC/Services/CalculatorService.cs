using System;
using System.Collections.Generic;
using MyPP2MVC.Models;


namespace MyPP2MVC.Services
{
    public static class CalculatorService
    {
        // Normaliza longitudes agregando ceros a la izquierda
        private static (string, string) Normalize(string a, string b)
        {
            var max = Math.Max(a.Length, b.Length);
            return (a.PadLeft(max, '0'), b.PadLeft(max, '0'));
        }

        // Operaciones bit a bit (sobre strings)
        public static string AndStrings(string a, string b)
        {
            (a, b) = Normalize(a, b);
            string resultado = "";

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' && b[i] == '1')
                    resultado += '1';
                else
                    resultado += '0';
            }

            return resultado;
        } 
        
        public static string OrStrings(string a, string b)
        {
            (a, b) = Normalize(a, b);
            string resultado = "";

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' || b[i] == '1')
                    resultado += '1';
                else
                    resultado += '0';
            }

            return resultado;
        }


        public static string XorStrings(string a, string b)
        {
            (a, b) = Normalize(a, b);
            string resultado = "";

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    resultado += '1';
                else
                    resultado += '0';
            }

            return resultado;
        }
        
        // Conversión entre bases
        private static (string Bin, string Oct, string Dec, string Hex) ToBases(int value)
        {
            var bin = Convert.ToString(value, 2);
            var oct = Convert.ToString(value, 8);
            var dec = value.ToString();
            var hex = Convert.ToString(value, 16).ToUpperInvariant();
            return (bin, oct, dec, hex);
        }  

        // Una fila por canda tipo de operacion
        public static List<ResultRow> BuildResults(string aRaw, string bRaw)
        {
            // a y b integers
            int aVal = Convert.ToInt32(aRaw, 2);
            int bVal = Convert.ToInt32(bRaw, 2);


            var rows = new List<ResultRow>();


            // a y b: 
            {
                var aBases = ToBases(aVal);
                rows.Add(new ResultRow
                {
                    Label = "a",
                    Bin = aBases.Bin.PadLeft(8, '0'),//binario mostrado a 8 bits
                    Oct = aBases.Oct,
                    Dec = aBases.Dec,
                    Hex = aBases.Hex
                });
            }
            {
                var bBases = ToBases(bVal);
                rows.Add(new ResultRow
                {
                    Label = "b",
                    Bin = bBases.Bin.PadLeft(8, '0'),//binario mostrado a 8 bits
                    Oct = bBases.Oct,
                    Dec = bBases.Dec,
                    Hex = bBases.Hex
                });
            }


            // Operaciones binariasaca cons trings
            string andBin = AndStrings(aRaw, bRaw);
            string orBin = OrStrings(aRaw, bRaw);
            string xorBin = XorStrings(aRaw, bRaw);


            rows.Add(MakeRow("a AND b", andBin));
            rows.Add(MakeRow("a OR b", orBin));
            rows.Add(MakeRow("a XOR b", xorBin));


            // Operaciones aritméticas(label mas valores)
            rows.Add(MakeRowFromInt("a + b", aVal + bVal)); 
            rows.Add(MakeRowFromInt("a • b", aVal * bVal));


            return rows;
        }

        private static ResultRow MakeRow(string label, string bin)
        {
            int val = Convert.ToInt32(string.IsNullOrEmpty(bin) ? "0" : bin, 2);
            var bases = ToBases(val);
            return new ResultRow { Label = label, Bin = bases.Bin, Oct = bases.Oct, Dec = bases.Dec, Hex = bases.Hex };
        }


        private static ResultRow MakeRowFromInt(string label, int value)
        {
            var bases = ToBases(value);
            return new ResultRow { Label = label, Bin = bases.Bin, Oct = bases.Oct, Dec = bases.Dec, Hex = bases.Hex };
        }
    }
}