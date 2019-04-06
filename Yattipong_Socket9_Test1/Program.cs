/* Yattipong Sriburee *************************************
 * 7 April 2019 *
 * Socket9 Test *
 * 1. FindWeekday.cs (Main)
 * 2. PhuTestCase.cs (unit test)
 * 
 * - Visual Studio 2017
 * - DotNET Framework v4.5
 * - NUnit v3.11.0
 * - NUnit3TestAdapter 3.13.0.
***********************************************************/

using System;


/// <summary>
/// Main Program
/// </summary>
namespace Yattipong_Socket9_Test1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Call main function DoFindWeekDay()
			FindWeekday test1 = new FindWeekday();
			test1.DoFindWeekDay();
		}

		
	}
}
