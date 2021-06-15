using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationCore.Helpers
{
    public class ThreadHelpers
    {
        static Dictionary<Guid, Thread> _setTimeoutHandles =
            new Dictionary<Guid, Thread>();

        //SetTimeout for no UI Thread issue
        public static Guid SetTimeout(Action cb, int delay)
        {
            return SetTimeout(cb, delay, null);
        }
        //Javascript-style SetTimeout function
        //remember to set uiForm argument when there cb is trying
        //to change UI controls in window form
        //it will return a GUID as handle for cancelling
        public static Guid SetTimeout(Action cb, int delay, Form uiForm)
        {
            Guid g = Guid.NewGuid();
            Thread t = new Thread(() =>
            {
                Thread.Sleep(delay);
                _setTimeoutHandles.Remove(g);

                if (uiForm != null) uiForm.Invoke(cb);
                else cb();

            });
            _setTimeoutHandles.Add(g, t);
            t.Start();
            return g;
        }
        //Javascript-style ClearTimeout
        static void ClearTimeout(Guid g)
        {
            if (!_setTimeoutHandles.ContainsKey(g))
                return;
            _setTimeoutHandles[g].Abort();
            _setTimeoutHandles.Remove(g);
        }
    }
}
