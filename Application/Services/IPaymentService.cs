using Application.Enums;

namespace Application.Services;

public interface IPaymentService
{
    public bool Pay(decimal price);
}