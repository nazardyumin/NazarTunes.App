using Dapper.Contrib.Extensions;
using System;

namespace NazarTunes.Models.DataTemplates
{
    public class Procurement
    {
        public int ProcurementId { get; set; }
        public DateTime DateOfProcurement { get; set; }
        public string? SupplierName { get; set; }
        public int RecordId { get; set; }
        [Computed]
        public string? RecordInfo { get; set; }
        public int Amount { get; set; }
        public double CostPrice { get; set; }
        [Computed]
        public DateOnly? Date 
        {
            get => new DateOnly(DateOfProcurement.Year, DateOfProcurement.Month, DateOfProcurement.Day); 
        }
    }
}
