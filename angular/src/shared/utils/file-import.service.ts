import { Injectable } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { FileDto } from '@shared/service-proxies/service-proxies';

@Injectable()
export class FileImportService {

 

    parseJsonFromArray(data: any) {
  
        let jsonData = { fields: data[0], values: [] }
        data.forEach((x, i) => {
            if (i != 0) jsonData.values.push(x);
        });
        return jsonData;
    }
  
  
}
