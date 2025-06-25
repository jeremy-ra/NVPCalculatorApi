import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NpvInputComponent } from './npv-input.component';
import { NpvService } from '../../services/npv.service';
import { of } from 'rxjs';

describe('NpvInputComponent', () => {
  let component: NpvInputComponent;
  let fixture: ComponentFixture<NpvInputComponent>;
  let npvServiceSpy: jasmine.SpyObj<NpvService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('NpvService', ['calculate']);
    spy.calculate.and.returnValue(of([]));

    await TestBed.configureTestingModule({      
      imports: [FormsModule, NpvInputComponent],
      providers: [{ provide: NpvService, useValue: spy }]
    }).compileComponents();

    fixture = TestBed.createComponent(NpvInputComponent);
    component = fixture.componentInstance;
    npvServiceSpy = TestBed.inject(NpvService) as jasmine.SpyObj<NpvService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call npvService.calculate on submit with correct input', () => {
    component.cashFlows = '100, 200';
    component.lowerRate = 1;
    component.upperRate = 5;
    component.increment = 1;

    component.onSubmit();

    expect(npvServiceSpy.calculate).toHaveBeenCalledWith({
      cashFlows: [100, 200],
      lowerBoundDiscountRate: 0.01,
      upperBoundDiscountRate: 0.05,
      discountRateIncrement: 0.01
    });
  });

  it('should set error if cashFlows is empty', () => {
    component.cashFlows = '   ,  ,  ';
    component.lowerRate = 1;
    component.upperRate = 10;
    component.increment = 0.25;

    component.onSubmit();

    expect(component.errorMessage).toBe('Please enter valid cash flows.');
    expect(npvServiceSpy.calculate).not.toHaveBeenCalled();
  });


  it('should set error if lowerRate is greater than upperRate', () => {
    component.cashFlows = '100, 200';
    component.lowerRate = 5;
    component.upperRate = 1;
    component.increment = 0.25;

    component.onSubmit();

    expect(component.errorMessage).toBe('Lower rate cannot be greater than upper rate.');
    expect(npvServiceSpy.calculate).not.toHaveBeenCalled();
  });

});
