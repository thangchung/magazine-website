using System;

namespace Cik.CoreLibs.Bus
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(object message)
        {
            Message = message;
        }

        public object Message { get; set; }
    }
}