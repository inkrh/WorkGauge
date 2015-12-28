using System;
using SQLite;

namespace WorkGauge
{
	public class AnItem
	{
		public AnItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Name { get; set; }
		public string Notes { get; set; }
		public string Skills {get;set;}
		public int QualityScore { get; set;}
		public int CostScore {get;set;}
		public int TimeScore {get;set;}
		public int Busy { get; set; }
		public string Phone {get;set;}
		public string Email {get; set;}
	}
}

