using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Clientes : Support
    {
        private Clientes() { }
        private static Clientes? _instance;
        public static Clientes Instance => _instance ??= new Clientes();
        internal void GetClientes(string strConnMySql, string strConnFirebird)
        {
            var clientes = new List<SClientes>();
            using (var conn = new SqlConnection(strConnMySql))
            {
                using (var cmd = new SqlCommand("SELECT * FROM INTCLI", conn))
                {
                    cmd.Connection.Open();
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var cli = new SClientes
                                {
                                    CodCli = dados["CLI_CODCLI"].ToString()?.PadLeft(5, '0'),
                                    NomCli = RemoveAcento(dados["CLI_NOMCLI"].ToString()),
                                    CicCli = dados["CLI_CICCLI"].ToString(),
                                    CepCli = dados["CLI_CEPCLI"].ToString(),
                                    FonCli = dados["CLI_FONCLI"].ToString(),
                                    FaxCli = dados["CLI_FAXCLI"].ToString(),
                                    DatCad = GetDataAammdd(dados["CLI_DATCAD"].ToString()),
                                    DtuAtu = GetDataAammdd(dados["CLI_DTUATU"].ToString()),
                                    EstCli = dados["CLI_ESTCLI"].ToString(),
                                    CidCli = RemoveAcento(dados["CLI_CIDCLI"].ToString()),
                                    BaiCli = RemoveAcento(dados["CLI_BAICLI"].ToString()),
                                    EndCli = RemoveAcento(dados["CLI_ENDCLI"].ToString()),
                                    EndNum = RemoveAcento(dados["CLI_ENDNUM"].ToString()),
                                    NomFan = RemoveAcento(dados["CLI_NOMFAN"].ToString()),
                                    CodTcl = dados["CLI_CODTCL"].ToString()
                                };
                                clientes.Add(cli);
                            }
                        }
                    }
                }
            }
            SendDados(strConnFirebird, clientes);
        }
        private static void SendDados(string strConnFirebird, List<SClientes> clientes)
        {
            using (var conn = new FbConnection(strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var cli in clientes)
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
            return "UPDATE OR INSERT INTO TB_CLIE" +
                   "(CLI_CODI,CLI_NOME,CLI_CONT,CLI_PESS,CLI_CDZN,CLI_CPF,CLI_CGC,CLI_CGF," +
                   "CLI_CEP,CLI_CEND,CLI_FONE,CLI_FAX,CLI_GRUP,CLI_REND,CLI_LIMC,CLI_SALD,CLI_DNAS,CLI_DCAD,CLI_DALT," +
                   "CLI_ATIV,CLI_RETE,CLI_BANC,CLI_AGEN,CLI_CONTA,CLI_CFID, CLI_CDVD, CLI_PDES, CLI_UF, CLI_LOCAL, CLI_BAIR, CLI_LOGR, CLI_OBSE," +
                   "CLI_TABPREC,CLI_PLAN,CLI_ACRES,CLI_CEPENT,CLI_UFENT,CLI_LOCALENT,CLI_BAIRENT,CLI_LOGRENT,CLI_CENDENT," +
                   "CLI_CEPCOB,CLI_UFCOB,CLI_LOCALCOB,CLI_BAIRCOB,CLI_LOGRCOB,CLI_CENDCOB,CLI_PCOMI,CLI_TPPG,CLI_DIAPG,CLI_FCOM," +
                   "CLI_SITUAC,CLI_NOME2,CLI_CDPG,CLI_DIAMES,CLI_CARENCIA,CLI_PJURO,CLI_PMULTA,CLI_FSEG," +
                   "CLI_FTER,CLI_FQUA,CLI_FQUI,CLI_FSEX,CLI_FSAB,CLI_CDGE,CLI_ESTCIV,CLI_PAIS)" +
                   "VALUES" +
                   $"('{cli.CodCli}','{cli.NomCli}','','{GetTipoPessoa(cli.CodTcl)}','','{cli.CicCli}','',''," +
                   $"'{cli.CepCli}','.','{cli.FonCli}','{cli.FaxCli}','000',0," +
                   $"200,0,CAST('{cli.DatCad}' AS TIMESTAMP),CAST('{cli.DatCad}' AS TIMESTAMP),'{cli.DtuAtu}'," +
                   $"'S','N','','','','','000',0,'{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}',''," +
                   $"'01','00',0,'{cli.CepCli}','{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}','.'," +
                   $"'{cli.CepCli}','{cli.EstCli}','{cli.CidCli}','{cli.BaiCli}','{logr}','.',0,0,0,'N'," +
                   $"1,'{cli.NomFan}','01',0,0,0,0,'S'," +
                   "'S','S','S','S','S','',0,1058)" +
                   "MATCHING (CLI_CODI)";
            //CLI_NOMEDOCAMPO
            //CLI_CODCLI
        }
        internal struct SClientes
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
            internal string? CodTcl { get; set; }
        }
    }
}