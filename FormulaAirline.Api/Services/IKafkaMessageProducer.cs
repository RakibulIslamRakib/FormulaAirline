namespace FormulaAirline.Api.Services;

public interface IKafkaMessageProducer
{
    public Task SendMessageAsync<T>(T message );
}