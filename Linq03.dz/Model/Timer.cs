
namespace Linq03.dz.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Timer")]
    public partial class Timer
    {
        public int TimerId { get; set; }

        public int? UserId { get; set; }

        public int? AreaId { get; set; }

        public int? DocumentId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateFinish { get; set; }

        public int? DurationInSeconds { get; set; }
    }
}
