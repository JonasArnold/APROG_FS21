using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruefungsvorbereiten
{
  public interface I1
  {
    void Method();
  }
  public interface I2
  {
    void Method();
  }

  public class ExplicitInterfaceCall : I1, I2
  {
    public void Method() { Console.WriteLine(nameof(ExplicitInterfaceCall) + "Method()"); }

    void I1.Method() { Console.WriteLine(nameof(I1) + "Method()"); }

    void I2.Method() { Console.WriteLine(nameof(I2) + "Method()"); }

    public ExplicitInterfaceCall()
    {
      // call local
      this.Method();

      // call from I1
      ((I1)this).Method();

      // call from I2
      ((I2)this).Method();
    }
  }
}
