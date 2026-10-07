using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Documents.Responses
{
    public sealed class DocumentResponse
{
        public int Id { get; init; }

        public int DocumentTypeId { get; init; }

        public required string OriginalFileName { get; init; }

        public string? MimeType { get; init; }

        public long? FileSize { get; init; }

        public int UploadedByUserId { get; init; }

        public DateTime UploadedAt { get; init; }

        public bool IsActive { get; init; }

        public string? Notes { get; init; }
    }
}
