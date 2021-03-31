namespace TestMessageHub.Models
{
    /// <summary>
    /// Model of message from Nike company
    /// </summary>
    public class NikeMessage : MessageBase
    {
        public string Caption { get; set; }
        public string Message { get; set; }
    }
}
