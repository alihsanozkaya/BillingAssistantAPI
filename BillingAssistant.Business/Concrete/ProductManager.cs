using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using BillingAssistant.Entities.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Net.Http.Json;
using Tesseract;

namespace BillingAssistant.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;
        IMapper _mapper;
        public ProductManager(IProductRepository productRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IResult> AddAsync(ProductAddDto entity)
        {
            var newProduct = _mapper.Map<Product>(entity);
            await _productRepository.AddAsync(newProduct);
            return new SuccessResult(Messages.Added);
        }
        public async Task<IResult> AddProductFromOCR(IFormFile file, int invoiceId)
        {
            List<string> lines = new List<string>();
            List<Product> products = new List<Product>();
            string tessdataPath = @"C:\Users\alihs\.nuget\packages\tesseract\5.2.0\";

            using (var engine = new TesseractEngine(tessdataPath, "tur", EngineMode.Default))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var img = Pix.LoadFromMemory(memoryStream.ToArray()))
                    {
                        using (var page = engine.Process(img))
                        {
                            string text = page.GetText();
                            text = text.Replace("TL", "");
                            string[] textLines = text.Split('\n');
                            foreach (string line in textLines)
                            {
                                if (!string.IsNullOrWhiteSpace(line))
                                {
                                    lines.Add(line.Trim());
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine("lines[{0}]: {1}", i, lines[i]);
            }
            if (lines.Any(line => line.Contains("KTÜN MARKET")))
            {
                int startIndex = lines.IndexOf("ÜRÜN AÇIKLAMASI") + 1;
                int endIndex = lines.IndexOf("ÖDEME BİLGİSİ");
                int startIndex2 = lines.IndexOf("ADET FİYAT TOPLAM") + 1;

                if (startIndex != -1 && endIndex != -1 && startIndex2 != -1)
                {
                    List<Task<IResult>> addTasks = new List<Task<IResult>>();

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        string name = lines[i];
                        string[] parts = lines[startIndex2].Split(' ');
                        int unit = int.Parse(parts[0]);
                        double price = double.Parse(parts[1]);

                        ProductAddDto productAddDto = new ProductAddDto
                        {
                            Name = name,
                            Price = price,
                            Unit = unit,
                            InvoiceId = invoiceId
                        };
                        addTasks.Add(AddAsync(productAddDto));
                        startIndex2++;
                    }
                    await Task.WhenAll(addTasks);
                    return new SuccessResult(Messages.InvoicesAddedSuccessfully);
                }
                else
                {
                    return new ErrorResult(Messages.OCRParsingFailed);
                }
            }
            else if (lines.Any(line => line.Contains("A101") || line.Contains("FILE") || line.Contains("MIGROS")))
            {
                List<string> filteredLines = new List<string>();
                bool reachedEnd = false;
                foreach (string line in lines)
                {
                    if (line.Contains("TOPKDV"))
                    {
                        reachedEnd = true;
                    }

                    if (!reachedEnd)
                    {
                        filteredLines.Add(line);
                    }
                }
                foreach (string line in filteredLines)
                {
                    if (line.Contains("*"))
                    {
                        string[] parts = line.Split('*');
                        string productName = parts[0].Trim();

                        int percentIndex = productName.IndexOf('%');
                        if (percentIndex != -1)
                        {
                            productName = productName.Substring(0, percentIndex).Trim();
                        }
                        string pricePart = parts[1].Trim();
                        pricePart = pricePart.Replace(".", ",");
                        string priceString = new string(pricePart.Where(c => char.IsDigit(c) || c == ',').ToArray());
                        double price = double.Parse(priceString);

                        products.Add(new Product { Name = productName, Price = price });
                    }
                }

                List<Task<IResult>> addTasks = new List<Task<IResult>>();
                foreach (var product in products)
                {
                    ProductAddDto productAddDto = new ProductAddDto
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Unit = 1,
                        InvoiceId = invoiceId
                    };
                    addTasks.Add(AddAsync(productAddDto));
                }
                await Task.WhenAll(addTasks);
                return new SuccessResult(Messages.InvoicesAddedSuccessfully);
            }
            else
            {
                return new ErrorResult(Messages.OCRParsingFailed);
            }
        }
        public async Task<IDataResult<bool>> DeleteAsync(int id)
        {
            var isDelete = await _productRepository.DeleteAsync(id);
            return new SuccessDataResult<bool>(isDelete, Messages.Deleted);
        }
        public async Task<IDataResult<ProductsDto>> GetAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetAsync(filter);
            if (product != null)
            {
                var productDto = _mapper.Map<ProductsDto>(product);
                return new SuccessDataResult<ProductsDto>(productDto, Messages.Listed);
            }
            return new ErrorDataResult<ProductsDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<ProductsDto>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetAsync(x=>x.Id ==id);
            if (product != null)
            {
                var productDto = _mapper.Map<ProductsDto>(product);
                return new SuccessDataResult<ProductsDto>(productDto, Messages.Listed);
            }
            return new ErrorDataResult<ProductsDto>(null, Messages.NotListed);
        }
        public async Task<IDataResult<List<ProductsDto>>> GetByInvoiceIdAsync(int invoiceId)
        {
            var products = await _productRepository.GetListAsync(x => x.InvoiceId == invoiceId);
            if (products?.Any() == true)
            {
                var productDtos = _mapper.Map<List<ProductsDto>>(products);
                return new SuccessDataResult<List<ProductsDto>>(productDtos, Messages.Listed);
            }
            return new ErrorDataResult<List<ProductsDto>>(null, Messages.NotListed);
        }
        public async Task<IDataResult<IEnumerable<ProductsDto>>> GetListAsync(Expression<Func<Product, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _productRepository.GetListAsync();
                var responseProductDetailDto = _mapper.Map<IEnumerable<ProductsDto>>(response);
                return new SuccessDataResult<IEnumerable<ProductsDto>>(responseProductDetailDto, Messages.Listed);
            }
            else
            {
                var response = await _productRepository.GetListAsync(filter);
                var responseProductDetailDto = _mapper.Map<IEnumerable<ProductsDto>>(response);
                return new SuccessDataResult<IEnumerable<ProductsDto>>(responseProductDetailDto, Messages.Listed);
            }
        }

        public async Task<IDataResult<List<ProductsByUserDto>>> GetProductsByUserIdAsync(int userId)
        {
            var products = await _productRepository.GetProductsByUserIdAsync(userId);
            if (products?.Any() == true)
            {
                var productDtos = _mapper.Map<List<ProductsByUserDto>>(products);
                return new SuccessDataResult<List<ProductsByUserDto>>(productDtos, Messages.Listed);
            }
            return new ErrorDataResult<List<ProductsByUserDto>>(null, Messages.NotListed);
        }

        public async Task<IDataResult<ProductUpdateDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var getProduct = await _productRepository.GetAsync(x => x.Id == productUpdateDto.Id);
            var product = _mapper.Map<Product>(productUpdateDto);

            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = 1;

            var productUpdate = await _productRepository.UpdateAsync(product);
            var resultUpdateDto = _mapper.Map<ProductUpdateDto>(productUpdate);
            return new SuccessDataResult<ProductUpdateDto>(resultUpdateDto, Messages.Updated);
        }
    }    
}
