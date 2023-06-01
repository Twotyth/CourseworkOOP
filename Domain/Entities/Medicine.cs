using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.Entities;

public class Medicine
{
    private string _name = null!;

    public Medicine(uint id, string name, bool isPrescriptionRequired, string type, string typeCategory)
    {
        Id = id;
        Name = name;
        IsPrescriptionRequired = isPrescriptionRequired;
        Type = type;
        TypeCategory = typeCategory;
    }

    public uint Id { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (!Regex.IsMatch(value.Trim(), "^([a-zA-Z][a-zA-Z0-9-]*)+( [a-zA-Z0-9-]+)*$"))
            {
                throw new ArgumentException($"Name for medicine {value} is not valid");
            }
            _name = value.Trim();
        }
    }
    
    

    public bool IsPrescriptionRequired { get; set; }

    public string Type { get; set; }
    
    public string TypeCategory { get; set; }
}