namespace Pruefungsvorbereiten
{
  internal static class Inheritance
  {
    internal class Shape { }
    internal class Circle : Shape { }
    internal class Rectangle : Shape { }

    public static void Test()
    {
      Shape s = new Shape();
      Circle c = new Circle();
      Rectangle r = new Rectangle();

      // Polymorphism
      Shape s0 = new Shape();
      Shape s1 = new Circle();
      Shape s2 = new Rectangle();      

      s = r;  // works
      s = c;  // works
      //r = c;  // does not work
      //c = r;  // does not work
      //r = s;  // does not work (can be casted explicitly)
      //c = s;  // does not work (can be casted explicitly)
    }
  }
}