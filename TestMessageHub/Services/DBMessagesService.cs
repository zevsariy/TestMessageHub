using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMessageHub.Interfaces;
using TestMessageHub.Models;

namespace TestMessageHub.Services
{
    public class DBMessagesService : IDBMessagesService
    {
        public async Task<List<DBMessageEntity>> GetMessages(
            string companyName,
            DateTime? fromDate,
            DateTime? toDate,
            bool? read)
        {
            using ApplicationContext db = new ApplicationContext();
            var messages = await db.Messages.Where(
                (message) => message.To == companyName
                && (message.SendDate >= fromDate || message.SendDate <= toDate)
            ).ToListAsync();
            return messages;
        }

        public async Task SaveMessage(DBMessageEntity message)
        {
            using ApplicationContext db = new ApplicationContext();
            await db.Messages.AddAsync(message);
            db.SaveChanges();
        }
    }
}
