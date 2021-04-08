using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace SW06.Languages
{
  internal enum GenderType
  {
    Male = 0,
    Female,
    Generic
  }

  internal class Gender
  {
    private GenderType gender;
    public Gender(GenderType gender)
    {
      this.gender = gender;
    }

    private static Dictionary<GenderType, string> GenderSalutationResourceAssociation = new Dictionary<GenderType, string>()
    {
      { GenderType.Male, Constants.ResourcesSalutationMaleName },
      { GenderType.Female, Constants.ResourcesSalutationFenaleName },
      { GenderType.Generic, Constants.ResourcesSalutationGenericName }
    };

    public string GetSalutationSentence()
    {
      return GetTextFromResourcesWithCurrentCulture(GenderSalutationResourceAssociation[this.gender]);
    }

    private static string GetTextFromResourcesWithCurrentCulture(string resourceName)
    {
      var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      var assemblyName = executingAssembly.GetName().Name;
      return new ResourceManager($"{assemblyName}.Properties.text", executingAssembly).GetString(resourceName, CultureInfo.CurrentCulture);
    }
  }
}
