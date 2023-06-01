using Application.Services;

namespace Infrastructure.Services;

public class CashPaymentService : IPaymentService
{
    public bool Pay(decimal price) => true;
}