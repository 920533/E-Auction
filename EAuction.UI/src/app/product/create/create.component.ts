import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductInfo } from '../../shared/interfaces/productInfoparam';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-create-product',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  categories: any = ['Painting', 'Ornament', 'Sculptor'];
  sellerId: number = 0;
  selCategory: string = "";
  errorMessage: string = "";

  productName = new FormControl('', [Validators.required, Validators.maxLength(30)]);
  description = new FormControl('', [Validators.required, Validators.maxLength(50)]);
  sdescription = new FormControl('', [Validators.required, Validators.maxLength(30)]);
  startPrice = new FormControl('', [Validators.pattern('^[0-9]*$'), Validators.required]);
  endDate = new FormControl('', [Validators.required]);

  formData = new FormData();

  lstSeller: any[] = [];
  lstProducts: any[] = [];
  constructor(private productService: ProductService, private router: Router, private titleService: Title) {
    titleService.setTitle('Create product');
  }

  ngOnInit(): void {
    this.getAllproducts();
    this.getAllSeller();
  }



  create() {
    if (this.sellerId == 0) {
      this.errorMessage = "Please select a product seller.";
      return;
    }
    else if (this.productName.value == "" || this.productName.value == null) {
      this.errorMessage = "Product name should not be empty and not more then 30 character.";
      return;
    }
    else if (this.sdescription.value == "" || this.sdescription.value == null) {
      this.errorMessage = "Short decription should not be empty.";
      return;
    }
    else if (this.description.value == "" || this.description.value == null) {
      this.errorMessage = "Decription should not be empty.";
      return;
    }
    else if (this.selCategory == "") {
      this.errorMessage = "Please select a product category.";
      return;
    }
    else if (this.startPrice.value == "" || this.startPrice.value == null) {
      this.errorMessage = "Start price should not be empty.";
      return;
    }
    else if (this.endDate.value == "" || this.endDate.value == null) {
      this.errorMessage = "End date should not be empty.";
      return;
    }

    let productparam: ProductInfo = new ProductInfo();
    productparam.SellerId = this.sellerId;
    productparam.BidEndDate = new Date(this.endDate.value);
    productparam.Category = this.selCategory;
    productparam.DetailedDescription = this.description.value;
    productparam.ProductName = this.productName.value;
    productparam.ShortDescription = this.sdescription.value;
    productparam.StartingPrice = this.startPrice.value;
    this.productService.addProduct(productparam).subscribe(data => {
      this.sellerId = 0;
      this.selCategory = "";
      this.endDate.reset();
      this.description.reset();
      this.productName.reset();
      this.sdescription.reset();
      this.startPrice.reset();
      this.getAllproducts();
    });

  }


  getAllSeller() {
    this.productService.getAllSeller().subscribe(data => {
      this.lstSeller = data;
    });
  }

  getAllproducts() {
    this.productService.getAllproducts().subscribe(data => {
      this.lstProducts = data;
    });
  }

  deleteProducts(id: number) {
    this.productService.deleteProduct(id).subscribe(data => {
      this.getAllproducts();
    });
  }
}
