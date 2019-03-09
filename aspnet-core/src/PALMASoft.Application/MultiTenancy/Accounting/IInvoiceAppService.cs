using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using PALMASoft.MultiTenancy.Accounting.Dto;

namespace PALMASoft.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
