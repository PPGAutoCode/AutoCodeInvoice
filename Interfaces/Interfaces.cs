
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectName.Types;

namespace ProjectName.Interfaces
{{
    /// <summary>
    /// Interface for interacting with a cloud API client.
    /// </summary>
    public interface ICloudApiClient
    {{
        /// <summary>
        /// Adds a header to the API client.
        /// </summary>
        /// <param name="headerName">The name of the header.</param>
        /// <param name="headerValue">The value of the header.</param>
        void AddHeader(string headerName, string headerValue);

        /// <summary>
        /// Sends a PRCInvoiceDto asynchronously.
        /// </summary>
        /// <param name="invoice">The PRCInvoiceDto to send.</param>
        /// <returns>A Task representing the asynchronous operation containing the HttpResponseMessage.</returns>
        Task<HttpResponseMessage> SendOneAsync(PRCInvoiceDto invoice);

        /// <summary>
        /// Sends a PRCAttachmentDto asynchronously.
        /// </summary>
        /// <param name="attachment">The PRCAttachmentDto to send.</param>
        /// <returns>A Task representing the asynchronous operation containing the HttpResponseMessage.</returns>
        Task<HttpResponseMessage> SendOneAsync(PRCAttachmentDto attachment);

        /// <summary>
        /// Activates an invoice asynchronously.
        /// </summary>
        /// <param name="prcInvoiceId">The ID of the invoice to activate.</param>
        /// <returns>A Task representing the asynchronous operation containing the HttpResponseMessage.</returns>
        Task<HttpResponseMessage> ActivateInvoice(Guid prcInvoiceId);
    }}

    /// <summary>
    /// Interface for managing invoice attachments.
    /// </summary>
    public interface IInvoiceAttachmentService
    {{
        /// <summary>
        /// Adds invoice attachments asynchronously.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to which attachments are added.</param>
        /// <param name="fileIds">The list of file IDs to be added as attachments.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task AddInvoiceAttachmentsAsync(Guid invoiceId, List<int> fileIds);

        /// <summary>
        /// Checks if an invoice exists asynchronously.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to check.</param>
        /// <returns>A Task representing the asynchronous operation containing a boolean indicating if the invoice exists.</returns>
        Task<bool> InvoiceExistsAsync(Guid invoiceId);

        /// <summary>
        /// Checks if an attachment belongs to an invoice asynchronously.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <param name="attachmentId">The ID of the attachment.</param>
        /// <returns>A Task representing the asynchronous operation containing a boolean indicating if the attachment belongs to the invoice.</returns>
        Task<bool> AttachmentBelongsToInvoiceAsync(Guid invoiceId, int attachmentId);

        /// <summary>
        /// Marks an attachment as received asynchronously.
        /// </summary>
        /// <param name="fileId">The ID of the file to mark as received.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task MarkAsReceivedAsync(int fileId);

        /// <summary>
        /// Checks if all attachments for an invoice have been received asynchronously.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <returns>A Task representing the asynchronous operation containing the HttpResponseMessage.</returns>
        Task<HttpResponseMessage> AllAttachmentsReceivedAsync(Guid invoiceId);
    }}
}}
