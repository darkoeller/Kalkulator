using System;
using System.ComponentModel;

namespace ObracunPlace
{
    public static class Extenzije
    {
        public static void PozoviAkoTreba(this ISynchronizeInvoke obj, Action action)
        {
        if (obj.InvokeRequired)
        obj.BeginInvoke(action, new object[0]);
        else
        action();
        }
    }
}
