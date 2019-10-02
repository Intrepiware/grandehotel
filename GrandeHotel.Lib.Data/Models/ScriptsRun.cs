using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class ScriptsRun
    {
        public long Id { get; set; }
        public long? VersionId { get; set; }
        public string ScriptName { get; set; }
        public string TextOfScript { get; set; }
        public string TextHash { get; set; }
        public bool? OneTimeScript { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
