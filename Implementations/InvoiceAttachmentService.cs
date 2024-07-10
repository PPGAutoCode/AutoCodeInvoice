
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
            _dbConnection = dbConnection;
        }

        public async Task AddInvoiceAttachmentsAsync(Guid invoiceId, List<int> fileIds)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("InvalidInvoiceId", "InvoiceId cannot be empty.");

            if (fileIds == null || !fileIds.Any())
                throw new BusinessException("InvalidFileIds", "FileIds cannot be null or empty.");

            var invoiceAttachments = fileIds.Select(fileId => new InvoiceAttachment
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                FileId = fileId,
                IsReceived = false
            }).ToList();

            const string sql = @"
                INSERT INTO InvoiceAttachments (Id, InvoiceId, FileId, IsReceived)
                VALUES (@Id, @InvoiceId, @FileId, @IsReceived);
            ";

            try
            {
                foreach (var invoiceAttachment in invoiceAttachments)
                {
                    await _dbConnection.ExecuteAsync(sql, invoiceAttachment);
                }
            }
            catch (Exception ex)
            {
                throw new TechnicalException("DatabaseError", $"Error adding invoice attachments: {ex.Message}");
            }
        }

        public async Task<bool> InvoiceExistsAsync(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("InvalidInvoiceId", "InvoiceId cannot be empty.");

            const string sql = @"
                SELECT COUNT(1) 
                FROM InvoiceAttachments 
                WHERE InvoiceId = @InvoiceId;
            ";

            try
            {
                var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId });
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("DatabaseError", $"Error checking if invoice exists: {ex.Message}");
            }
        }

        public async Task<bool> AttachmentBelongsToInvoiceAsync(Guid invoiceId, int attachmentId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("InvalidInvoiceId", "InvoiceId cannot be empty.");

            const string sql = @"
                SELECT COUNT(1) 
                FROM InvoiceAttachments 
                WHERE InvoiceId = @InvoiceId AND FileId = @FileId;
            ";

            try
            {
                var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId, FileId = attachmentId });
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("DatabaseError", $"Error checking if attachment belongs to invoice: {ex.Message}");
            }
        }

        public async Task MarkAsReceivedAsync(int fileId)
        {
            const string sql = @"
                UPDATE InvoiceAttachments 
                SET IsReceived = true 
                WHERE FileId = @FileId;
            ";

            try
            {
                await _dbConnection.ExecuteAsync(sql, new { FileId = fileId });
            }
            catch (Exception ex)
            {
                throw new TechnicalException("DatabaseError", $"Error marking attachment as received: {ex.Message}");
            }
        }

        public async Task<bool> AllAttachmentsReceivedAsync(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
                throw new BusinessException("InvalidInvoiceId", "InvoiceId cannot be empty.");

            const string sql = @"
                SELECT COUNT(1) 
                FROM InvoiceAttachments 
                WHERE InvoiceId = @InvoiceId AND IsReceived = false;
            ";

            try
            {
                var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { InvoiceId = invoiceId });
                return count == 0;
            }
            catch (Exception ex)
            {
                throw new TechnicalException("DatabaseError", $"Error checking if all attachments are received: {ex.Message}");
            }
        }
    }
}
