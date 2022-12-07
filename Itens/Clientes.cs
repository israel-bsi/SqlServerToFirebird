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
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = "";
                    cmd.Connection.Open();
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            dados.Read();
                        }
                    }
                }
            }
            //SendDados(strConnFirebird);
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
        struct SClientes
        {
            internal string CodCli { get; set; }
            internal string NomCli { get; set; }
            internal string CicCli { get; set; }
            internal string CepCli { get; set; }
            internal string FonCli { get; set; }
            internal string FaxCli { get; set; }
            internal string DatCad { get; set; }
            internal string DtuAtu { get; set; }
            internal string EstCli { get; set; }
            internal string CidCli { get; set; }
            internal string BaiCli { get; set; }
            internal string EndCli { get; set; }
            internal string EndNum { get; set; }
            internal string NomFan { get; set; }
        }
    }
}