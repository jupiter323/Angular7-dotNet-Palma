import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAnalisisForViewDto, AnalisisDto, AnalisisTipo, ASuelosServiceProxy, AFoliaresServiceProxy, GetASueloForViewDto, GetFincaForViewDto, GetAFoliarForViewDto, CreateOrEditAFoliarDto, CreateOrEditASueloDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as XLSX from 'xlsx';
import { finalize } from 'rxjs/operators';
import { FileImportService } from '@shared/utils/file-import.service';
import { Router } from '@angular/router';
@Component({
    selector: 'viewAnalisisDetailModal',
    templateUrl: './view-analisis-detail-modal.component.html'
})
export class ViewAnalisisDetailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAnalisisForViewDto;
    analisisTipo = AnalisisTipo;

    sRecords: Array<GetASueloForViewDto> = [];
    fRecords: Array<GetAFoliarForViewDto> = []

    analisisId: any
    tipoId: number
    constructor(
        injector: Injector,
        private _aSuelosServiceProxy: ASuelosServiceProxy,
        private _aFoliaresServiceProxy: AFoliaresServiceProxy,
        private _fileImportService: FileImportService,
        private router: Router
    ) {
        super(injector);
        this.item = new GetAnalisisForViewDto();
        this.item.analisis = new AnalisisDto();
    }

    show(item: GetAnalisisForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();

        console.log(item.analisis.id);
        this.analisisId = item.analisis.id;
        this.tipoId = item.analisis.tipO_INFORME;
        this.getDetails();


    }
    getDetails() {
        if (this.tipoId == this.analisisTipo.Foliar)
            this._aFoliaresServiceProxy.getAll(
                undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, this.analisisId, undefined, undefined, undefined
            ).subscribe(result => {
                console.log(result.items)
                this.fRecords = result.items;
                this.sRecords = undefined
            });
        else if (this.tipoId == this.analisisTipo.Suelo)
            this._aSuelosServiceProxy.getAll(
                undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, this.analisisId, undefined, undefined, undefined
            ).subscribe(result => {
                console.log(result.items)
                this.sRecords = result.items;
                this.fRecords = undefined;
            });

    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }

    //
    goToForm() {
        const url = this.tipoId == this.analisisTipo.Foliar ? "/app/main/aFoliares/aFoliares/" : "/app/main/aSuelos/aSuelos/";
        const id = this.analisisId;
        this.router.navigate([url, id]).then((e) => {
            if (e) {
                console.log("Navigation is successful!");
            } else {
                console.log("Navigation has failed!");
            }
        });
    }
    //

    @ViewChild('file') file;
    initializing
    isInitAndInsert = false;
    getting
    onFileChange(evt: any) {
        /* wire up file reader */
        const target: DataTransfer = <DataTransfer>(evt.target);
        if (target.files.length !== 1) return new Error('Cannot use multiple files');
        if (this.isInitAndInsert) this.initEntityRecords();

        const reader: FileReader = new FileReader();
        reader.onload = (e: any) => {
            /* read workbook */
            const bstr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

            /* grab first sheet */
            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            /* save data */
            let data = (XLSX.utils.sheet_to_json(ws, { header: 1 }));
            let sendJsonData = this._fileImportService.parseJsonFromArray(data);
            console.log(sendJsonData.values);
            // this._paisesServiceProxy.importcsv(sendJsonData).subscribe(result => {
            //     console.log(result);
            // });


            let resultMap = this.tipoId == this.analisisTipo.Foliar ?
                this.fRecords.map((x) => {
                    return [null, x.aFoliar.iD_FOLIAR]
                }) :
                this.sRecords.map((x) => {
                    return [null, x.aSuelo.iD_SUELOS];
                });

            let sendValues = this._fileImportService.duplicatedMRObjectArray([...resultMap, ...sendJsonData.values], 1);
            console.log("total values :", [...resultMap, ...sendJsonData.values])
            console.log("checked duplicated values :", sendValues)
            sendValues = sendValues.slice(resultMap.length, sendValues.length);
            console.log("send values", sendValues)//checked duplicated values by id_name

            sendValues.forEach((x, i) => {//insert new list
                if (this.tipoId == this.analisisTipo.Foliar) {//foliar
                    var foliar: CreateOrEditAFoliarDto = new CreateOrEditAFoliarDto();
                    foliar.coD_FOLIAR = x[0];
                    foliar.iD_FOLIAR = x[1];
                    foliar.materiaL_FOLIAR = x[2];
                    foliar.nuM_HOJA_FOLIAR = x[3];
                    foliar.nitrogeno = x[4];
                    foliar.fosforo = x[5];
                    foliar.potasio = x[6];
                    foliar.calcio = x[7];
                    foliar.magnesio = x[8];
                    foliar.cloro = x[9];
                    foliar.azufre = x[10];
                    foliar.boro = x[11];
                    foliar.hierro = x[12];
                    foliar.cobre = x[13];
                    foliar.manganeso = x[14];
                    foliar.zinc = x[15];
                    foliar.ca_Mg_K = x[16];
                    foliar.ca_Mg_div_K = x[17];
                    foliar.n_div_K = x[18];
                    foliar.n_div_P = x[19];
                    foliar.k_div_P = x[20];
                    foliar.ca_div_B = x[21];
                    foliar.analisiS_ID = this.analisisId;
                    console.log(foliar)
                    this.insert(foliar);
                } else {//suelos
                    var suelo: CreateOrEditASueloDto = new CreateOrEditASueloDto();
                    suelo.coD_SUELOS = x[0];
                    suelo.iD_SUELOS = x[1];
                    suelo.materiaL_SUELOS = x[2];
                    suelo.profundidaD_MUESTRA = x[3];
                    suelo.texturA_SUELOS = x[4];
                    suelo.arena = x[5];
                    suelo.limo = x[6];
                    suelo.arcilla = x[7];
                    suelo.ph = x[8];
                    suelo.carbonO_ORGANICO = x[9];
                    suelo.materiA_ORGANICA = x[10];
                    suelo.fosforo = x[11];
                    suelo.azufre = x[12];
                    suelo.acidez = x[13];
                    suelo.aluminio = x[14];
                    suelo.calcio = x[15];
                    suelo.magnesio = x[16];
                    suelo.potasio = x[17];
                    suelo.sodio = x[18];
                    suelo.cationico = x[19];
                    suelo.electrica = x[20];
                    suelo.boro = x[21];
                    suelo.hierro = x[22];
                    suelo.cobre = x[23];
                    suelo.manganeso = x[24];
                    suelo.zinc = x[25];
                    suelo.cice = x[26];
                    suelo.sumA_BASES = x[27];
                    suelo.saT_BASES = x[28];
                    suelo.saT_K = x[29];
                    suelo.saT_CA = x[30];
                    suelo.saT_MG = x[31];
                    suelo.saT_NA = x[32];
                    suelo.saT_AL = x[33];
                    suelo.cA_MG = x[34];
                    suelo.k_MG = x[35];
                    suelo.cA_MG_DIV_K = x[36];
                    suelo.analisiS_ID = this.analisisId;
                    console.log(suelo);
                    this.insert(suelo);
                }

            });





        };
        reader.readAsBinaryString(target.files[0]);
    }
    initEntityRecords(): void {
        this.initializing = true;
        if (this.tipoId == this.analisisTipo.Foliar) {
            this.fRecords.forEach((x) => {
                this._aFoliaresServiceProxy.delete(x.aFoliar.id)
                    .subscribe(() => {
                        // this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            })
        } else if (this.tipoId == this.analisisTipo.Suelo) {
            this.sRecords.forEach((x) => {
                this._aSuelosServiceProxy.delete(x.aSuelo.id)
                    .subscribe(() => {
                        // this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            })
        }
    }

    insert(foliarOrSuelo: any): void {
        this.saving = true;
        if (this.tipoId == this.analisisTipo.Foliar) {
            let foliar: CreateOrEditAFoliarDto = foliarOrSuelo
            this._aFoliaresServiceProxy.createOrEdit(foliar)
                .pipe(finalize(() => { this.saving = false; this.getDetails(); }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                });
        } else if (this.tipoId == this.analisisTipo.Suelo) {
            let suelo: CreateOrEditASueloDto = foliarOrSuelo
            this._aSuelosServiceProxy.createOrEdit(suelo)
                .pipe(finalize(() => { this.saving = false; this.getDetails(); }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                });
        }
    }

    addFiles() {
        this.file.nativeElement.click();
    }
}
