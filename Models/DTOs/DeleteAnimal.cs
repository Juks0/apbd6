namespace Tutorial5.Models.DTOs;

public class DeleteAnimal
{
    public int IdAnimal { get; set; }

    public DeleteAnimal()
    {
    }

    public DeleteAnimal(int idAnimal)
    {
        IdAnimal = idAnimal;
    }
}