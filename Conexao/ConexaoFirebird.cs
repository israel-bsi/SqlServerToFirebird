namespace ImportaDadosSGE.Conexao
{
    internal class ConexaoFirebird
    {
        private ConexaoFirebird() { }
        private static ConexaoFirebird? _instance;
        public static ConexaoFirebird Instance => _instance ??= new ConexaoFirebird();
        internal string GetStringConnection(SFirebird dados)
        {
            return "User=SYSDBA;" +
                   "Password=masterkey;" +
                   $"Database={dados.Database};"+
                   $"DataSource={dados.DataSource};"+
                   "MaxPoolSize=250;" +
                   "Port=3050;" +
                   "Charset=WIN1252";
        }
    }
    public struct SFirebird
    {
        public string Database { get; set; }
        public string DataSource { get; set; }
    }
}