using System.Text.Json;
using System.Text.RegularExpressions;

namespace PrescriptionMedicineProvider;

static class Program
{
    private static string _path = @"C:\Users\timka\source\CS repos\Coursework OOP\PrescriptionMedicineProvider\.repos";

    private static string _med = Path.Combine(_path, "medicine_repos.json");
    private static string _presc = Path.Combine(_path, "prescription_repos.json");
    
    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                @"
1 - Add prescription
2 - View Prescriptions
3 - Add medicine
4 - Delete medicine
5 - View medicines
6 - Close"
            );
            
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    CreateIfNotExist();
                    AddPrescription();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    CreateIfNotExist();
                    ShowPrescriptions();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    CreateIfNotExist();
                    AddMedicine();
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    CreateIfNotExist();
                    DeleteMedicine();
                    break;
                case ConsoleKey.D5:
                    Console.Clear();
                    CreateIfNotExist();
                    ShowMedicines();
                    break;
                case ConsoleKey.D6: return;
                
                default: continue;
            }
        }
    }

    private static void ShowMedicines()
    {
        var med = JsonSerializer.Deserialize<List<Medicine>>(File.ReadAllText(_med))!;

        foreach (var medicine in med)
        {
            Console.WriteLine($"{medicine.Id}: {medicine.Name}, requires prescription: {medicine.IsPrescriptionRequired}");
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void DeleteMedicine()
    {
        var meds = JsonSerializer.Deserialize<List<Medicine>>(File.ReadAllText(_med));
        uint id;


        while (true)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Type in medicine's ID to delete:");
            Console.ResetColor();
            try
            {
                id = uint.Parse(Console.ReadLine()!);
                meds!.Remove(meds.Find(med => med.Id == id)!);
                break;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
            }
        }
        
        File.WriteAllText(_med, JsonSerializer.Serialize(meds));
    }

    private static void AddMedicine()
    {
        var meds = JsonSerializer.Deserialize<List<Medicine>> (File.ReadAllText(_med));
        Medicine med = new Medicine(0, "aa", true, "", "");
        

        while (true)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Write name of the new medicine:");
            Console.ResetColor();
            try
            {
                med.Name = Console.ReadLine()!.Trim();
                break;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
            }
        }
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press '+' if the medicine will need prescription to be sold:");
        
        med.IsPrescriptionRequired = 
            Console.ReadKey(true).KeyChar switch
            {
                '+' => true,
                _ => false
            };
        
        Console.Clear();
        
        while (true)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Write type of the new medicine:");
            Console.ResetColor();
            try
            {
                med.Type = Console.ReadLine()!.Trim();
                break;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
            }
        }
        
        while (true)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Write category of the new medicine:");
            Console.ResetColor();
            try
            {
                med.TypeCategory = Console.ReadLine()!.Trim();
                break;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
            }
        }

        meds!.Add(med);
        
        File.WriteAllText(_med, JsonSerializer.Serialize(meds));
    }

    private static void ShowPrescriptions()
    {
        var presc = JsonSerializer.Deserialize<List<PrescriptionDto>>(File.ReadAllText(_presc))!;

        foreach (var prescription in presc)
        {
            Console.WriteLine($"{prescription.Id}: {string.Join(' ', prescription.PrescribedMedicineId)}");
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void CreateIfNotExist()
    {
        if (!File.Exists(_presc))
        {
            File.WriteAllText(_presc, JsonSerializer.Serialize(new List<PrescriptionDto>()));
        }

        if (!File.Exists(_med))
        {
            File.WriteAllText(_med, JsonSerializer.Serialize(new List<Medicine>()));
        }
    }

    private static void AddPrescription()
    {
        var meds = JsonSerializer.Deserialize<List<Medicine>>(File.ReadAllText(_med));
        var presc = JsonSerializer.Deserialize<List<PrescriptionDto>>(File.ReadAllText(_presc))!;

        if (meds == null || meds.Count == 0)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Cannot add new prescriptions -- medicine must be present");
            Console.ReadKey(true);
            return;
        }
        
        var medicineIds = Array.Empty<uint>();
        
        
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Write medicine ids separated by a whitespace ' ':");
        Console.ResetColor();

        while (true)
        {
            try
            {
                medicineIds = Console.ReadLine()!.Split(' ').Select(uint.Parse).ToArray();
                if (medicineIds.Any(id => meds.All(med => med.Id != id)))
                {
                    throw new Exception("One or more of listed IDs are of not existing medicine");
                }
                break;
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        
        presc.Add(new (presc.Any() ? presc.Max(i => i.Id) : 0, medicineIds));
        
        File.WriteAllText(_presc, JsonSerializer.Serialize(presc));
    }
}

public class PrescriptionDto
{
    public PrescriptionDto(uint id, IEnumerable<uint> prescribedMedicineId)
    {
        Id = id;
        PrescribedMedicineId = prescribedMedicineId;
    }

    public uint Id { get; set; }
    public IEnumerable<uint> PrescribedMedicineId { get; set; }
}

public class Medicine
{
    private string _name = null!;
    private string _type = null!;
    private string _typeCategory = null!;

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

    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException($"Type name {value} is not valid");
            }
            _type = value;
        }
    }

    public string TypeCategory
    {
        get => _typeCategory;
        set
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException($"Category name {value} is not valid");
            }
            _typeCategory = value;
        }
    }
}