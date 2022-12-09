using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SqlServerToFirebird.Itens
{
    internal class Support
    {
        internal static string RemoveAcento(string? texto)
        {
            var encodeEightBit = Encoding.GetEncoding(1251).GetBytes(texto);
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
        internal static string GetTipoPessoa(string? codTcl)
        {
            return codTcl switch
            {
                "001" => "F",
                "002" => "J",
                _ => "F"
            };
        }
        internal static string?[] GetTipoPessoa(string? tipoPessoaInt, string? cicFor)
        {
            var pessoa = new string?[2];
            switch (tipoPessoaInt)
            {
                case "001":
                    pessoa[0] = ""; //cpf
                    pessoa[1] = cicFor; //cnpj
                    break;
                case "002":
                    pessoa[0] = cicFor; //cpf
                    pessoa[1] = ""; //cnpj
                    break;
            }
            return pessoa;
        }
    }
}