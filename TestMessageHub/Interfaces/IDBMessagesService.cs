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
        /// <param name="dateRange">DateTime range.</param>
        /// <param name="read">Status of messages.</param>
        /// <returns>List of DBMessageEntity</returns>
        Task<List<DBMessageEntity>> GetMessages(
            string companyName,
            DateRange dateRange,
            bool? read);

        /// <summary>
        /// Save message to DB
        /// </summary>
        /// <param name="message">Message to save</param>
        Task SaveMessage(DBMessageEntity message);
    }
}
