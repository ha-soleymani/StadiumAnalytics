using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISensorEventService
    {
        Task AddEventAsync(SensorEvent evt);

        Task<List<SensorEventSummary>> GetSummaryAsync(string? gate, string? type, DateTime? start, DateTime? end);
    }
}
