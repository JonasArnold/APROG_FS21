using System;

namespace SW10.Events
{
  public delegate void DataChangedDelegate(string str);

  /// <summary>
  /// Model contains Data (e.g. measurement data, sensor data, ...).
  /// </summary>
  public class Model
  {
    /// <summary>
    /// Is invoked whenever the data was updated.
    /// </summary>
    public event DataChangedDelegate DataChanged;

    /// <summary>
    /// Changes the data.
    /// </summary>
    public void UpdateData() 
    { 
      // call event
      DataChanged?.Invoke("Model updated"); 
    }
  }

  /// <summary>
  /// Views can display data.
  /// There are many ways to display the data.
  /// </summary>
  public class View
  {
    /// <summary>
    /// Creates a new view, with model <paramref name="m"/>.
    /// </summary>
    /// <param name="m">data model</param>
    public View(Model m) 
    { 
      m.DataChanged += OnDataUpdated; 
    }

    /// <summary>
    /// Update View when data was updated.
    /// </summary>
    /// <param name="data">the new data</param>
    public void OnDataUpdated(string data)
    {
      // redraw UI
      Console.WriteLine("Message received: " + data);
    }
  }

  public class Program
  {
    static void Main()
    {
      // create one data container
      Model m = new();

      // create multiple views for the same data.
      View v1 = new(m);
      View v2 = new(m);

      // update data
      m.UpdateData();
    }
  }
}
