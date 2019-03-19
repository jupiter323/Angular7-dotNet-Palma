import { Component, OnInit, forwardRef, Injector, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR, FormControl, ControlValueAccessor } from '@angular/forms';
import { AnalisesServiceProxy, GetAnalisisForViewDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'analise-combo',
  templateUrl: './analise-combo.component.html',
  styleUrls: ['./analise-combo.component.css'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => AnaliseComboComponent),
    multi: true,
  }]
})
export class AnaliseComboComponent extends AppComponentBase implements ControlValueAccessor {


  @Input('tipo')

  set tipo(val: any) {

    if (val)
      this._analiseService.getAll(undefined, undefined, val, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined).subscribe(result => {
        this.mis_analises = result.items;
      });
  }

  selectedAnalisis = new FormControl('');
  mis_analises: GetAnalisisForViewDto[] = [];
  onTouched: any = () => { };

  constructor(
    private _analiseService: AnalisesServiceProxy,
    injector: Injector) {
    super(injector);
  }

  writeValue(obj: any): void {
    if (this.selectedAnalisis) {
      this.selectedAnalisis.setValue(obj);
    }
  }

  registerOnChange(fn: any): void {
    this.selectedAnalisis.valueChanges.subscribe(fn);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.selectedAnalisis.disable();
    } else {
      this.selectedAnalisis.enable();
    }
  }
}
