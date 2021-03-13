using System;

namespace SW03.DefaultParameterDemo
{
  public class ExampleDrawing : IDrawing
  {
    private int test;

    private bool myVar;

    public bool MyProperty
    {
      get { return myVar; }
      set {
        myVar = value; }
    }


    public ExampleDrawing(bool test)
    {
      this.MyProperty = test;
    }

    public void Draw(bool monaLisa = false)
    {
      Console.WriteLine("Test");
    }

    public void Draw()
    {
      throw new NotImplementedException();
    }
  }
}
