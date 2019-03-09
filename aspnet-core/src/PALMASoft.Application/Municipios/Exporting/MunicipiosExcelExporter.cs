using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Municipios.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Municipios.Exporting
{
    public class MunicipiosExcelExporter : EpPlusExcelExporterBase, IMunicipiosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MunicipiosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMunicipioForViewDto> municipios)
        {
            return CreateExcelPackage(
                "Municipios.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Municipios"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_MUNICIPIO"),
                        L("NOMBRE_MUNICIPIO"),
                        (L("Departamento")) + L("NOMBRE_DEPARTAMENTO")
                        );

                    AddObjects(
                        sheet, 2, municipios,
                        _ => _.Municipio.ID_MUNICIPIO,
                        _ => _.Municipio.NOMBRE_MUNICIPIO,
                        _ => _.DepartamentoNOMBRE_DEPARTAMENTO
                        );

					

                });
        }
    }
}
