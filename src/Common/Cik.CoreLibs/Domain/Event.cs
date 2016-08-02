namespace Cik.CoreLibs.Domain
{
    public class Event : IMessage
    {
        public byte[] Version { get; set; }
    }
}