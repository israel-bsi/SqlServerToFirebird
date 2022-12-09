using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Produtos : Support
    {
        private Produtos() { }
        private static Produtos? _instance;
        public static Produtos Instance => _instance ??= new Produtos();
        internal void GetProdutos(string strConnMySql, string strConnFirebird)
        {
            using (var conn = new SqlConnection(strConnMySql))
            {
                using (var cmd = new SqlCommand())
                {
                    //getdados
                }
            }
            SendDados(strConnFirebird);
        }
        private static void SendDados(string strConnFirebird)
        {
            using (var conn = new FbConnection(strConnFirebird))
            {
                using (var cmd = new FbCommand())
                {
                    //send dados
                }
            }
        }
        internal struct SProdutos
        {
            public string? CodPrd { get; set; }
            public string? DesPrd { get; set; }
            public string? RefPrd { get; set; }
            public string? CodUnd { get; set; }
            public string? SitPrd { get; set; }
            public string? DatAtu { get; set; }
            public string? PrdNcm { get; set; }
            public string? CodBar { get; set; }
            public string? MovEst { get; set; }
        }
    }
}