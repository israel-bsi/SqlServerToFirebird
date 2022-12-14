using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ImportaDadosSGE.Itens
{
    internal class Support
    {
        internal static string RemoveAcento(string? texto)
        {
            var encodeEightBit = Encoding.GetEncoding(1251).GetBytes(texto ?? string.Empty);
            var stringSevenBits = Encoding.ASCII.GetString(encodeEightBit);
            var regex = new Regex("[^a-zA-Z0-9]=-_/");
            return regex.Replace(stringSevenBits, " ").ToUpper();
        }
        internal static string GetDataAammdd(string? texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return DateTime.ParseExact(texto, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        }
        internal static string?[] GetTipoPessoa(string? tipoPessoaInt, string? cicFor)
        {
            var pessoa = new string?[3];
            switch (tipoPessoaInt)
            {
                case "001":
                    pessoa[0] = "J"; //For_Pess
                    pessoa[1] = cicFor; //For_Cgc
                    pessoa[2] = ""; //For_Cpf
                    break;
                case "002":
                    pessoa[0] = "F"; //For_Pess
                    pessoa[1] = ""; //For_Cgc
                    pessoa[2] = cicFor?[3..]; //For_Cpf
                    break;
            }
            return pessoa;
        }
        internal static string RetornaTrintaChar(string texto)
        {
            return texto.Length > 30 ? texto[..30] : texto;
        }
        internal static string? FormataParaCpfSeFisico(string? tipo, string? ciccli)
        {
            return tipo == "F" ? ciccli?[3..] : ciccli;
        }
    }
    internal static class StrConexao
    {
        public static string? StrConnSqlServer { get; set; }
        public static string? StrConnFirebird { get; set; }
    }
}