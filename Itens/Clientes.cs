using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Clientes
    {
        private Clientes() { }
        private static Clientes? _instance;
        public static Clientes Instance => _instance ??= new Clientes();
        internal void GetClientes(string strConnMySql, string strConnFirebird)
        {
            using (var conn = new SqlConnection(strConnMySql))
            {
                using (var cmd = new SqlCommand())
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