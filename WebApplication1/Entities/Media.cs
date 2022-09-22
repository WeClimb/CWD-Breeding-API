using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class Media : BaseEntity
    {
        public Guid DeerId { get; set; }
        public string BlobId { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual Deer Deer { get; set; } = null!;
    }
}
