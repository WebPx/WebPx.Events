using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPx.Events
{
    public sealed class StateChangingEventArgs : EventArgs
    {
        public StateChangingEventArgs(string currentState, string targetState)
        {
            this.CurrentState = currentState;
            this.TargetState = targetState;
        }

        public string CurrentState { get; private set; }

        public string TargetState { get; private set; }
    }
}
