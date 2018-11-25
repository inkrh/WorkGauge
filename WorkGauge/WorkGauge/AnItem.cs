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

        //TODO
        public int SCMVersioning { get; set; }
        public int Binaries { get; set; }
        public int Dependencies { get; set; }
        public int Branching { get; set; }
        public int SecurityInTransit { get; set;}
        public int VulnerabilityScan { get; set; }
        public int StaticCodeAnalysis { get; set; }
        public int TestLevels { get; set; }
        public int PeerCodeReviews { get; set; }

	}
}

