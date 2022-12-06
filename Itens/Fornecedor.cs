using FirebirdSql.Data.FirebirdClient;
using System.Data.SqlClient;

namespace SqlServerToFirebird.Itens
{
    internal class Fornecedor
    {
        private Fornecedor() { }
        private static Fornecedor? _instance;
        public static Fornecedor Instance => _instance ??= new Fornecedor();
        internal void GetFornecedor(string strConnSqlServer, string strConnFirebird)
        {
            using (var conn = new SqlConnection(strConnSqlServer))
            {
                using (var cmd = new SqlCommand())
                {
                    //getdados
                }
            }
            SendDados(strConnFirebird);
        }
        internal void SendDados(string strConnFirebird)
        {
            using (var conn = new FbConnection(strConnFirebird))
            {
                using (var cmd = new FbCommand())
                {
                    //send dados
                }
            }
        }
    }
}