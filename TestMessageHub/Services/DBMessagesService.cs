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
            // prepare date from and date to if one or both dates is null
            if (!fromDate.HasValue && !toDate.HasValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-7);
                toDate = DateTime.UtcNow;
            }
            if (!fromDate.HasValue) fromDate = toDate.Value.AddDays(-1);
            if (!toDate.HasValue) toDate = fromDate.Value.AddDays(1);

            using ApplicationContext db = new ApplicationContext();

            if (read.HasValue)
            {
                var query = db.Messages.Where(
                    (message) => message.To == companyName
                    && (message.SendDate >= fromDate && message.SendDate <= toDate)
                    && message.Read == read
                );
                var messages = await query.ToListAsync();
                return messages;
                
            }
            else
            {
                var query = db.Messages.Where(
                    (message) => message.To == companyName
                    && (message.SendDate >= fromDate && message.SendDate <= toDate)
                );
                var messages = await query.ToListAsync();
                return messages;
            }
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
