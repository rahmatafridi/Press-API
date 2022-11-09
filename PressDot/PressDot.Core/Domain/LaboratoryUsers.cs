using System.Collections.Generic;

namespace PressDot.Core.Domain
{
    public class LaboratoryUsers : BaseEntity
    {
        public int LaboratoryId { get; set; }
        public int UserId { get; set; }
        public bool IsEmailReceiver { get; set; }

        public Laboratory Laboratory { get; set; }
        public Users User { get; set; }
    }
}
