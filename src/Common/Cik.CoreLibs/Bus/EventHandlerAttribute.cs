using System;

namespace Cik.CoreLibs.Bus
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class EventHandlerAttribute : Attribute
    {
    }
}