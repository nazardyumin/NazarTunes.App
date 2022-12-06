using Dapper.Contrib.Extensions;
using NazarTunes.ViewModels.LanguagePacks;
using System;

namespace NazarTunes.Models.DataTemplates
{
    public class Promotion
    {
        public int DiscountPromotionId { get; set; }
        public string? GenreName { get; set; }
        public int? RecordId { get; set; }
        [Computed]
        public string? RecordInfo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BandName { get; set; }
        public int? Discount { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Computed]
        public string? PromotionSubject 
        { 
            get
            {
                var (promoByGenre, promoByBand, promoByPerformer, promoByRecord) = LanguagePack.GetPromotionSubjects();
                if (GenreName is not null) return $"{promoByGenre}{GenreName}";
                else if (FirstName is not null || LastName is not null) return $"{promoByPerformer}{FirstName} {LastName}";
                else if (RecordInfo is not null) return $"{promoByRecord}{RecordInfo}";
                else if (BandName is not null) return $"{promoByBand}{BandName}";
                else return string.Empty;
            }
        }

        public DateOnly? DateStart
        {
            get
            {
                var date = new DateTime();
                return StartDate == date ? null : new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            }
        }

        public DateOnly? DateEnd
        {
            get
            {
                var date = new DateTime();
                return EndDate == date ? null : new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            }
        }

        public override string ToString()
        {
            if (IsStarted)
            {
                return $"(ID {DiscountPromotionId}) {PromotionSubject}  -{Discount}%  ({DateStart})";
            }
            else
            {
                return $"(ID {DiscountPromotionId}) {PromotionSubject}  -{Discount}%";
            }
        }
    }
}
