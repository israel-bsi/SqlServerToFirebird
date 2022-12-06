using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Produtos
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