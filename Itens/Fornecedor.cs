using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace MysqlToFirebird.Itens
{
    internal class Fornecedor
    {
        private Fornecedor() { }
        private static Fornecedor? _instance;
        public static Fornecedor Instance => _instance ??= new Fornecedor();
        internal void GetFornecedor(string strConnMySql, string strConnFirebird)
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