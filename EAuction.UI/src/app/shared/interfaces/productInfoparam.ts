export class ProductInfo {
  SellerId: number;
  ProductName: string;
  ShortDescription: string;
  DetailedDescription: string;
  Category: string;
  StartingPrice: number
  BidEndDate: Date;
  IsDeleted: boolean = false;
}
