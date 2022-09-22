using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class MediaModel : BaseModel
    {
        public Guid DeerId { get; set; }
        public string BlobId { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual DeerModel Deer { get; set; } = null!;
    }
}
