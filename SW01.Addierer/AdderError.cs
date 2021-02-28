namespace SW01.Adder
{
  internal enum AdderError:int
  {
    /// <summary>
    /// Error OK => No error happened
    /// </summary>
    OK = 0,

    /// <summary>
    /// No Integer Number could be parsed from Arguments.
    /// </summary>
    NoNumberParsedFromArgs = -11,
  }
}