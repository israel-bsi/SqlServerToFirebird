namespace ImportaDadosSGE.Conexao
{
    internal class ConexaoSqlServer
    {
        private ConexaoSqlServer() { }
        private static ConexaoSqlServer? _instance;
        public static ConexaoSqlServer Instance => _instance ??= new ConexaoSqlServer();
        internal string GetStringConnection(SSqlServer dados)
        {
            return $"Server={dados.ServerName};" +
                   $"Database={dados.Database};" +
                   "Integrated Security=SSPI;";
        }
    }
    public struct SSqlServer
    {
        public string ServerName { get; set; }
        public string Database { get; set; }
    }
}