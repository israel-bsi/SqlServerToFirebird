using ImportaDadosSGE.Conexao;
using ImportaDadosSGE.Itens;

namespace ImportaDadosSGE
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            var dadosSqlServer = new SSqlServer
            {
                ServerName = txtSqlServerDatasource.Text,
                Database = txtSqlServerDatabase.Text
            };
            //var dadosSqlServer = new SSqlServer
            //{
            //    ServerName = @"localhost\SQLEXPRESS",
            //    Database = "D33Q8TG575OEBYV7U5VS"
            //};
            StrConexao.StrConnSqlServer = ConexaoSqlServer.Instance.GetStringConnection(dadosSqlServer);

            var dadosFirebird = new SFirebird
            {
                Database = txtFbDataBase.Text,
                DataSource = txtFbDataSource.Text
            };
            //var dadosFirebird = new SFirebird
            //{
            //    //Database = @"C:\Sistemas\Sge\Dados\DB_SGE.FDB",
            //    Database = @"D:\Bancos\HDias.FDB",
            //    DataSource = "localhost"
            //};
            StrConexao.StrConnFirebird = ConexaoFirebird.Instance.GetStringConnection(dadosFirebird);

            progressbar.Maximum = 3;
            Fornecedores.Instance.StartFornecedores();
            progressbar.Value = 1;
            Clientes.Instance.StartClientes();
            progressbar.Value = 2;
            Produtos.Instance.StartProdutos();
            progressbar.Value = 3;
            Thread.Sleep(2000);
            MessageBox.Show("Importação Concluída", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}