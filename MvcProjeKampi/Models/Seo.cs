using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjeKampi.Models
{
    public class Seo
    {
        public static string UrlCevir(string kelime)
        {
            if (kelime == "" || kelime == null) { return string.Empty; }

            kelime = kelime.Replace(".", "");
            kelime = kelime.Replace(":", "");
            kelime = kelime.Replace(";", "");
            kelime = kelime.Replace(",", "");
            kelime = kelime.Replace(" ", "-");
            kelime = kelime.Replace("!", "");
            kelime = kelime.Replace("(", "");
            kelime = kelime.Replace(")", "");
            kelime = kelime.Replace("'", "'");
            kelime = kelime.Replace("ğ", "g");
            kelime = kelime.Replace("Ğ", "G");
            kelime = kelime.Replace("ı", "i");
            kelime = kelime.Replace("I", "i");
            kelime = kelime.Replace("ç", "c");
            kelime = kelime.Replace("Ç", "C");
            kelime = kelime.Replace("ö", "o");
            kelime = kelime.Replace("Ö", "O");
            kelime = kelime.Replace("ü", "u");
            kelime = kelime.Replace("Ü", "U");
            kelime = kelime.Replace("ş", "s");
            kelime = kelime.Replace("Ş", "S");
            kelime = kelime.Replace("`", "");
            kelime = kelime.Replace("=", "");
            kelime = kelime.Replace("&", "");
            kelime = kelime.Replace("%", "");
            kelime = kelime.Replace("#", "");
            kelime = kelime.Replace("<", "");
            kelime = kelime.Replace(">", "");
            kelime = kelime.Replace("*", "");
            kelime = kelime.Replace("?", "");
            kelime = kelime.Replace("+", "-");
            kelime = kelime.Replace("\"", "");
            kelime = kelime.Replace("»", "-");
            kelime = kelime.Replace("|", "-");
            kelime = kelime.Replace("^", "");
            var sonHarf = kelime[kelime.Length - 1]; //son harfi gösterir
            if (sonHarf == '-')
            {
                var sonHarfSilme = kelime.Substring(0, kelime.Length - 1); //son harfi çıkarır
                kelime = sonHarfSilme;
            }
            return kelime.ToLower();
        }
    }
}