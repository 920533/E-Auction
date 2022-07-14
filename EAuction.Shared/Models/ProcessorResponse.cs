using EAuction.Shared.Enum;
using System.Collections.Generic;

namespace EAuction.Shared.Models
{
    public class ProcessorResponse<TData>
    {
        public TData Data { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public ResponseCode ResponseCode { get; set; }
    }
}
