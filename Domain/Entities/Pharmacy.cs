using Domain.Common;
using Domain.Entities.Users;

namespace Domain.Entities;

public class Pharmacy 
{
    private string _address;

    public Pharmacy(uint id, string address, Pharmacist? workingPharmacist)
    {
        Id = id;
        _address = address;
        Address = address;
        WorkingPharmacist = workingPharmacist;
        Stock = new();
    }

    public uint Id { get; }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException("Pharmacy address must be non-empty string");
            }
            _address = value;
        }
    }

    public ProductStock Stock { get; set; }
    public Pharmacist? WorkingPharmacist { get; set; }
}