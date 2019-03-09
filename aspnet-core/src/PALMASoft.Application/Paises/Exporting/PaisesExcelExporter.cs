using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Paises.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Paises.Exporting
{
    public class PaisesExcelExporter : EpPlusExcelExporterBase, IPaisesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PaisesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPaisForViewDto> paises)
        {
            return CreateExcelPackage(
                "Paises.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Paises"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_PAIS"),
                        L("NOMBRE_PAIS")
                        );

                    AddObjects(
                        sheet, 2, paises,
                        _ => _.Pais.ID_PAIS,
                        _ => _.Pais.NOMBRE_PAIS
                        );

					

                });
        }
    }
}
