namespace SqlServerToFirebird.Conexao
{
    internal class ConexaoSqlServer
    {
        private ConexaoSqlServer() { }
        private static ConexaoSqlServer? _instance;
        public static ConexaoSqlServer Instance => _instance ??= new ConexaoSqlServer();
        internal string GetStringConnection(SSqlServer dados)
        {
            return $"DATASOURCE={dados.DataSource};" + //ip
                   $"USERNAME={dados.UserName};" +
                   $"PASSWORD={dados.Password};" +
                   $"DATABASE={dados.Database};" +
                   "CONVERT ZERO DATETIME=True";
        }
    }
    public struct SSqlServer
    {
        public string DataSource { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}