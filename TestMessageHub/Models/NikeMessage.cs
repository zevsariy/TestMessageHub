namespace TestMessageHub.Models
{
    /// <summary>
    /// Model of message from Nike company
    /// </summary>
    public class NikeMessage : MessageBase
    {
        /// <summary>
        /// Message title
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        public string Message { get; set; }
    }
}
