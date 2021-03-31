namespace TestMessageHub.Models
{
    /// <summary>
    /// Model of message from Puma company
    /// </summary>
    public class PumaMessage : MessageBase
    {
        /// <summary>
        /// Message title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        public string Body { get; set; }
    }
}
