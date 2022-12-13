using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace ImportaDadosSGE.Itens
{
    internal class Fornecedor : Support
    {
        private Fornecedor() { }
        private static Fornecedor? _instance;
        public static Fornecedor Instance => _instance ??= new Fornecedor();
        internal void GetFornecedor(string strConnSqlServer, string strConnFirebird)
        {
            var fornecedores = new List<SFornecedores>();
            using (var conn = new SqlConnection(strConnSqlServer))
            {
                using (var cmd = new SqlCommand("SELECT * FROM INTFOR", conn))
                {
                    cmd.Connection.Open();
                    using (var dados = cmd.ExecuteReader())
                    {
                        if (dados.HasRows)
                        {
                            var count = 1;
                            while (dados.Read())
                            {
                                var forn = new SFornecedores
                                {
                                    CodFor = count.ToString().PadLeft(5, '0'),
                                    CicFor = dados["FOR_CICFOR"].ToString(),
                                    NomFor = RemoveAcento(dados["FOR_NOMFOR"].ToString()),
                                    CodTcl = dados["FOR_CODTCL"].ToString(),
                                    CepFor = dados["FOR_CEPFOR"].ToString(),
                                    FonFor = dados["FOR_FONFOR"].ToString(),
                                    FaxFor = dados["FOR_FAXFOR"].ToString(),
                                    EstFor = dados["FOR_ESTFOR"].ToString(),
                                    CidFor = RemoveAcento(dados["FOR_CIDFOR"].ToString()),
                                    BaiFor = RemoveAcento(dados["FOR_BAIFOR"].ToString()),
                                    EndFor = RemoveAcento(dados["FOR_ENDFOR"].ToString()),
                                    EndNum = RemoveAcento(dados["FOR_ENDNUM"].ToString()),
                                    NomFan = RemoveAcento(dados["FOR_NOMFAN"].ToString()),
                                    CodMun = dados["FOR_CODMUN"].ToString()
                                };
                                count++;
                                fornecedores.Add(forn);
                            }
                        }
                    }
                }
            }
            SendDados(strConnFirebird, fornecedores);
        }
        private static void SendDados(string strConnFirebird, List<SFornecedores> fornecedores)
        {
            using (var conn = new FbConnection(strConnFirebird))
            {
                using (var cmd = new FbCommand("", conn))
                {
                    cmd.Connection.Open();
                    foreach (var forn in fornecedores)
                    {
                        cmd.CommandText = string.Empty;
                        var pessoa = GetTipoPessoa(forn.CodTcl, forn.CicFor);
                        cmd.CommandText = InsertFornecedores(forn, pessoa);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "delete from tb_empforn";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = string.Empty;
                    cmd.CommandText = "insert into tb_empforn (emf_loja,emf_fornecedor,emf_ativo)" +
                                      "select emp_codi,for_codi,'S' from tb_emp cross join tb_forn";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static string InsertFornecedores(SFornecedores forn, string?[] pessoa)
        {
            var logr = forn.EndFor + "," + forn.EndNum;
            return "Update or Insert into tb_forn" +
                   "(FOR_CODI,FOR_RAZA,FOR_NFAN,FOR_PESS,FOR_CGC,FOR_CPF,FOR_CGF,FOR_CEP,FOR_FONE,FOR_FAX," +
                   "FOR_RETE,FOR_DCAD,FOR_DALT,FOR_UF,FOR_LOCAL,FOR_BAIR,FOR_LOGR,FOR_CEND,FOR_CONT,FOR_EMAIL,FOR_PAIS,FOR_CODMUNICIPIO)" +
                   "VALUES" +
                   $"('{forn.CodFor}','{forn.NomFor}','{forn.NomFan}','{GetTipoPessoa(forn.CodTcl)}','{pessoa[0]}','{pessoa[1]}','','{forn.CepFor}','{forn.FonFor}','{forn.FaxFor}'," +
                   $"'N',current_date,current_date,'{forn.EstFor}','{forn.CidFor}','{forn.BaiFor}','{logr}','.','','',1058,'{forn.CodMun}')" +
                   "MATCHING (FOR_CODI)";
        }
        internal struct SFornecedores
        {
            public string? CodFor { get; set; }
            public string? CicFor { get; set; }
            public string? NomFor { get; set; }
            public string? CidFor { get; set; }
            public string? EndFor { get; set; }
            public string? EndNum { get; set; }
            public string? BaiFor { get; set; }
            public string? EstFor { get; set; }
            public string? CepFor { get; set; }
            public string? FonFor { get; set; }
            public string? FaxFor { get; set; }
            public string? CodTcl { get; set; }//Pessoa fisica ou juridica
            public string? NomFan { get; set; }
            public string? CodMun { get; set; }
        }
    }
}