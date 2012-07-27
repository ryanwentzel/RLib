using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Extensions
{
    public static class EventExtensions
    {
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler == null) return;
			
			handler(sender, e);
        }
    }
}
