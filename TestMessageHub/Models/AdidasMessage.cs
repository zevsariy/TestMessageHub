namespace TestMessageHub.Models
{
    /// <summary>
    /// Model of message from Adidas company
    /// </summary>
    public class AdidasMessage: MessageBase
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
