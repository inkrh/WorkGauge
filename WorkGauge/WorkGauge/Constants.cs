using System;
using System.Collections.Generic;

namespace WorkGauge
{
	public static class Constants
	{
		public static List<AnItem> CurrentItems { get; set; }
		public static AnItem CurrentItem {get; set;}
		public static string Where {get; set;}

		public static string Results = "Results";
		public static string Detail = "Detail";
		public static string Home = "Home";

		public static double Width{ get; set; }
		public static double Height {get; set;}
		public static double TopRowHeight {get;set;}
		public static double DataRowContentHeight {get; set;}
		public static double ResultsTableHeight {get; set;}
		public static double BottomButtonRowHeight {get;set;}
	}
}

