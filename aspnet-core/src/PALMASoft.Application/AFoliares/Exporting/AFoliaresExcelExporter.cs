using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.AFoliares.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.AFoliares.Exporting
{
    public class AFoliaresExcelExporter : EpPlusExcelExporterBase, IAFoliaresExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AFoliaresExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAFoliarForViewDto> aFoliares)
        {
            return CreateExcelPackage(
                "AFoliares.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AFoliares"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("COD_FOLIAR"),
                        L("ID_FOLIAR"),
                        L("MATERIAL_FOLIAR"),
                        L("NUM_HOJA_FOLIAR"),
                        L("NITROGENO"),
                        L("FOSFORO"),
                        L("POTASIO"),
                        L("CALCIO"),
                        L("MAGNESIO"),
                        L("CLORO"),
                        L("AZUFRE"),
                        L("BORO"),
                        L("HIERRO"),
                        L("COBRE"),
                        L("MANGANESO"),
                        L("ZINC"),
                        L("Ca_Mg_K"),
                        L("Ca_Mg_div_K"),
                        L("N_div_K"),
                        L("N_div_P"),
                        L("K_div_P"),
                        L("Ca_div_B"),
                        L("ANALISIS_ID")
                        );

                    AddObjects(
                        sheet, 2, aFoliares,
                        _ => _.AFoliar.COD_FOLIAR,
                        _ => _.AFoliar.ID_FOLIAR,
                        _ => _.AFoliar.MATERIAL_FOLIAR,
                        _ => _.AFoliar.NUM_HOJA_FOLIAR,
                        _ => _.AFoliar.NITROGENO,
                        _ => _.AFoliar.FOSFORO,
                        _ => _.AFoliar.POTASIO,
                        _ => _.AFoliar.CALCIO,
                        _ => _.AFoliar.MAGNESIO,
                        _ => _.AFoliar.CLORO,
                        _ => _.AFoliar.AZUFRE,
                        _ => _.AFoliar.BORO,
                        _ => _.AFoliar.HIERRO,
                        _ => _.AFoliar.COBRE,
                        _ => _.AFoliar.MANGANESO,
                        _ => _.AFoliar.ZINC,
                        _ => _.AFoliar.Ca_Mg_K,
                        _ => _.AFoliar.Ca_Mg_div_K,
                        _ => _.AFoliar.N_div_K,
                        _ => _.AFoliar.N_div_P,
                        _ => _.AFoliar.K_div_P,
                        _ => _.AFoliar.Ca_div_B,
                        _ => _.AFoliar.ANALISIS_ID
                        );

					

                });
        }
    }
}
