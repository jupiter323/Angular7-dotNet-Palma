using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Departamentos.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Departamentos.Exporting
{
    public class DepartamentosExcelExporter : EpPlusExcelExporterBase, IDepartamentosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DepartamentosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDepartamentoForViewDto> departamentos)
        {
            return CreateExcelPackage(
                "Departamentos.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Departamentos"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_DEPARTAMENTO"),
                        L("NOMBRE_DEPARTAMENTO"),
                        (L("Pais")) + L("NOMBRE_PAIS"),
                        (L("Pais")) + ("_ID")
                        );

                    AddObjects(
                        sheet, 2, departamentos,
                        _ => _.Departamento.ID_DEPARTAMENTO,
                        _ => _.Departamento.NOMBRE_DEPARTAMENTO,
                        _ => _.PaisNOMBRE_PAIS,
                        _ => _.Departamento.PaisId
                        );

					

                });
        }
    }
}
