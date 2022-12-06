namespace MysqlToFirebird.Conexao
{
    internal class ConexaoMySql
    {
        private ConexaoMySql() { }
        private static ConexaoMySql? _instance;
        public static ConexaoMySql Instance => _instance ??= new ConexaoMySql();
        internal string GetStringConnection(SMysql dados)
        {
            return $"DATASOURCE={dados.DataSource};" + //ip
                   $"USERNAME={dados.UserName};" +
                   $"PASSWORD={dados.Password};" +
                   $"DATABASE={dados.Database};" +
                   "CONVERT ZERO DATETIME=True";
        }
    }
    public struct SMysql
    {
        public string DataSource { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}