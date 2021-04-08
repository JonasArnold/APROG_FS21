using System.Resources;
using System.Globalization;
using System;

namespace SW06.Languages
{
  class Person
  {
    private string name;
    Gender gender;

    public Person(string name, GenderType gender)
    {
      this.name = name;
      this.gender = new Gender(gender);
    }

    public void Print()
    {
      Console.WriteLine($"{this.gender.GetSalutationSentence()} {this.name}");
    }

    public static void SwitchLanguage(CultureInfo culture)
    {
      CultureInfo.CurrentCulture = culture;
    }

  }
}
