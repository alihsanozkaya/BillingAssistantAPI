using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using BillingAssistant.Entities.DTOs.OrderDtos;
using BillingAssistant.Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace BillingAssistant.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;
        IMapper _mapper;
        public ProductManager(IProductRepository productRepository, IMapper mapper)
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
        public async Task<IResult> AddProductFromOCR(string filePath)
        {
            List<string> lines = new List<string>();
            List<Product> products = new List<Product>();

            string tessdataPath = @"C:\Users\alihs\.nuget\packages\tesseract\5.2.0\";

            using (var engine = new TesseractEngine(tessdataPath, "tur", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(filePath))
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
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine("lines[{0}]: {1}", i, lines[i]);
            }
            // OCR'den alınan verileri işleme ve veritabanına kaydetme
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
                        // Fatura ile ilgili diğer bilgiler
                        Name = name,
                        Price = price,
                        Unit = unit,
                        InvoiceId = 2
                    };
                    addTasks.Add(AddAsync(productAddDto));
                    startIndex2++;
                }
                return new SuccessResult("Faturalar başarıyla eklendi.");
            }
            else
            {
                // OCR parsing failed
                Console.WriteLine("OCR parsing hatası.");
                return new ErrorResult("OCR parsing hatası.");
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
