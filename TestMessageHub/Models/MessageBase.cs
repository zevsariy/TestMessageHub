using System;

namespace TestMessageHub.Models
{
    /// <summary>
    /// Message base class
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// Company sender name
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Company receiver name
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Message send dateTime
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// Message read status
        /// </summary>
        public bool Read { get; set; }
    }
}
