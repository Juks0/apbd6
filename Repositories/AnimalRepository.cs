﻿using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    private IAnimalRepository _animalRepositoryImplementation;
    
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy};";
        var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                idAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                name = reader.GetString(reader.GetOrdinal("Name")),
                description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                area = reader.IsDBNull(reader.GetOrdinal("Area")) ? null : reader.GetString(reader.GetOrdinal("Area"))
            });
        }
        return animals;
    }

    public void AddAnimal(Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@name, @description, @category, @area);";
        command.Parameters.AddWithValue("@Name", animal.name);
        command.Parameters.AddWithValue("@Description", (object)animal.description ?? DBNull.Value);
        command.Parameters.AddWithValue("@Category", (object)animal.category ?? DBNull.Value);
        command.Parameters.AddWithValue("@Area", (object)animal.area ?? DBNull.Value);
        command.ExecuteNonQuery();
    }
    
    public Animal GetAnimalById(int idAnimal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Animal WHERE IdAnimal = @idAnimal;";
            command.Parameters.AddWithValue("idAnimal", idAnimal);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Animal()
                {
                    idAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                    name = reader.GetString(reader.GetOrdinal("Name")),
                    description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                    area = reader.IsDBNull(reader.GetOrdinal("Area")) ? null : reader.GetString(reader.GetOrdinal("Area"))
                };
            }
            return null;
        }

    public void UpdateAnimal(int idAnimal, Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @IdAnimal;";
        command.Parameters.AddWithValue("@IdAnimal", idAnimal);
        command.Parameters.AddWithValue("@Name", animal.name);
        command.Parameters.AddWithValue("@Description", (object)animal.description ?? DBNull.Value);
        command.Parameters.AddWithValue("@Category", (object)animal.category ?? DBNull.Value);
        command.Parameters.AddWithValue("@Area", (object)animal.area ?? DBNull.Value);
        command.ExecuteNonQuery();
    }
        public void DeleteAnimal(int idAnimal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @idAnimal;";
            command.Parameters.AddWithValue("idAnimal", idAnimal);
            command.ExecuteNonQuery();
        }
}