using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.ASuelos.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.ASuelos.Exporting
{
    public class ASuelosExcelExporter : EpPlusExcelExporterBase, IASuelosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ASuelosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetASueloForViewDto> aSuelos)
        {
            return CreateExcelPackage(
                "ASuelos.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ASuelos"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("COD_SUELOS"),
                        L("ID_SUELOS"),
                        L("MATERIAL_SUELOS"),
                        L("PROFUNDIDAD_MUESTRA"),
                        L("TEXTURA_SUELOS"),
                        L("ARENA"),
                        L("LIMO"),
                        L("ARCILLA"),
                        L("PH"),
                        L("CARBONO_ORGANICO"),
                        L("MATERIA_ORGANICA"),
                        L("FOSFORO"),
                        L("AZUFRE"),
                        L("ACIDEZ"),
                        L("ALUMINIO"),
                        L("CALCIO"),
                        L("MAGNESIO"),
                        L("POTASIO"),
                        L("SODIO"),
                        L("CATIONICO"),
                        L("ELECTRICA"),
                        L("BORO"),
                        L("HIERRO"),
                        L("COBRE"),
                        L("MANGANESO"),
                        L("ZINC"),
                        L("CICE"),
                        L("SUMA_BASES"),
                        L("SAT_BASES"),
                        L("SAT_K"),
                        L("SAT_CA"),
                        L("SAT_MG"),
                        L("SAT_NA"),
                        L("SAT_AL"),
                        L("CA_MG"),
                        L("K_MG"),
                        L("CA_MG_DIV_K"),
                        L("ANALISIS_ID")
                        );

                    AddObjects(
                        sheet, 2, aSuelos,
                        _ => _.ASuelo.COD_SUELOS,
                        _ => _.ASuelo.ID_SUELOS,
                        _ => _.ASuelo.MATERIAL_SUELOS,
                        _ => _.ASuelo.PROFUNDIDAD_MUESTRA,
                        _ => _.ASuelo.TEXTURA_SUELOS,
                        _ => _.ASuelo.ARENA,
                        _ => _.ASuelo.LIMO,
                        _ => _.ASuelo.ARCILLA,
                        _ => _.ASuelo.PH,
                        _ => _.ASuelo.CARBONO_ORGANICO,
                        _ => _.ASuelo.MATERIA_ORGANICA,
                        _ => _.ASuelo.FOSFORO,
                        _ => _.ASuelo.AZUFRE,
                        _ => _.ASuelo.ACIDEZ,
                        _ => _.ASuelo.ALUMINIO,
                        _ => _.ASuelo.CALCIO,
                        _ => _.ASuelo.MAGNESIO,
                        _ => _.ASuelo.POTASIO,
                        _ => _.ASuelo.SODIO,
                        _ => _.ASuelo.CATIONICO,
                        _ => _.ASuelo.ELECTRICA,
                        _ => _.ASuelo.BORO,
                        _ => _.ASuelo.HIERRO,
                        _ => _.ASuelo.COBRE,
                        _ => _.ASuelo.MANGANESO,
                        _ => _.ASuelo.ZINC,
                        _ => _.ASuelo.CICE,
                        _ => _.ASuelo.SUMA_BASES,
                        _ => _.ASuelo.SAT_BASES,
                        _ => _.ASuelo.SAT_K,
                        _ => _.ASuelo.SAT_CA,
                        _ => _.ASuelo.SAT_MG,
                        _ => _.ASuelo.SAT_NA,
                        _ => _.ASuelo.SAT_AL,
                        _ => _.ASuelo.CA_MG,
                        _ => _.ASuelo.K_MG,
                        _ => _.ASuelo.CA_MG_DIV_K,
                        _ => _.ASuelo.ANALISIS_ID
                        );

					

                });
        }
    }
}
