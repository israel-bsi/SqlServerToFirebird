using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Produtos : Support
    {
        private Produtos() { }
        private static string? _strConnFirebird;
        private static string? _strConnSqlServer;
        private static readonly List<SProdutos> ListaProdutos = new();
        private static readonly List<SProdutos> ListaProdutosPrecos = new();
        private static readonly List<SCategorias> Categorias = new();
        private static Produtos? _instance;
        public static Produtos Instance => _instance ??= new Produtos();
        internal void GetProdutos(string strConnSqlServer, string strConnFirebird)
        {
            _strConnFirebird = strConnFirebird;
            _strConnSqlServer = strConnSqlServer;
            //Categoria
            using (var conn = new SqlConnection(strConnSqlServer))
            {
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = "select grp_codgrp, grp_desgrp from intgrp";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var cat = new SCategorias
                                {
                                    CodGrp = dados["GRP_CODGRP"].ToString()?[3..],
                                    DesGrp = dados["GRP_DESGRP"].ToString()
                                };
                                Categorias?.Add(cat);
                            }
                        }
                    }
                    cmd.CommandText = string.Empty;
                }
                GravaCategorias();
            }
            //Produtos
            using (var conn = new SqlConnection(strConnSqlServer))
            {
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = "select prd_codprd, prd_desprd, prd_refprd, prd_codund, prd_datatu, prd_prdncm, prd_codbar, prd_movest, prd_codgrp from intprd";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var prod = new SProdutos
                                {
                                    CodPrd = dados["PRD_CODPRD"].ToString()?.PadLeft(5, '0'),
                                    DesPrd = RetornaTrintaChar(dados["PRD_DESPRD"].ToString() ?? string.Empty),
                                    RefPrd = dados["PRD_REFPRD"].ToString(),
                                    CodUnd = dados["PRD_CODUND"].ToString(),
                                    DatAtu = GetDataAammdd(dados["PRD_DATATU"].ToString()),
                                    PrdNcm = dados["PRD_PRDNCM"].ToString(),
                                    CodBar = dados["PRD_CODBAR"].ToString(),
                                    MovEst = dados["PRD_MOVEST"].ToString(),
                                    CodGrp = dados["PRD_CODGRP"].ToString()?[3..],
                                };
                                ListaProdutos?.Add(prod);
                            }
                        }
                    }
                    cmd.CommandText = string.Empty;
                }
                GravaProdutos();
                GravaProdutoUf();
                //GravaVwCodAux();
            }
            //Preços
            using (var conn = new SqlConnection(strConnSqlServer))
            {
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = "select prd_codprd, pvp_preven, pvp_codtpv from intprd " +
                                      "inner join intpvp on prd_codprd = pvp_codprd;";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var prec = new SProdutos
                                {
                                    CodPrd = dados["PRD_CODPRD"].ToString(),
                                    PreVen = dados["PVP_PREVEN"].ToString(),
                                    CodTpv = dados["PVP_CODTPV"].ToString()
                                };
                                ListaProdutosPrecos?.Add(prec);
                            }
                        }
                    }
                }
                GravaPreco();
            }
        }
        private static void GravaCategorias()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var cat in Categorias)
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertCategorias(cat);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "Insert into tb_mat_grup (mgr_cdse,mgr_codi,mgr_desc)" +
                                      "select mse_codi,'000','GERAL' from tb_mat_sec where mse_codi<>'000'" +
                                      "and not exists(select * from tb_mat_grup g1 where g1.mgr_cdse=mse_codi)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "Insert into tb_mat_sbg (msg_cdse,msg_cdgr,msg_codi,msg_desc)" +
                                      "select mgr_cdse,mgr_codi,'000','GERAL' from tb_mat_grup where mgr_cdse<>'000'" +
                                      "and not exists(select * from tb_mat_sbg s1 where s1.msg_cdse=mgr_cdse and s1.msg_cdgr=mgr_codi)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static void GravaProdutos()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = "delete from tb_empmate";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_estprod";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_estoq";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_estanomes";
                    cmd.ExecuteNonQuery();

                    foreach (var prod in ListaProdutos)
                    {
                        var est = GetQtdEst(prod.CodPrd);
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertProdutos(prod);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Empty;
                        cmd.CommandText = "update or insert into tb_empmate (ema_loja, ema_produto, ema_ativo)" +
                                          $"values ('001', '{prod.CodPrd}', 'S')" +
                                          "matching (ema_loja, ema_produto)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Empty;
                        cmd.CommandText = "update or insert into tb_estprod (ep_loja, ep_cdmt, ep_quan, ep_qfisc, ep_pecas)" +
                                          $"values ('001', '{prod.CodPrd}', {est}, {est}, {est})" +
                                          "matching (ep_loja, ep_cdmt)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Empty;
                        cmd.CommandText = "update or insert into tb_estoq (est_data, est_loja, est_cdmt, est_qent, est_qentf, est_pcent)" +
                                          $"values (current_date, '001', '{prod.CodPrd}', '{est}', '{est}', '{est}')" +
                                          "matching (est_data, est_loja, est_cdmt)";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static void GravaProdutoUf()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var prod in ListaProdutos)
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertProdutoUf(prod);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static void GravaPreco()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var prod in ListaProdutosPrecos)
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertPreco(prod);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static void GravaVwCodAux()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var prod in ListaProdutos.Where(prod => !string.IsNullOrEmpty(prod.CodBar)))
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertVwCodAux(prod);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static string GetTabelaPrec(string? codTpv)
        {
            return codTpv switch
            {
                "001" => "09",
                "002" => "11",
                _ => "09"
            };
        }
        private static double GetQtdEst(string? codPrd)
        {
            double qntEst = 0;
            using (var conn = new SqlConnection(_strConnSqlServer))
            {
                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = $"SELECT top(1) Fmp_QtdEst from IntFmp WHERE Fmp_CodPrd = '{codPrd}'";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            dados.Read();
                            qntEst = dados.GetDouble(1);
                        }
                    }
                }
            }
            return qntEst;
        }
        private static string InsertCategorias(SCategorias cat)
        {
            return "Update or Insert into tb_mat_sec" +
                   "(MSE_CODI, MSE_DESC, MSE_ATIVO)" +
                   "Values" +
                   $"('{cat.CodGrp}', '{cat.DesGrp}', 'S')" +
                   "MATCHING (MSE_CODI)";
        }
        private static string InsertProdutos(SProdutos prod)
        {
            var est = GetQtdEst(prod.CodPrd);
            return "Insert into tb_mate" +
                   "(MAT_CODI, MAT_DESC, MAT_DESR, MAT_UNID, MAT_CDFR," +
                   "MAT_CDSE, MAT_CDGR, MAT_CDSG, MAT_PESA, MAT_ATAC, MAT_MEDI, MAT_EMIN, MAT_EMAX, MAT_ESTG," +
                   "MAT_LOCE, MAT_PESB, MAT_PESL, MAT_IMAG, MAT_DCAD, MAT_DALT, MAT_IPI, MAT_PVAR, MAT_PATA, MAT_CDTP," +
                   "MAT_FRAC, MAT_EMBC, MAT_EMBV, MAT_SERI, MAT_FLAG, MAT_TXAD, MAT_TXPV,MAT_MARC,MAT_NCM,MAT_CEST, MAT_REFE)" +
                   "Values" +
                   $"('{prod.CodPrd}', '{prod.DesPrd}', '{prod.DesPrd}', '{prod.CodUnd}', '00000'," +
                   $"'{prod.CodGrp}', '000', '000', 'N', 'N', 'N', 0, 0, {GetQtdEst(prod.CodPrd)}," +
                   $"'', 0, 0, NULL, CAST('{prod.DatAtu}' AS TIMESTAMP), CAST('{prod.DatAtu}' AS TIMESTAMP), 0, 0, 0, '01'," +
                   $"'S', 1, 1, 'N','S', 0, 0,'','{prod.PrdNcm}','','{prod.RefPrd}')";
        }
        private static string InsertProdutoUf(SProdutos prod)
        {
            return "Insert into TB_MAT_UF" +
                   "(MUF_CDMT, MUF_UF, MUF_CODT, MUF_CST, MUF_ICMS,MUF_CFOPNFCE)" +
                   $"VALUES ('{prod.CodPrd}', 'PI', 'T', '000', 18, '5102')";
        }
        private static string InsertPreco(SProdutos prod)
        {
            return "Update or Insert into TB_MAT_PREC" +
                   "(MPR_CDMT, MPR_CODI, MPR_VALO, MPR_PACR, MPR_PDES)" +
                   "VALUES" +
                   $"('{prod.CodPrd}','{GetTabelaPrec(prod.CodTpv)}',{prod.PreVen?.Replace(',','.')}, 100, 100)" +
                   "matching (MPR_CDMT, MPR_CODI)";
        }
        private static string InsertVwCodAux(SProdutos prod)
        {
            var codbar = string.IsNullOrEmpty(prod.CodBar) ? prod.CodPrd?.PadLeft(13, '0') : prod.CodBar;
            return "update or insert into vw_prod_cdaux" +
                   "(vw_codi, vw_cdmt, vw_desc, vw_desr, vw_unid, vw_flag," +
                   "vw_dcad, vw_dalt, vw_pesob, vw_pesol, vw_embc, vw_categoria," +
                   "vw_grupo, vw_subgrupo, vw_embalagem)" +
                   "values" +
                   $"('{codbar}', '{prod.CodPrd}', '{prod.DesPrd}', '{prod.DesPrd}', '{prod.CodUnd}', 'S'," +
                   $"CAST('{prod.DatAtu}' AS TIMESTAMP), CAST('{prod.DatAtu}' AS TIMESTAMP), 0, 0, 1, '{prod.CodGrp}', '000'," +
                   "'000','','N')" +
                   "matching (vw_cdmt)";
        }
        private struct SProdutos
        {
            internal string? CodPrd { get; set; }
            internal string? DesPrd { get; set; }
            internal string? RefPrd { get; set; }
            internal string? CodUnd { get; set; }
            internal string? DatAtu { get; set; }
            internal string? PrdNcm { get; set; }
            internal string? CodBar { get; set; }
            internal string? MovEst { get; set; }
            internal string? CodTpv { get; set; }
            internal string? PreVen { get; set; }
            internal string? CodGrp { get; set; }
        }
        private struct SCategorias
        {
            public string? CodGrp { get; set; }
            public string? DesGrp { get; set; }
        }
    }
}