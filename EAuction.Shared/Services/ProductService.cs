using EAuction.Shared;
using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAuction.Shared.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductToBuyerRepository _productToBuyerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly IServiceBusMessageProducer _serviceBusMessageSender;
        public ProductService(IProductRepository productRepository, IProductToBuyerRepository productToBuyerRepository, IUserRepository userRepository, IRabbitMqProducer rabbitMqProducer, IServiceBusMessageProducer serviceBusMessageSender)
        {
            if (productRepository is null)
            {
                throw new ArgumentException(nameof(productRepository));
            }

            if (productToBuyerRepository is null)
            {
                throw new ArgumentException(nameof(productToBuyerRepository));
            }

            if (userRepository is null)
            {
                throw new ArgumentException(nameof(userRepository));
            }

            if (rabbitMqProducer is null)
            {
                throw new ArgumentException(nameof(rabbitMqProducer));
            }
            if (serviceBusMessageSender is null)
            {
                throw new ArgumentException(nameof(serviceBusMessageSender));
            }

            _productRepository = productRepository;
            _productToBuyerRepository = productToBuyerRepository;
            _userRepository = userRepository;
            _rabbitMqProducer = rabbitMqProducer;
            _serviceBusMessageSender = serviceBusMessageSender;
        }

        public async Task<int> AddBidForProductAsync(BuyerBid product)
        {
            var existingProduct = await _productRepository.GetProductByIDAsync(product.ProductId);
            if (existingProduct == null)
            {
                throw new ProductNotFounException($"{Constants.ProductNotFound}");
            }

            if (existingProduct.BidEndDate <= DateTime.Today)
            {
                throw new ProductException($"{Constants.BidPlaceError}");
            }

            var user = await _userRepository.GetUserByEmailAsync(product.Email);

            if(user == null)
            {
                throw new UserNotFounException($"{Constants.UserNotFound}");
            }

            var productToBuyer = new ProductToBuyer()
            {
                BidAmount = product.BidAmount,
                ProductId = product.ProductId,
                UserID = user.UserId
            };
            var prod = await _productToBuyerRepository.CreateOrUpdateAsync(productToBuyer);
            //_rabbitMqProducer.Publish(String.Format($"{Constants.NewBid}", product.FirstName, product.LastName));
            await _serviceBusMessageSender.SendMessageAsync(String.Format($"{Constants.NewBid}", product.FirstName, product.LastName));
            return prod;
        }

        public async Task<int> CreateOrUpdateProductAsync(Product product)
        {
          
                var newProduct = await _productRepository.CreateOrUpdateAsync(product);
                return newProduct;
        }

        public async Task<int> DeleteAsync(string ProductID)
        {
            var existingProduct = await _productRepository.GetProductByIDAsync(ProductID);
            if (existingProduct == null)
            {
                throw new ProductNotFounException($"{Constants.ProductNotFound}");
            }

            var bids = await _productToBuyerRepository.GetBidByProductIDAsync(ProductID);

            if (bids != null && bids.Count() > 0)
            {
                foreach (var item in bids)
                {
                    if (item.CreatedDate > existingProduct.BidEndDate)
                    {
                        throw new ProductException($"{Constants.ProductCannotDelete}");
                    }
                }
                throw new ProductException($"{Constants.ProductCannotDelete}");
            }
            
            return await _productRepository.DeleteAsync(ProductID);
        }


        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return products;
        }

        public async Task<ProductBids> GetbidsByProductID(string ProductID)
        {
            var prod = await _productRepository.GetProductByIDAsync(ProductID);
            var bids = await _productToBuyerRepository.GetBidByProductIDAsync(ProductID);

            if (bids == null)
            {
                throw new ProductNotFounException($"{Constants.ProductBidNotFound}");
            }

            var productbids = new ProductBids()
            {
                ProductName = prod.ProductName,
                ShortDescription = prod.ShortDeceription,
                DetailedDeceription = prod.DetailedDeceription,
                Category = prod.Category,
                BidEndDate = prod.BidEndDate,
                StartingPrice = prod.StartingPrice
            };

            var bidDetails = new List<BidDetails>();
            foreach (var item in bids)
            {
                var existingUser = await _userRepository.GetUserByIDAsync(item.UserID);
                var prodbid = new BidDetails()
                {
                    BidAmount = item.BidAmount,
                    BuyerName = string.Format($"{existingUser.FirstName} {existingUser.LastName}"),
                    EmailId = existingUser.Email,
                    Phone = existingUser.Phone
                };
                bidDetails.Add(prodbid);
            }
            productbids.BidDetails = bidDetails.OrderByDescending(x => x.BidAmount).ToList();
            //_rabbitMqProducer.Receive();
            return productbids;
        }

        public async Task<int> UpdateBidForProduct(string ProductID, string buyerEmail, decimal amount)
        {
            var user = await _userRepository.GetUserByEmailAsync(buyerEmail);
            if (user == null)
            {
                throw new UserNotFounException($"{Constants.UserNotFound}");
            }
            var result = await _productToBuyerRepository.GetProductByUserIDAsync(ProductID, user.UserId);
            result.BidAmount = amount;
            return await _productToBuyerRepository.CreateOrUpdateAsync(result);
        }
    }
}
