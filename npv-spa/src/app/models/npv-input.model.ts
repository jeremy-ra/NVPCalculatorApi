export interface NpvInput {
    cashFlows: number[];
    lowerBoundDiscountRate: number;
    upperBoundDiscountRate: number;
    discountRateIncrement: number;
  }