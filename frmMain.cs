using MysqlToFirebird.Conexao;
using MysqlToFirebird.Itens;

namespace MysqlToFirebird
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            var dadosMysql = new SMysql
            {
                DataSource = txtMsqlDatasource.Text,
                UserName = txtMsqlUsername.Text,
                Database = txtMsqlDatabase.Text
            };
            var strConnMysql = ConexaoMySql.Instance.GetStringConnection(dadosMysql);

            var dadosFirebird = new SFirebird
            {
                Database = txtFbDataBase.Text
            };
            var strConnFirebird = ConexaoFirebird.Instance.GetStringConnection(dadosFirebird);

            Clientes.Instance.GetClientes(strConnMysql, strConnFirebird);
            Produtos.Instance.GetProdutos(strConnMysql, strConnFirebird);
            Fornecedor.Instance.GetFornecedor(strConnMysql, strConnFirebird);
        }
    }
}