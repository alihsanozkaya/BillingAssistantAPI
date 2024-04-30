using AutoMapper;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.InvoiceDtos;
using BillingAssistant.Entities.DTOs.StoreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoicesDto, Invoice>();
            CreateMap<InvoicesDto, Invoice>().ReverseMap();
            CreateMap<InvoiceAddDto, Invoice>();
            CreateMap<InvoiceAddDto, Invoice>().ReverseMap();
            CreateMap<InvoiceUpdateDto, Invoice>();
            CreateMap<InvoiceUpdateDto, Invoice>().ReverseMap();
        }
    }
}
