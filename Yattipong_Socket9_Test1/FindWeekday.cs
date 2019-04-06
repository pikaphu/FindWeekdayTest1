using System;

namespace Yattipong_Socket9_Test1
{
	public class FindWeekday
	{
		#region Variable & Properties

		public enum DayValue // the day of week | start with 1
		{
			Mon = 1,
			Tue = 2,
			Wed = 3,
			Thu = 4,
			Fri = 5,
			Sat = 6,
			Sun = 7
		}

		public enum MonthValue // the month of year for identify name only 
		{
			Jan = 1,
			Feb = 2,
			Mar = 3,
			Apr = 4,
			May = 5,
			Jun = 6,
			Jul = 7,
			Aug = 8,
			Sep = 9,
			Oct = 10,
			Nov = 11,
			Dec = 12
		}

		#endregion

		#region Main

		/// <summary>
		/// Default constructor
		/// </summary>
		void FindWeekDay()
		{
			//Console.WriteLine("Called");
		}

		/// <summary>
		/// Find weekday - upon user input
		/// </summary>
		public void DoFindWeekDay()
		{            
			// Loop input until exit
			do
			{
				// Start ---------------------------------------------------------------------------------
				Console.Clear();
				Console.WriteLine("*************** What weekday is today? ********************");

				// Get and check input date format
				int[] targetDate = GetInputDate();
				if (targetDate is null)
				{
					Console.WriteLine("Error!:");
					continue;
				}

				// Main Process 
				DoFindWeekDay(targetDate[0], targetDate[1], targetDate[2]);


				// just check exit -----------------------------------
				ConsoleKeyInfo tempInput = Console.ReadKey();
				if (tempInput.Key == ConsoleKey.Escape)
				{
					break;
				}
				// End ---------------------------------------------------------------------------------

			} while (true);

		}

		/// <summary>
		/// Find weekday - upon passed value
		/// </summary>
		/// <param name="day"></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		/// <returns>just for unit test</returns>
		public string DoFindWeekDay(int day, int month, int year)
		{
			// 1. Check date correction
			if (CheckInputDate(day, month, year) == false)
			{
				Console.WriteLine("Error!: input date is incorrect");
				return null;
			}

			// 2. Find weekday of input date
			return GetWeekday(day, month, year);
		}

		

		/// <summary>
		/// Get input and check format
		/// </summary>
		/// <returns></returns>
		public int[] GetInputDate()
		{
			Console.Write("Please input the date in pattern: dd/MM/yyyy ");
			Console.Write("( *note. year must equal or more than 1900 [press ctrl+c to exit]) \n :");

			// get input
			string inputDate = Console.ReadLine();

			try
			{
				// check input ----------
				// 1. check pattern of string
				string[] tempDateArr = inputDate.Split('/');
				if (tempDateArr.Length == 3)
				{
					// 2. try convert string to int
					int chkDay = int.Parse(tempDateArr[0]);
					int chkMonth = int.Parse(tempDateArr[1]);
					int chkYear = int.Parse(tempDateArr[2]);

					return new int[] { chkDay, chkMonth, chkYear };
				}
			}
			catch (Exception ex)
			{
				//inputDate = ex.Message;
			}

			Console.WriteLine("ERROR: input date is incorrect! please try again. " + "(" + inputDate + ")");
			return null;
		}

		/// <summary>
		/// Check input date that is valid or not.
		/// </summary>
		/// <param name="day"> input day of month (1 - 31)</param>
		/// <param name="month"> input month as number (1 - 12) </param>
		/// <param name="year"> input year as number (1900) </param>
		public bool CheckInputDate(int day, int month, int year)
		{
			// Check date correction

			// 1. check year
			if (year >= 1900 && year <= int.MaxValue)
			{
				// 2. check month
				if (month >= 1 && month <= 12)
				{
					// 3. check day
					if (day >= 1 && day <= 31)
					{
						if (day <= GetTotalDaysInMonth(month, year))
						{
							return true;
						}
					}
				}
			}

			return false; // incorrect date format
		}


