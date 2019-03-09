using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Clientes.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Clientes.Exporting
{
    public class ClientesExcelExporter : EpPlusExcelExporterBase, IClientesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClientesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClienteForViewDto> clientes)
        {
            return CreateExcelPackage(
                "Clientes.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Clientes"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_CLIENTE"),
                        L("NOMBRE_CLIENTE"),
                        L("APELLIDO_CLIENTE"),
                        L("GENERO_CLIENTE"),
                        L("FECHA_CLIENTE"),
                        L("CELULAR_CLIENTE"),
                        L("DIRECCION_CLIENTE"),
                        L("DEPARTAMENTO_CLIENTE"),
                        L("MUNICIPIO_CLIENTE"),
                        L("EMPRESA_CLIENTE"),
                        L("PROFESION_CLIENTE")
                        );

                    AddObjects(
                        sheet, 2, clientes,
                        _ => _.Cliente.ID_CLIENTE,
                        _ => _.Cliente.NOMBRE_CLIENTE,
                        _ => _.Cliente.APELLIDO_CLIENTE,
                        _ => _.Cliente.GENERO_CLIENTE,
                        _ => _timeZoneConverter.Convert(_.Cliente.FECHA_CLIENTE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Cliente.CELULAR_CLIENTE,
                        _ => _.Cliente.DIRECCION_CLIENTE,
                        _ => _.Cliente.DEPARTAMENTO_CLIENTE,
                        _ => _.Cliente.MUNICIPIO_CLIENTE,
                        _ => _.Cliente.EMPRESA_CLIENTE,
                        _ => _.Cliente.PROFESION_CLIENTE
                        );

					var fechA_CLIENTEColumn = sheet.Column(5);
                    fechA_CLIENTEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					fechA_CLIENTEColumn.AutoFit();
					

                });
        }
    }
}
