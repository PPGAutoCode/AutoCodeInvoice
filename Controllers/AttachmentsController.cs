
using Microsoft.AspNetCore.Mvc;
using ProjectName.Types;
using ProjectName.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectName.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentsController : ControllerBase
    {
        private readonly ICloudApiClient _cloudApiClient;
        private readonly IInvoiceAttachmentService _invoiceAttachmentService;

        public AttachmentsController(ICloudApiClient cloudApiClient, IInvoiceAttachmentService invoiceAttachmentService)
        {
            _cloudApiClient = cloudApiClient;
            _invoiceAttachmentService = invoiceAttachmentService;
        }

        [HttpPost("invoices/{invoiceId}/attachments/add")]
        public async Task<IActionResult> Add([FromRoute] Guid invoiceId, [FromBody] Attachment attachment)
        {
            return await SafeExecutor.ExecuteAsync(async () =>
            {
                var tenantId = Request.Headers["X-TenantId"].ToString();
                if (string.IsNullOrEmpty(tenantId))
                {
                    return BadRequest("Missing or invalid headers");
                }

                _cloudApiClient.AddHeader("X-TenantId", tenantId);

                var invoiceExists = await _invoiceAttachmentService.InvoiceExistsAsync(invoiceId);
                if (!invoiceExists)
                {
                    return NotFound("The specified invoiceId does not exist");
                }

                var attachmentBelongsToInvoice = await _invoiceAttachmentService.AttachmentBelongsToInvoiceAsync(invoiceId, attachment.FileId);
                if (!attachmentBelongsToInvoice)
                {
                    return BadRequest("Wrong
