using CommonProject.ViewModels.ContactReport;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.WorkerService.Services;
using System.Text;
using System.Text.Json;

namespace Report.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly ReportHttpClientService _reportHttpClientService;

        private IModel _channel;
        public Worker(RabbitMQClientService rabbitMQClientService, ReportHttpClientService reportHttpClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _reportHttpClientService = reportHttpClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);
            var model = JsonSerializer.Deserialize<ContactReportViewModel>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            if (model != null)
            {
                var result = _reportHttpClientService.DoneRequest(model.Id);
                if(!result.HasFailed && result.Data)
                {
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
        }
    }
}