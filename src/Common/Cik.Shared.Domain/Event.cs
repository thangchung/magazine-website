namespace Cik.Shared.Domain
{
    public class Event : IMessage
    {
        public byte[] Version { get; set; }
    }
}