using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background worker starting at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Ovde dodaj svoju logiku pozadinskog zadatka,
                    // npr. čišćenje starih podataka ili slanje notifikacija.
                    _logger.LogInformation("Worker is running at: {time}", DateTimeOffset.Now);

                    // Simulacija dužeg zadatka
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Graceful shutdown
                    _logger.LogInformation("Background worker is stopping.");
                    break;
                }
                catch (Exception ex)
                {
                    // Loguj svaku nepredviđenu grešku, ali nastavi sa radom
                    _logger.LogError(ex, "Greška u pozadinskom zadatku");
                }
            }

            _logger.LogInformation("Background worker stopped at: {time}", DateTimeOffset.Now);
        }
    }
}