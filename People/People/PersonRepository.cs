using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People.Models;
using SQLite;

namespace People
{
    public class PersonRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        //private SQLiteConnection conn;
        //make it async:
        private SQLiteAsyncConnection conn;

        private async void Init()
        {
            if (conn != null)
                return;

            //conn = new SQLiteConnection(_dbPath);
            //conn.CreateTable<Person>();

            //make it async:

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;                        
        }

        public async void AddNewPerson(string name)
        {            
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Please enter a name.");

                result = await conn.InsertAsync(new Person { Name = name }).ConfigureAwait(false); 

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            try
            {
                //Call Init() to make sure the DB has been initialized.  
                Init();
                List<Person> l = new List<Person>();
                l  = await conn.Table<Person>().ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
