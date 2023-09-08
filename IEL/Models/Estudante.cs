using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEL.Models
{
    public enum InstituicaoEnsino
    {
        UFBA, UNIFACS, USP, UFMG, EBMSP

    }
    public enum EstadoEn
    {
        Acre,
        Alagoas,
        [Description("Amapá")]
        Amapa,
        Amazonas,
        Bahia,
        [Description("Ceará")]
        Ceara,
        [Description("Distrito Federal")]
        DistritoFederal,
        [Description("Espirito Santo")]
        EspiritoSanto,
        [Description("Goiás")]
        Goias,
        [Description("Maranhão")]
        Maranhao,
        [Description("Mato Grosso")]
        MatoGrosso,
        [Description("Mato Grosso do Sul")]
        MatoGrossoDoSul,
        [Description("Minas Gerais")]
        MinasGerais,
        [Description("Pará")]
        Para,
        [Description("Paraíba")]
        Paraiba,
        [Description("Paraná")]
        Parana,
        Pernambuco,
        [Description("Piauí")]
        Piaui,
        [Description("Rio de Janeiro")]
        RioDeJaneiro,
        [Description("Rio Grande do Norte")]
        RioGrandeDoNorte,
        [Description("Rio Grande do Sul")]
        RioGrandeDoSul,
        [Description("Rondônia")]
        Rondonia,
        [Description("Roraima")]
        Roraima,
        [Description("Santa Catarina")]
        SantaCatarina,
        [Description("São Paulo")]
        SaoPaulo,
        [Description("Sergipe")]
        Sergipe,
        [Description("Tocantins")]
        Tocantins
    }

    public class Estudante
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required, MaxLength(200)]
        public string Endereco { get; set; }

        [ForeignKey("Estado")]
        public int EstadoId { get; set; }

        [Required]
        public EstadoEn Estado { get; set; }

        [Required]
        public string Cidade { get; set; } 

        [Required]
        public InstituicaoEnsino Instituicao { get; set; }

        [Required, MaxLength(100)]
        public string NomeDoCurso { get; set; }

        [Required]
        public DateTime DataDeConclusao { get; set; }
    }


}
