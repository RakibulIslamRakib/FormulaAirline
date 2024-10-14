namespace FormulaAirline.Api.Services;
public interface IMessageProducer
{
    public void SendMessage<T>(T message );
}