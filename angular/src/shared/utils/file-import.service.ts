import { Injectable } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { FileDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
@Injectable()
export class FileImportService {



    parseJsonFromArray(data: any) {

        let jsonData = { fields: data[0], values: [] }
        data.forEach((x, i) => {
            if (i != 0) jsonData.values.push(x);
        });
        return jsonData;
    }
    excelDateValueToMoment(excelData: any) {
        return moment(this.parseDateExcel(excelData));

    }
    excelDateValueToDate(excelData: any) {
        return new Date((excelData - (25567 + 2)) * 86400 * 1000)
    }

    parseDateExcel(excelTimestamp: any) {
        const secondsInDay = 24 * 60 * 60;
        const excelEpoch = new Date(1899, 11, 31);
        const excelEpochAsUnixTimestamp = excelEpoch.getTime();
        const missingLeapYearDay = secondsInDay * 1000;
        const delta = excelEpochAsUnixTimestamp - missingLeapYearDay;
        const excelTimestampAsUnixTimestamp = excelTimestamp * secondsInDay * 1000;
        const parsed = excelTimestampAsUnixTimestamp + delta;
        return isNaN(parsed) ? null : parsed;
    }
}
