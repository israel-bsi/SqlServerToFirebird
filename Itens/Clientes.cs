using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
// ReSharper disable UseObjectOrCollectionInitializer

namespace ImportaDadosSGE.Itens
{
    internal class Clientes : Support
    {
        private Clientes() { }
        private static Clientes? _instance;
        private static readonly List<SClientes> ListaClientes = new();
        public static Clientes Instance => _instance ??= new Clientes();
        internal void StartClientes()
        {
            GetClientes();
            GravaClientes();
        }
        private static void GetClientes()
        {
            using (var conn = new SqlConnection(StrConexao.StrConnSqlServer))
            {
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = "SELECT Cli_CicCli, Cli_NomCli, Cli_NomFan, Cli_EndCli, Cli_EndNum, Cli_BaiCli, Cli_CidCli," +
                                      "Cli_EstCli, Cli_CepCli, Cli_FonCli, Cli_FaxCli, Cli_CelCli, Cli_DatCad, Cli_DtuAtu," +
                                      "Cli_CodCli, Cli_CodMun, Fcl_TipFic, Fcl_InsEst FROM IntCli " +
                                      "JOIN IntFcl on Cli_CicCli  = Fcl_CicCli";
                    cmd.Connection.Open();
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var cli = new SClientes();
                                cli.CodCli = dados["CLI_CODCLI"].ToString()?.PadLeft(5, '0');
                                cli.NomCli = RemoveAcento(dados["CLI_NOMCLI"].ToString());
                                cli.CepCli = dados["CLI_CEPCLI"].ToString();
                                cli.FonCli = dados["CLI_FONCLI"].ToString();
                                cli.FaxCli = dados["CLI_FAXCLI"].ToString();
                                cli.DatCad = GetDataAammdd(dados["CLI_DATCAD"].ToString());
                                cli.DtuAtu = GetDataAammdd(dados["CLI_DTUATU"].ToString());
                                cli.EstCli = dados["CLI_ESTCLI"].ToString();
                                cli.CidCli = RemoveAcento(dados["CLI_CIDCLI"].ToString());
                                cli.BaiCli = RemoveAcento(dados["CLI_BAICLI"].ToString());
                                cli.EndCli = RemoveAcento(dados["CLI_ENDCLI"].ToString());
                                cli.EndNum = RemoveAcento(dados["CLI_ENDNUM"].ToString());
                                cli.NomFan = RemoveAcento(dados["CLI_NOMFAN"].ToString());
                                cli.CliTipo = dados["FCL_TIPFIC"].ToString();
                                cli.CicCli = FormataParaCpfSeFisico(cli.CliTipo, dados["CLI_CICCLI"].ToString());
                                cli.InsEst = dados["FCL_INSEST"].ToString();
                                cli.CodMun = dados["CLI_CODMUN"].ToString();
                                ListaClientes?.Add(cli);
                            }
                        }
                    }
                }
            }
        }
        private static void GravaClientes()
        {
            using (var conn = new FbConnection(StrConexao.StrConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var cli in ListaClientes)
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertClientes(cli);
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_empclie";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "insert into tb_empclie (emc_loja,emc_cliente,emc_ativo)" +
                                      "select emp_codi,cli_codi,'S' from tb_emp cross join tb_clie";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_clfpgto";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "insert into tb_clfpgto (clf_cliente,clf_fpgto,clf_przmax)" +
                                      "select cli_codi,fpg_codi,999 from tb_clie cross join tb_fpgto";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static string InsertClientes(SClientes cli)
        {
            var logr = cli.EndCli + "," + cli.EndNum;
            string? cpf = "", cnpj = "", ie = "";
            switch (cli.CliTipo)
            {
                case "F":
                    cpf = cli.CicCli;
                    cnpj = "";
                    ie = "";
                    break;
                case "J":
                    cpf = "";
                    cnpj = cli.CicCli;
                    ie = cli.InsEst;
                    break;
            }
            return "UPDATE OR INSERT INTO TB_CLIE" +
                   "(CLI_CODI,CLI_NOME,CLI_CONT,CLI_PESS,CLI_CDZN,CLI_CPF,CLI_CGC,CLI_CGF," +
                   "CLI_CEP,CLI_CEND,CLI_FONE,CLI_FAX,CLI_GRUP,CLI_REND,CLI_LIMC,CLI_SALD,CLI_DNAS,CLI_DCAD,CLI_DALT," +
                   "CLI_ATIV,CLI_RETE,CLI_BANC,CLI_AGEN,CLI_CONTA,CLI_CFID, CLI_CDVD, CLI_PDES, CLI_UF, CLI_LOCAL, CLI_BAIR, CLI_LOGR, CLI_OBSE," +
                   "CLI_TABPREC,CLI_PLAN,CLI_ACRES,CLI_CEPENT,CLI_UFENT,CLI_LOCALENT,CLI_BAIRENT,CLI_LOGRENT,CLI_CENDENT," +
                   "CLI_CEPCOB,CLI_UFCOB,CLI_LOCALCOB,CLI_BAIRCOB,CLI_LOGRCOB,CLI_CENDCOB,CLI_PCOMI,CLI_TPPG,CLI_DIAPG,CLI_FCOM," +
                   "CLI_SITUAC,CLI_NOME2,CLI_CDPG,CLI_DIAMES,CLI_CARENCIA,CLI_PJURO,CLI_PMULTA,CLI_FSEG," +
                   "CLI_FTER,CLI_FQUA,CLI_FQUI,CLI_FSEX,CLI_FSAB,CLI_CDGE,CLI_ESTCIV,CLI_PAIS,CLI_CODMUNICIPIO)" +
                   "VALUES" +
                   $"('{cli.CodCli}','{cli.NomCli}','','{cli.CliTipo}','','{cpf}','{cnpj}','{ie}'," +
                   $"'{cli.CepCli}','.','{cli.FonCli}','{cli.FaxCli}','000',0," +
                   $"200,0,CAST('{cli.DatCad}' AS TIMESTAMP),CAST('{cli.DatCad}' AS TIMESTAMP),'{cli.DtuAtu}'," +
                   $"'S','N','','','','','000',0,'{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}',''," +
                   $"'01','00',0,'{cli.CepCli}','{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}','.'," +
                   $"'{cli.CepCli}','{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}','.',0,0,0,'N'," +
                   $"1,'{cli.NomFan}','01',0,0,0,0,'S'," +
                   $"'S','S','S','S','S','',0,1058,'{cli.CodMun}')" +
                   "MATCHING (CLI_CODI)";
        }
        private struct SClientes
        {
            internal string? CodCli { get; set; }
            internal string? NomCli { get; set; }
            internal string? CicCli { get; set; }
            internal string? CepCli { get; set; }
            internal string? FonCli { get; set; }
            internal string? FaxCli { get; set; }
            internal string? DatCad { get; set; }
            internal string? DtuAtu { get; set; }
            internal string? EstCli { get; set; }
            internal string? CidCli { get; set; }
            internal string? BaiCli { get; set; }
            internal string? EndCli { get; set; }
            internal string? EndNum { get; set; }
            internal string? NomFan { get; set; }
            internal string? CliTipo { get; set; }
            internal string? InsEst { get; set; }
            internal string? CodMun { get; set; }
        }
    }
}