
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProjectName.ControllersExceptions;
using ProjectName.Interfaces;
using ProjectName.Types;

namespace ProjectName.Services
{
    public class InvoiceAttachmentService : IInvoiceAttachmentService
    {
        private readonly IDbConnection _dbConnection;

        public InvoiceAttachmentService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task AddInvoiceAttachmentsAsync(Guid invoiceId, List<int> fileIds)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "InvoiceId is required.");

            if (fileIds == null || !fileIds.Any())
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "FileIds are required.");

            foreach (var fileId in fileIds)
            {
                var invoiceAttachment = new InvoiceAttachment
                {
                    Id = Guid.NewGuid(),
                    InvoiceId = invoiceId,
                    FileId = fileId,
                    IsReceived = false
                };

                const string sql = "INSERT INTO InvoiceAttachments (Id, InvoiceId, FileId, IsReceived) VALUES (@Id, @InvoiceId, @FileId, @IsReceived)";
                await _dbConnection.ExecuteAsync(sql, invoiceAttachment);
            }
        }

        public async Task<bool> InvoiceExistsAsync(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "InvoiceId is required.");

            const string sql = "SELECT COUNT(1) FROM InvoiceAttachments WHERE InvoiceId = @InvoiceId";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId });
            return count > 0;
        }

        public async Task<bool> AttachmentBelongsToInvoiceAsync(Guid invoiceId, int attachmentId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "InvoiceId is required.");

            const string sql = "SELECT COUNT(1) FROM InvoiceAttachments WHERE InvoiceId = @InvoiceId AND FileId = @FileId";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId, FileId = attachmentId });
            return count > 0;
        }

        public async Task MarkAsReceivedAsync(int fileId)
        {
            if (fileId <= 0)
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "FileId is required.");

            const string sql = "UPDATE InvoiceAttachments SET IsReceived = 1 WHERE FileId = @FileId";
            await _dbConnection.ExecuteAsync(sql, new { FileId = fileId });
        }

        public async Task<bool> AllAttachmentsReceivedAsync(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "One or more validation errors occurred.", "InvoiceId is required.");

            const string sql = "SELECT COUNT(1) FROM InvoiceAttachments WHERE InvoiceId = @InvoiceId AND IsReceived = 0";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId });
            return count == 0;
        }
    }
}
