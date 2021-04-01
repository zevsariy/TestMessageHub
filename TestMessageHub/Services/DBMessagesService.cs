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
        /// <param name="dateRange">Date range</param>
        /// <param name="read">Read status of message</param>
        /// <returns>List of messages</returns>
        public async Task<List<DBMessageEntity>> GetMessages(
            string companyName,
            DateRange dateRange,
            bool? read)
        {
            // prepare date from and date to if one or both dates is null
            if (!dateRange.From.HasValue && !dateRange.To.HasValue)
            {
                dateRange.From = DateTime.UtcNow.AddDays(-7);
                dateRange.To = DateTime.UtcNow;
            }
            if (!dateRange.From.HasValue) dateRange.From = dateRange.To.Value.AddDays(-1);
            if (!dateRange.To.HasValue) dateRange.To = dateRange.From.Value.AddDays(1);

            using ApplicationContext db = new ApplicationContext();

            if (read.HasValue)
            {
                var query = db.Messages.Where(
                    (message) => message.To == companyName
                    && message.SendDate >= dateRange.From 
                    && message.SendDate <= dateRange.To
                    && message.Read == read
                );
                var messages = await query.ToListAsync();
                return messages;
            }
            else
            {
                var query = db.Messages.Where(
                    (message) => message.To == companyName
                    && message.SendDate >= dateRange.From
                    && message.SendDate <= dateRange.To
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
