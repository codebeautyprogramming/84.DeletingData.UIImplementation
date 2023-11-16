using Dapper;
using DataAccessLayer.Contracts;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class IngredientsRepository : IIngredientsRepository
    {
        public async Task AddIngredient(Ingredient ingredient)
        {
            string query = @"insert into Ingredients 
                (Name, Weight, KcalPer100g, PricePer100g, Type) 
                values (@Name, @Weight, @KcalPer100g, @PricePer100g, @Type)";

            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                await connection.ExecuteAsync(query, ingredient);
            }
        }

        public async Task<List<Ingredient>> GetIngredients(string? name="")
        {
            string query = "select * from Ingredients";
            if (!string.IsNullOrEmpty(name))
                query += $" where Name like '{name}%'";

            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                return (await connection.QueryAsync<Ingredient>(query)).ToList();
            }
        }

    }
}
