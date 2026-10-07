using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Documents.Requests
{
    public sealed class CreateDocumentRequest
    {
        public int DocumentTypeId { get; init; }
        public string? Notes { get; init; }
    }
}
