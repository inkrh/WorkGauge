using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace WorkGauge
{
	public class ItemDatabase 
	{
		static object locker = new object ();

		SQLiteConnection database;

		public ItemDatabase()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			// create the tables
			database.CreateTable<AnItem>();
		}

		public IEnumerable<AnItem> GetItems ()
		{
			lock (locker) {
				return (from i in database.Table<AnItem>() select i).ToList();
			}
		}

		public IEnumerable<AnItem> GetItemsNotDone ()
		{
			lock (locker) {
				return database.Query<AnItem>("SELECT * FROM [AnItem] WHERE [Busy] = 0");
			}
		}

		public AnItem GetItem (int id) 
		{
			lock (locker) {
				return database.Table<AnItem>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveItem (AnItem item) 
		{
			lock (locker) {
				if (item.ID != 0) {
					database.Update(item);
					return item.ID;
				} else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker) {
				return database.Delete<AnItem>(id);
			}
		}
	}
}

