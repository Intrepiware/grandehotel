using System;
using System.Collections.Generic;

namespace GrandeHotel.Lib.Data.Models
{
    public partial class Version
    {
        public long Id { get; set; }
        public string RepositoryPath { get; set; }
        public string Version1 { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
