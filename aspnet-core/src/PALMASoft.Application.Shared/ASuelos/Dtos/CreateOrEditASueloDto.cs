
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.ASuelos.Dtos
{
    public class CreateOrEditASueloDto : EntityDto<long?>
    {

		public string COD_SUELOS { get; set; }
		
		
		public string ID_SUELOS { get; set; }
		
		
		public string MATERIAL_SUELOS { get; set; }
		
		
		public int PROFUNDIDAD_MUESTRA { get; set; }
		
		
		public string TEXTURA_SUELOS { get; set; }
		
		
		public decimal ARENA { get; set; }
		
		
		public decimal LIMO { get; set; }
		
		
		public decimal ARCILLA { get; set; }
		
		
		public decimal PH { get; set; }
		
		
		public decimal CARBONO_ORGANICO { get; set; }
		
		
		public decimal MATERIA_ORGANICA { get; set; }
		
		
		public decimal FOSFORO { get; set; }
		
		
		public decimal AZUFRE { get; set; }
		
		
		public decimal? ACIDEZ { get; set; }
		
		
		public decimal? ALUMINIO { get; set; }
		
		
		public decimal CALCIO { get; set; }
		
		
		public decimal MAGNESIO { get; set; }
		
		
		public decimal POTASIO { get; set; }
		
		
		public decimal SODIO { get; set; }
		
		
		public decimal CATIONICO { get; set; }
		
		
		public decimal ELECTRICA { get; set; }
		
		
		public decimal BORO { get; set; }
		
		
		public decimal HIERRO { get; set; }
		
		
		public decimal COBRE { get; set; }
		
		
		public decimal MANGANESO { get; set; }
		
		
		public decimal ZINC { get; set; }
		
		
		public decimal CICE { get; set; }
		
		
		public decimal SUMA_BASES { get; set; }
		
		
		public decimal SAT_BASES { get; set; }
		
		
		public decimal SAT_K { get; set; }
		
		
		public decimal SAT_CA { get; set; }
		
		
		public decimal SAT_MG { get; set; }
		
		
		public decimal SAT_NA { get; set; }
		
		
		public decimal SAT_AL { get; set; }
		
		
		public decimal CA_MG { get; set; }
		
		
		public decimal K_MG { get; set; }
		
		
		public decimal CA_MG_DIV_K { get; set; }
		
		
		public string ANALISIS_ID { get; set; }
		
		

    }
}