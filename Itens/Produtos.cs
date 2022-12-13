using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
// ReSharper disable UseObjectOrCollectionInitializer

namespace ImportaDadosSGE.Itens
{
    internal class Produtos : Support
    {
        private Produtos() { }
        private static string? _strConnFirebird;
        private static string? _strConnSqlServer;
        private static readonly List<SProdutos> ListaProdutos = new();
        private static readonly List<SPrecos> ListaPrecos = new();
        private static readonly List<SCategorias> Categorias = new();
        private static Produtos? _instance;
        public static Produtos Instance => _instance ??= new Produtos();
        internal void StartProdutos(string strConnSqlServer, string strConnFirebird)
        {
            _strConnFirebird = strConnFirebird;
            _strConnSqlServer = strConnSqlServer;

            //Categoria
            GetCategorias();
            GravaCategorias();

            //Produtos
            GetProdutos();
            GravaProdutos();

            //Preços
            GetPreco();
            GravaPreco();
        }
        private static void GetCategorias()
        {
            using (var conn = new SqlConnection(_strConnSqlServer))
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
                }
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
        private static void GetProdutos()
        {
            using (var conn = new SqlConnection(_strConnSqlServer))
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
                                var prod = new SProdutos();
                                prod.CodPrd = dados["PRD_CODPRD"].ToString()?.PadLeft(5, '0');
                                prod.DesPrd = RetornaTrintaChar(dados["PRD_DESPRD"].ToString() ?? string.Empty);
                                prod.RefPrd = dados["PRD_REFPRD"].ToString();
                                prod.CodUnd = dados["PRD_CODUND"].ToString();
                                prod.DatAtu = GetDataAammdd(dados["PRD_DATATU"].ToString());
                                prod.PrdNcm = dados["PRD_PRDNCM"].ToString();
                                prod.CodBar = dados["PRD_CODBAR"].ToString();
                                prod.MovEst = dados["PRD_MOVEST"].ToString();
                                prod.CodGrp = dados["PRD_CODGRP"].ToString()?[3..];
                                prod.QtdEst = GetQtdEst(prod.CodPrd);
                                ListaProdutos?.Add(prod);
                            }
                        }
                    }
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
                                          $"values ('001', '{prod.CodPrd}', {prod.QtdEst}, {prod.QtdEst}, {prod.QtdEst})" +
                                          "matching (ep_loja, ep_cdmt)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Empty;
                        cmd.CommandText = "update or insert into tb_estoq (est_data, est_loja, est_cdmt, est_qent, est_qentf, est_pcent)" +
                                          $"values (current_date, '001', '{prod.CodPrd}', {prod.QtdEst}, {prod.QtdEst}, {prod.QtdEst})" +
                                          "matching (est_data, est_loja, est_cdmt)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertProdutoUf(prod);
                        cmd.ExecuteNonQuery();

                        if (!string.IsNullOrEmpty(prod.CodBar))
                        {
                            cmd.CommandText = string.Empty;
                            cmd.CommandText = InsertTbCodAux(prod);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private static void GetPreco()
        {
            using (var conn = new SqlConnection(_strConnSqlServer))
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
                                var prec = new SPrecos()
                                {
                                    CodPrd = dados["PRD_CODPRD"].ToString()?.PadLeft(5, '0'),
                                    PreVen = dados["PVP_PREVEN"].ToString(),
                                    CodTpv = dados["PVP_CODTPV"].ToString()
                                };
                                ListaPrecos?.Add(prec);
                            }
                        }
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
                    foreach (var prod in ListaPrecos)
                    {
                        cmd.CommandText = string.Empty;
                        cmd.CommandText = InsertPreco(prod, GetTabelaPrec(prod.CodTpv));
                        cmd.ExecuteNonQuery();

                        PreenchePrecVazio(prod.CodPrd);
                    }
                }
            }
        }
        private static void PreenchePrecVazio(string? cod)
        {
            var listaPrecSge = new List<SPrecos>();
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandText = $"select mpr_codi, mpr_valo, mpr_cdmt from tb_mat_prec where mpr_cdmt = '{cod}'";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var prec = new SPrecos
                                {
                                    CodPrd = dados["mpr_cdmt"].ToString(),
                                    PreVen = dados["mpr_valo"].ToString(),
                                    CodTpv = dados["mpr_codi"].ToString()
                                };
                                listaPrecSge.Add(prec);
                            }
                        }
                    }
                    if (listaPrecSge.Count == 1)
                    {
                        if (listaPrecSge[0].CodTpv == "09")
                        {
                            cmd.CommandText = InsertPreco(listaPrecSge[0], "11");
                            cmd.ExecuteNonQuery();
                        }
                        if (listaPrecSge[0].CodTpv == "11")
                        {
                            cmd.CommandText = InsertPreco(listaPrecSge[0], "09");
                            cmd.ExecuteNonQuery();
                        }
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
                    cmd.CommandText = $"SELECT top(1) Fmp_QtdEst from IntFmp WHERE Fmp_CodPrd = {codPrd}";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            dados.Read();
                            qntEst = double.Parse(dados["FMP_QTDEST"].ToString() ?? string.Empty);
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
            return "Insert into tb_mate" +
                   "(MAT_CODI, MAT_DESC, MAT_DESR, MAT_UNID, MAT_CDFR," +
                   "MAT_CDSE, MAT_CDGR, MAT_CDSG, MAT_PESA, MAT_ATAC, MAT_MEDI, MAT_EMIN, MAT_EMAX, MAT_ESTG," +
                   "MAT_LOCE, MAT_PESB, MAT_PESL, MAT_IMAG, MAT_DCAD, MAT_DALT, MAT_IPI, MAT_PVAR, MAT_PATA, MAT_CDTP," +
                   "MAT_FRAC, MAT_EMBC, MAT_EMBV, MAT_SERI, MAT_FLAG, MAT_TXAD, MAT_TXPV,MAT_MARC,MAT_NCM,MAT_CEST, MAT_REFE)" +
                   "Values" +
                   $"('{prod.CodPrd}', '{prod.DesPrd}', '{prod.DesPrd}', '{prod.CodUnd}', '00000'," +
                   $"'{prod.CodGrp}', '000', '000', 'N', 'N', 'N', 0, 0, {prod.QtdEst}," +
                   $"'', 0, 0, NULL, CAST('{prod.DatAtu}' AS TIMESTAMP), CAST('{prod.DatAtu}' AS TIMESTAMP), 0, 0, 0, '01'," +
                   $"'S', 1, 1, 'N','S', 0, 0,'','{prod.PrdNcm}','','{prod.RefPrd}')";
        }
        private static string InsertProdutoUf(SProdutos prod)
        {
            return "Insert into TB_MAT_UF" +
                   "(MUF_CDMT, MUF_UF, MUF_CODT, MUF_CST, MUF_ICMS,MUF_CFOPNFCE)" +
                   $"VALUES ('{prod.CodPrd}', 'PI', 'T', '000', 18, '5102')";
        }
        private static string InsertPreco(SPrecos prod, string tabelaprec)
        {
            return "Update or Insert into TB_MAT_PREC" +
                   "(MPR_CDMT, MPR_CODI, MPR_VALO, MPR_PACR, MPR_PDES)" +
                   "VALUES" +
                   $"('{prod.CodPrd}','{tabelaprec}',{prod.PreVen?.Replace(',','.')}, 100, 100)" +
                   "matching (MPR_CDMT, MPR_CODI)";
        }
        private static string InsertTbCodAux(SProdutos prod)
        {
            var codbar = string.IsNullOrEmpty(prod.CodBar) ? prod.CodPrd?.PadLeft(14, '0') : prod.CodBar;
            return "update or insert into tb_mat_cdaux" +
                   "(maux_codi, maux_cdmt, maux_embalagem)" +
                   "values" +
                   $"('{codbar}', '{prod.CodPrd}', 'N')";
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
            internal string? CodGrp { get; set; }
            internal double? QtdEst { get; set; }
        }
        private struct SPrecos
        {
            internal string? CodPrd { get; init; }
            internal string? PreVen { get; init; }
            internal string? CodTpv { get; init; }
        }
        private struct SCategorias
        {
            public string? CodGrp { get; init; }
            public string? DesGrp { get; init; }
        }
    }
}