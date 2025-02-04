using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.SharedKernel.Utils
{
    public class EnviarMensagemServiceBus : IEnviarMensagemServiceBus
    {
        private readonly IConfiguration _configuration;

        public EnviarMensagemServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarMensagemParaFila(string fila, object mensagem)
        {
            await using (ServiceBusClient client = new ServiceBusClient(_configuration.GetSection("ServiceBusConnection").Value))
            {
                ServiceBusSender sender = client.CreateSender(fila);

                var messageJson = JsonConvert.SerializeObject(mensagem);

                ServiceBusMessage message = new ServiceBusMessage(messageJson);

                await sender.SendMessageAsync(message);
            }
        }
    }
}
