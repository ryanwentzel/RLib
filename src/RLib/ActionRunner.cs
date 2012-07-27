using System;
using System.Diagnostics;

namespace RLib
{
    public static class ActionRunner
    {
        public static void RunSafely(Action action)
        {
            if (action == null) return;

            try
            {
                action();
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.ToString());
            }
        }
    }
}
