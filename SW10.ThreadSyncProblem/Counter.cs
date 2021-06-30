namespace SW10.ThreadSyncProblem
{
  public class Counter
  {
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    private int cnt;
    private object syncRoot = new object();

    public int GetNextNumber()
    {
      lock (syncRoot)
      {
        cnt++;
      log.Info("New Counter value: " + cnt);
      return cnt;
      }

      //Monitor.Enter(syncRoot);
      //try
      //{
      //    cnt++;
      //    log.Info("New Counter value: " + cnt);
      //    return cnt;
      //}
      //finally
      //{
      //    Monitor.Exit(syncRoot);
      //}

    }
  }
}