using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruefungsvorbereiten
{
  public static class DatatypesMethodParameter
  {
		public static void Test()
    {
			int i = 5;
			long n = 5;
			short s = 5;

			MyMethod(i);
			MyMethod('5');
			MyMethod(i, n);
			MyMethod(n, s);
			//MyMethod(i, s);
			//MyMethod(i, i);
		}

		private static void MyMethod(int x)
		{
			Console.WriteLine("MyMethod(int x)");
		}

		private static void MyMethod(char x)
		{
			Console.WriteLine("MyMethod(char x)");
		}

		private static void MyMethod(int x, long y)
		{
			Console.WriteLine("MyMethod(int x, long y)");
		}

		private static void MyMethod(long x, int y)
		{
			Console.WriteLine("MyMethod(long x, int y)");
		}

	}
}
