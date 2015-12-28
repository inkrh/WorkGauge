using System;
using SQLite;

namespace WorkGauge
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

