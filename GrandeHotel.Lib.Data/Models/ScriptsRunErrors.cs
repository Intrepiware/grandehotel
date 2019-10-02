using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class ScriptsRunErrors
    {
        public long Id { get; set; }
        public string RepositoryPath { get; set; }
        public string Version { get; set; }
        public string ScriptName { get; set; }
        public string TextOfScript { get; set; }
        public string ErroneousPartOfScript { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
