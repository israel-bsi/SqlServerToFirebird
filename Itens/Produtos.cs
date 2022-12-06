using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace MysqlToFirebird.Itens
{
    internal class Produtos
    {
        private Produtos() { }
        private static Produtos? _instance;
        public static Produtos Instance => _instance ??= new Produtos();
        internal void GetProdutos(string strConnMySql, string strConnFirebird)
        {
            using (var conn = new MySqlConnection(strConnMySql))
            {
                using (var cmd = new MySqlCommand())
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