namespace Cik.Domain
{
  public class Event : IMessage
  {
    public byte[] Version { get; set; }
  }
}