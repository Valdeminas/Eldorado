using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EldoradoApi.Domain.Services;
using Microsoft.Extensions.Logging;

internal interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}

internal class ScopedProcessingService : IScopedProcessingService
{
    const int frequency= 60000;
    private readonly ILogger _logger;
    private readonly IOrderService _orderService;

    public ScopedProcessingService(ILogger<ScopedProcessingService> logger,IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
            "Starting expired order clean-up.");
            var deletedOrders = await _orderService.RemoveExpiredOrders();
            if (deletedOrders.Success)
                _logger.LogInformation(
            "Success! "+deletedOrders.Orders.Count()+" orders deleted successfully.");
            else
                _logger.LogInformation(
            "Error! Orders were not deleted. Error "+deletedOrders.Message);



            await Task.Delay(frequency, stoppingToken);
        }
    }
}