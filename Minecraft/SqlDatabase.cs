using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Minecraft
{
    public class SqlDatabase
    {
        private string _connectionString { get; set; }

        public SqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public async Task<List<ItemData>> DeleteSelectedQuantity(string name, int quantity)
        //{
        //    List<ItemData> results = new List<ItemData>();
        //    var query = $"DELETE {quantity} FROM Item WHERE (Name = {name})";
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var results = connection.Query<ItemData>(query).ToList();
        //        Console.WriteLine($"you used {quantity} {name}");
                
        //    }

        //    return results;
        //}
        public async Task<List<Block>> GetBlocks()
        {
            List<Block> blocks = new List<Block>();
            var query = $"Select * From Block";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                blocks = connection.Query<Block>(query).ToList();
            }

            return blocks;
        }
        public async Task<HashSet<Block>> StevesBlocks()
        {
            HashSet<Block> blocks = new HashSet<Block>();
            var query = $"Select * From MinedBlocks";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
               var minedBlocks = connection.Query<Block>(query);
               blocks.UnionWith(minedBlocks);
            }

            return blocks;
        }
        public async Task<List<MonsterData>> GetMonsters()
        {
            List<MonsterData> monsters = new List<MonsterData>();
            var query = "Select * From Monster WHERE (IsNetherCastle = 0 AND IsNether = 0)";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                monsters = connection.Query<MonsterData>(query).ToList();
            }

            return monsters;
        }

        public async Task<List<MonsterLoot>> GetMonsterLootId(int monsterId)
        {
            List<MonsterLoot> monsterLoot = new List<MonsterLoot>();
            var query = $"SELECT * FROM MonsterLoot WHERE MonsterId = {monsterId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                monsterLoot = connection.Query<MonsterLoot>(query).ToList();
            }

            return monsterLoot;
        }
        public async Task<List<MonsterLoot>> GetMonsterLoot(string userInput)
        {
            List<MonsterLoot> monsterLoot = new List<MonsterLoot>();
            var query = $"SELECT * FROM MonsterLoot WHERE Name == @userInput";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                monsterLoot = connection.Query<MonsterLoot>(query, new {userInput}).ToList();
            }

            return monsterLoot;
        }

        public async Task<List<ItemData>> GetItemData()
        {
            List<ItemData> Items = new List<ItemData>();
            var query = "SELECT * FROM Item";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Items = connection.Query<ItemData>(query).ToList();
            }

            return Items;
        }

        //public async Task RemoveItem(string itemName)
        //{
        //    var query = $@"DELETE TOP (1) FROM Item WHERE Name = @itemName";
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var rest = connection.Execute(query, new {itemName});
        //    }
        //}
        public async Task RemoveItem(string itemName, int quantity)
        {
            var query = $@"DELETE TOP({quantity}) FROM Item WHERE Name = @itemName";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var rest = connection.Execute(query, new { itemName });
                if (rest > 0)
                {
                    Console.WriteLine($"du brukte {quantity} {itemName}");
                }
            }
        }

        public async Task AddNewItem(Item item)
        {
            var query = $@"INSERT INTO Item (Name, Quantity)
            VALUES(@Name, @Quantity)";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = connection.Execute(query, new {item.Name, item.Quantity});
                if (result > 0)
                {
                    Console.WriteLine($"Item added");
                }
            }
        }

        public async Task<List<MonsterData>> NetherMonsters()
        {
            List<MonsterData> monsters = new List<MonsterData>();
            var query = "Select * From Monster WHERE IsNether = 1";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                monsters = connection.Query<MonsterData>(query).ToList();
            }

            return monsters;
        }

        public async Task AddBlocks(HashSet<Block> blocks)
        {
            var query = $@"INSERT INTO MinedBlocks (Name, Quantity)
            VALUES(@Name, @Quantity)";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                foreach (var block in blocks)
                {
                    var result = connection.Execute(query, new {block.Name, block.Quantity});
                    if (result > 0) //sier noe om antall rader, f.eks. rad 1 name = diamond
                    {
                        //Console.WriteLine($"Blocks added");
                    }
                }

            }
        }
        public async Task RemoveSelectedBlock(string userInput, int quantity)
        {
            var query = $@"DELETE TOP ({quantity}) FROM MinedBlocks WHERE Name = @userInput";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                    var result =  connection.Execute(query, new {userInput});
                    if (result > 0) //sier noe om antall rader, f.eks. rad 1 name = diamond
                    {
                        Console.WriteLine($"Du brukte {quantity} {userInput}");
                    }
            }
        }

        public async Task AddBlock(Block block)
        {
            var query = $@"INSERT INTO MinedBlocks (Name, Quantity)
            VALUES(@Name, @Quantity)";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result =  connection.Execute(query, new { block.Name, block.Quantity });
                    if (result > 0) //sier noe om antall rader, f.eks. rad 1 name = diamond
                    {
                        //Console.WriteLine($"Block added");
                    }

            }
        }
    }
}
