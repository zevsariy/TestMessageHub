namespace TestMessageHub.Models.DTO
{
    /// <summary>
    /// Model of message from Adidas company
    /// </summary>
    public class AdidasMessageDTO: MessageBaseDTO
    {
        /// <summary>
        /// Title of message
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        public string Content { get; set; }
    }
}
