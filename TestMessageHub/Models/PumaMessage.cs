namespace TestMessageHub.Models
{
    /// <summary>
    /// Model of message from Puma company
    /// </summary>
    public class PumaMessage : MessageBase
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
