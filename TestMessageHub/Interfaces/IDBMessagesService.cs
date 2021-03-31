using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMessageHub.Models;

namespace TestMessageHub.Interfaces
{
    /// <summary>
    /// Interface for DB message service
    /// </summary>
    public interface IDBMessagesService
    {
        /// <summary>
        /// Get messages by params
        /// </summary>
        /// <param name="companyName">Messages owner company name.</param>
        /// <param name="fromDate">Start date of messages range.</param>
        /// <param name="toDate">End date of messages range.</param>
        /// <param name="read">Status of messages.</param>
        /// <returns>List of DBMessageEntity</returns>
        Task<List<DBMessageEntity>> GetMessages(
            string companyName,
            DateTime? fromDate,
            DateTime? toDate,
            bool? read);

        /// <summary>
        /// Save message to DB
        /// </summary>
        /// <param name="message">Message to save</param>
        Task SaveMessage(DBMessageEntity message);
    }
}
