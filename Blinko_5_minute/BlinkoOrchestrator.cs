namespace Blinko_5_minute
{
    public interface IBlinkoOrchestrator
    {
        // Define methods that the orchestrator should implement
        IBlinkoFacade BlinkoFacade { get; set; }
        void Start();
        void Stop();
        void ProcessOrder(int orderId);
        void ReplenishInventory(int sku, int quantity);

    }
    public class BlinkoOrchestrator
    {

    }
}
