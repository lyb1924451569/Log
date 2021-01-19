using SamJan.LogService.Host.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.LogService.Host.Application.IService
{
    public interface IReceiveLogService : IApplicationService
    {
        Task<PagedResultDto<ReceiveLogDto>> GetReceiveLogsAsync();

        Task<List<ReceiveLogDto>> GetReceiveLogsByInputAsync();
    }
}
