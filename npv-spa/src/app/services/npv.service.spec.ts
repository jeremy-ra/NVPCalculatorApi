import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NpvService } from './npv.service';
import { NpvInput } from '../models/npv-input.model';
import { NpvResult } from '../models/npv-result.model';

describe('NpvService', () => {
  let service: NpvService;
  let httpMock: HttpTestingController;

  const mockRequest: NpvInput = {
    cashFlows: [200, 300, 400],
    lowerBoundDiscountRate: 1,
    upperBoundDiscountRate: 2,
    discountRateIncrement: 1
  };

  const mockResponse: NpvResult[] = [
    { rate: 1, npv: 50 },
    { rate: 2, npv: 30 }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [NpvService]
    });

    service = TestBed.inject(NpvService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // ensures no pending requests
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send POST request and return NPV results', (done) => {
    service.calculate(mockRequest).subscribe((result) => {
      expect(result).toEqual(mockResponse);
      done();
    });

    const req = httpMock.expectOne('https://localhost:7227/api/npv/calculate');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockRequest);

    req.flush(mockResponse);
  });
});
