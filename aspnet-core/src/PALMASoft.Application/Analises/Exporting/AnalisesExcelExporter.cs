using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PALMASoft.DataExporting.Excel.EpPlus;
using PALMASoft.Analises.Dtos;
using PALMASoft.Dto;
using PALMASoft.Storage;

namespace PALMASoft.Analises.Exporting
{
    public class AnalisesExcelExporter : EpPlusExcelExporterBase, IAnalisesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AnalisesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAnalisisForViewDto> analises)
        {
            return CreateExcelPackage(
                "Analises.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Analises"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ID_INFORME"),
                        L("TIPO_INFORME"),
                        L("FECHA_MUESTREO"),
                        L("FECHA_REGISTRO"),
                        L("FECHA_ENTREGA"),
                        (L("Finca")) + L("NOMBRE_FINCA")
                        );

                    AddObjects(
                        sheet, 2, analises,
                        _ => _.Analisis.ID_INFORME,
                        _ => _.Analisis.TIPO_INFORME,
                        _ => _timeZoneConverter.Convert(_.Analisis.FECHA_MUESTREO, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Analisis.FECHA_REGISTRO, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Analisis.FECHA_ENTREGA, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.FincaNOMBRE_FINCA
                        );

					var fechA_MUESTREOColumn = sheet.Column(3);
                    fechA_MUESTREOColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					fechA_MUESTREOColumn.AutoFit();
					var fechA_REGISTROColumn = sheet.Column(4);
                    fechA_REGISTROColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					fechA_REGISTROColumn.AutoFit();
					var fechA_ENTREGAColumn = sheet.Column(5);
                    fechA_ENTREGAColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					fechA_ENTREGAColumn.AutoFit();
					

                });
        }
    }
}
