using System;
using System.Globalization;

namespace SW06.Languages
{
  class Program
  {
    static void Main(string[] args)
    {
      Person jonas = new Person("Jonas", GenderType.Male);
      Person simi = new Person("Simi", GenderType.Generic);

      jonas.Print();
      CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
      simi.Print();
    }
  }
}
