using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace MysqlToFirebird.Itens
{
    internal class Clientes
    {
        private Clientes() { }
        private static Clientes? _instance;
        public static Clientes Instance => _instance ??= new Clientes();
        internal void GetClientes(string strConnMySql, string strConnFirebird)
        {
            using (var conn = new MySqlConnection(strConnMySql))
            {
                using (var cmd = new MySqlCommand())
                {
                    //get dados
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