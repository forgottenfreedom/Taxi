using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Taxi.Models;

namespace Taxi.Data
{
    public class TaxiFahrpreisDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TaxiFahrpreisDatabase> Instance = new AsyncLazy<TaxiFahrpreisDatabase>(async () =>
        {
            var instance = new TaxiFahrpreisDatabase();
            CreateTableResult result = await Database.CreateTableAsync<TaxiFahrpreis>();
            return instance;
        });

        public TaxiFahrpreisDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TaxiFahrpreis>> GetItemsAsync()
        {
            return Database.Table<TaxiFahrpreis>().ToListAsync();
        }

        public Task<List<TaxiFahrpreis>> GetFahrpreisAsync(string param1)
        {
            return Database.QueryAsync<TaxiFahrpreis>($"SELECT [{param1}] FROM [TaxiFahrpreis]");
        }
        public Task<List<TaxiFahrpreis>> GetTrinkgeldAsync(string param1)
        {
            return Database.QueryAsync<TaxiFahrpreis>($"SELECT * FROM [TaxiFahrpreis] WHERE [Schichttag] = '{param1}'");
        }
        public Task<List<TaxiFahrpreis>> GetKreditAsync()
        {
            return Database.QueryAsync<TaxiFahrpreis>("SELECT [Kredit] FROM [TaxiFahrpreis]");
        }

        public Task<TaxiFahrpreis> GetItemAsync(int id)
        {
            return Database.Table<TaxiFahrpreis>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TaxiFahrpreis item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TaxiFahrpreis item)
        {
            return Database.DeleteAsync(item);
        }
    }
}