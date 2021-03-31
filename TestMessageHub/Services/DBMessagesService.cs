using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMessageHub.Interfaces;
using TestMessageHub.Models;

namespace TestMessageHub.Services
{
    /// <summary>
    /// Service for CRUD operations on messages in DB
    /// </summary>
    public class DBMessagesService : IDBMessagesService
    {
        /// <summary>
        /// Get messages by params
        /// </summary>
        /// <param name="companyName">Message receiver name</param>
        /// <param name="fromDate">Start dateTime range</param>
        /// <param name="toDate">End dateTime range</param>
        /// <param name="read">Read status of message</param>
        /// <returns>List of messages</returns>
        public async Task<List<DBMessageEntity>> GetMessages(
            string companyName,
            DateTime? fromDate,
            DateTime? toDate,
            bool? read)
        {
            if (fromDate == null) fromDate = DateTime.UtcNow.AddDays(-7);
            if (toDate == null) toDate = DateTime.UtcNow;

            using ApplicationContext db = new ApplicationContext();
            var query = db.Messages.Where(
                (message) => message.To == companyName
                && (message.SendDate >= fromDate || message.SendDate <= toDate)
            );

            if (read != null)
                query = query.Where((message) => message.Read == read);

            var messages = await query.ToListAsync();
            return messages;
        }

        /// <summary>
        /// Save message to DB
        /// </summary>
        /// <param name="message">Message</param>
        public async Task SaveMessage(DBMessageEntity message)
        {
            using ApplicationContext db = new ApplicationContext();
            await db.Messages.AddAsync(message);
            db.SaveChanges();
        }
    }
}
