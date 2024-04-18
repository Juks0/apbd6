using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models.DTOs;

public class UpdateAnimal
{
    [MinLength(5)]
    public string Name { get; set; }

    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
    
    public UpdateAnimal()
    {
    }

    public UpdateAnimal(string name, string description, string category, string area)
    {
        Name = name;
        Description = description;
        Category = category;
        Area = area;
    }
}