using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace SqlServerToFirebird.Itens
{
    internal class Produtos : Support
    {
        private Produtos() { }
        private static string? _strConnFirebird;
        private static readonly List<SProdutos>? ListaProdutos = new();
        private static readonly List<SProdutos>? ListaProdutosPrecos = new();
        private static Produtos? _instance;
        public static Produtos Instance => _instance ??= new Produtos();
        internal void GetProdutos(string strConnSqlServer, string strConnFirebird)
        {
            _strConnFirebird = strConnFirebird;
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
                                    CodPrd = dados["PRD_CODPRD"].ToString(),
                                    DesPrd = dados["PRD_DESPRD"].ToString(),
                                    RefPrd = dados["PRD_REFPRD"].ToString(),
                                    CodUnd = dados["PRD_CODUND"].ToString(),
                                    DatAtu = dados["PRD_DATATU"].ToString(),
                                    PrdNcm = dados["PRD_PRDNCM"].ToString(),
                                    CodBar = dados["PRD_CODBAR"].ToString(),
                                    MovEst = dados["PRD_MOVEST"].ToString(),
                                    CodGrp = dados["PRD_CODGRP"].ToString()
                                };
                                ListaProdutos?.Add(prod);
                            }
                        }
                    }
                    cmd.CommandText = string.Empty;
                }

                GravaProdutos();

                using (var cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = "select prd_codprd, pvp_preven, pvp_codtpv from intprd" +
                                      "inner join intpvp on prd_codprd = pvp_codprd;";
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            while (dados.Read())
                            {
                                var prec = new SProdutos
                                {
                                    CodPrd = dados["PRD_CODGRP"].ToString(),
                                    PreVen = dados["PRD_PREVEN"].ToString(),
                                    CodTpv = dados["PRD_CODTPV"].ToString()
                                };
                                ListaProdutosPrecos?.Add(prec);
                            }
                        }
                    }
                }
            }
            //GravaCategorias();
            //GravaPreco();
        }
        //codtpv 001 = mpr_codi = 09
        //codtpv 002 = mpr_codi = 11
        private static void GravaCategorias()
        {
            
        }
        private static void GravaProdutos()
        {
            using (var conn = new FbConnection(_strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    //send dados
                }
            }
        }
        private static void GravaPreco()
        {

        }
        internal struct SProdutos
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
    }
}