		/// <summary>
		/// Find weekday of given date
		/// </summary>
		/// <param name="day"> input day of month (1 - 31)</param>
		/// <param name="month"> input month as number (1 - 12) </param>
		/// <param name="year"> input year as number (1900) </param>
		public string GetWeekday(int day, int month, int year)
		{   /* FACT *********************************************************************************************************************************
			- First date is 1/1/1900 
			- First day of week is Monday
			- A week has 7 days (1:Mon - 7:Sun)  *we will use 0:Mon - 6:Sun for calculate*
			- So last date of week is 7/1/1900 and is Sunday
			- Then repeat +7 days each week will get the same weekday (ex. Next date after first week is 8/1/1900 (Mon) -  14/1/1900 (Sun) )
			- Ex. if a date that different from first date  0 days = Monday, 
															1 days = Tuesday, 
															2 days = Wednesday, ....
															6 days = Sunday.
					and repeat: 7 days = Monday = 0 days of first week, 
								8 days = Tuesday = 1 days of first week;
								So, we can use Modulo with 7.
			***************************************************************************************************************************************/

			/* Solution ****************************************************************************************************************************
			1. Subtract target date from the first date and we will get the total number of different days [ex. (13/1/1900 -  1/1/1900) --> 12].
			2. Then modulo the number with total week days we will get the day in week (<=7) [ex. (12 % 7) + 1 --> 6].  **+1 because we start day in week (Monday) with 1**
			3. Then compare the number with the day of week, it is the name of that day (Mon:1 to Sun:7), will get weekday [6 = Saturday]
			4. Finally, result is [13/1/1900 is Saturday]... right? 
			*************************************************************************************************************************************/

			// Set first date [Fact]
			//int[] firstDate = { 1, 1, 1900 };
			int firstDateDay = 1;
			int firstDateMonth = 1;
			int firstDateYear = 1900;
			int firstDateWeekday = (int)DayValue.Mon; // Monday

			// Set target date 
			int targetDateDay = day;
			int targetDateMonth = month;
			int targetDateYear = year;
			int targetWeekday; // don't known yet.


			// Calculate with the solution            
			// 1. Find total different days between target date and first date.
			int sumOfDiffDays = 0; // calculate from current days + days in months + days in years

			// 1.1 years --> calculate sum of normal years and leap years
			for (int i = firstDateYear;  i < targetDateYear; i++)
			{
				sumOfDiffDays += 365;

				if (IsLeapYear(i))
				{
					sumOfDiffDays += 1; // 366
				}
			}

			// 1.2 month --> calculate number of days of months in the current year
			for (int i = firstDateMonth; i < targetDateMonth; i++)
			{
				sumOfDiffDays += GetTotalDaysInMonth(i, targetDateYear);
			}

			// 1.3 current days [End]
			sumOfDiffDays += targetDateDay - firstDateDay;

			// 2. Then calculate day in Week
			targetWeekday = (sumOfDiffDays % 7) + firstDateWeekday;

			// 3. Compare day to enum
			string weekDayName = GetWeekDayName(targetWeekday);

			// 4. Result
			Console.WriteLine( string.Format("Weekday of {0} {1}, {2} is {3}",
				GetMonthName(targetDateMonth),
				targetDateDay,
				targetDateYear,
				weekDayName)
				);

			return weekDayName.ToLower(); // just for unit test

		}


		#endregion

		#region Helper

		/// <summary>
		/// Check leap year condition:  https://www.timeanddate.com/date/leapyear.html
		/// </summary>
		/// <param name="year"></param>
		/// <returns></returns>
		public bool IsLeapYear(int year)
		{
			// 1. The year can be evenly divided by 4;
			// 2. If the year can be evenly divided by 100, it is NOT a leap year, 
			//    unless the year is also evenly divisible by 400.Then it is a leap year.
			if (year % 4 == 0)
			{
				if (year % 100 == 0)
				{
					if (year % 400 == 0)
					{
						return true;
					}

					return false;
				}
				else
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Check how many days in given months of selected year
		/// </summary>
		/// <param name="month"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		public int GetTotalDaysInMonth(int month, int year)
		{
			// check months that have 30 days
			if (month == (int)MonthValue.Apr ||
				month == (int)MonthValue.Jun ||
				month == (int)MonthValue.Sep ||
				month == (int)MonthValue.Nov)
			{
				return 30;
			}
			else if (month == (int)MonthValue.Feb)
			{
				// check February with leap years: 28 --> 29 days
				if (IsLeapYear(year))
				{
					return 29;
				}
				else
				{
					return 28;
				}
			}
			else
			{
				// another are 31 days
				return 31;
			}
		}

		/// <summary>
		/// Get the name of day
		/// </summary>
		/// <param name="day"> number of day start with 1:Monday </param>
		/// <returns></returns>
		private string GetWeekDayName(int day)
		{
			string weekDayName = "";
			switch (day)
			{
				case (int)DayValue.Mon: weekDayName = "Monday";
					break;
				case (int)DayValue.Tue: weekDayName = "Tuesday";
					break;
				case (int)DayValue.Wed: weekDayName = "Wednesday";
					break;
				case (int)DayValue.Thu: weekDayName = "Thursday";
					break;
				case (int)DayValue.Fri: weekDayName = "Friday";
					break;
				case (int)DayValue.Sat: weekDayName = "Saturday";
					break;
				case (int)DayValue.Sun: weekDayName = "Sunday";
					break;
				default:
					break;
			}

			return weekDayName;
		}

		/// <summary>
		/// Get name of month
		/// </summary>
		/// <param name="month"></param>
		/// <returns></returns>
		private string GetMonthName(int month)
		{
			string monthName = "";
			switch (month)
			{
				case (int)MonthValue.Jan: monthName = "January";
					break;
				case (int)MonthValue.Feb: monthName = "February";
					break;
				case (int)MonthValue.Mar: monthName = "March";
					break;
				case (int)MonthValue.Apr: monthName = "April";
					break;
				case (int)MonthValue.May: monthName = "May";
					break;
				case (int)MonthValue.Jun: monthName = "June";
					break;
				case (int)MonthValue.Jul: monthName = "July";
					break;
				case (int)MonthValue.Aug: monthName = "August";
					break;
				case (int)MonthValue.Sep: monthName = "September";
					break;
				case (int)MonthValue.Oct: monthName = "October";
					break;
				case (int)MonthValue.Nov: monthName = "November";
					break;
				case (int)MonthValue.Dec: monthName = "December";
					break;
				default:
					break;
			}

			return monthName;
		}

		#endregion
	}
}
