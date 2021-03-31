using System;

namespace TestMessageHub.Models
{
    public class DBMessageEntity
    {
        /// <summary>
        /// Message identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company sender name
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Company receiver name
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

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
