using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Fincas.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Fincas.Exporting
{
    public class FincasExcelExporter : EpPlusExcelExporterBase, IFincasExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public FincasExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetFincaForViewDto> fincas)
        {
            return CreateExcelPackage(
                "Fincas.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Fincas"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_FINCA"),
                        L("NOMBRE_FINCA"),
                        L("DEPARTAMENTO_FINCA"),
                        L("MUNICIPIO_FINCA"),
                        L("VEREDA_FINCA"),
                        L("CORREGIMIENTO_FINCA"),
                        L("UBICACION_FINCA"),
                        L("LONGITUD_FINCA"),
                        L("LATITUD_FINCA"),
                        L("CONTACTO_FINCA"),
                        L("TELEFONO_FINCA"),
                        L("CORREO_FINCA"),
                        (L("Cliente")) + L("NOMBRE_CLIENTE"),
                        (L("Cliente")) + ("_ID")
                        );

                    AddObjects(
                        sheet, 2, fincas,
                        _ => _.Finca.ID_FINCA,
                        _ => _.Finca.NOMBRE_FINCA,
                        _ => _.Finca.DEPARTAMENTO_FINCA,
                        _ => _.Finca.MUNICIPIO_FINCA,
                        _ => _.Finca.VEREDA_FINCA,
                        _ => _.Finca.CORREGIMIENTO_FINCA,
                        _ => _.Finca.UBICACION_FINCA,
                        _ => _.Finca.LONGITUD_FINCA,
                        _ => _.Finca.LATITUD_FINCA,
                        _ => _.Finca.CONTACTO_FINCA,
                        _ => _.Finca.TELEFONO_FINCA,
                        _ => _.Finca.CORREO_FINCA,
                        _ => _.ClienteNOMBRE_CLIENTE,
                        _ => _.Finca.ClienteId

                        );

					

                });
        }
    }
}
