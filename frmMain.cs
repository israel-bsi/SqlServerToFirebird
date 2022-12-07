using MysqlToFirebird.Conexao;
using SqlServerToFirebird.Conexao;
using SqlServerToFirebird.Itens;

namespace SqlServerToFirebird
{
    public partial class frmMain : Form
    {
        public frmMain()
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
            var strConnSqlServer = ConexaoSqlServer.Instance.GetStringConnection(dadosSqlServer);

            var dadosFirebird = new SFirebird
            {
                Database = txtFbDataBase.Text
            };
            var strConnFirebird = ConexaoFirebird.Instance.GetStringConnection(dadosFirebird);

            Clientes.Instance.GetClientes(strConnSqlServer, strConnFirebird);
            //Produtos.Instance.GetProdutos(strConnSqlServer, strConnFirebird);
            //Fornecedor.Instance.GetFornecedor(strConnSqlServer, strConnFirebird);
        }
    }
}