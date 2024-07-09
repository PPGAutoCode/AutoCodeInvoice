
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
    public class InvoicesController : ControllerBase
    {
        private readonly ICloudApiClient _cloudApiClient;
        private readonly IInvoiceAttachmentService _invoiceAttachmentService;

        public InvoicesController(ICloudApiClient cloudApiClient, IInvoiceAttachmentService invoiceAttachmentService)
        {
            _cloudApiClient = cloudApiClient;
            _invoiceAttachmentService = invoiceAttachmentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Invoice invoice)
        {
            return await SafeExecutor.ExecuteAsync(async () =>
            {
                var tenantId = Request.Headers["X-TenantId"].ToString();
                if (string.IsNullOrEmpty(tenantId))
                {
                    return BadRequest("Missing or invalid headers");
                }

                _cloudApiClient.AddHeader("X-TenantId", tenantId);

                var prcInvoiceDto = invoice.ToPRCInvoiceDto();
                var response = await _cloudApiClient.SendOneAsync(prcInvoiceDto);

                if (!response.IsSuccessStatusCode)
                {
                    return Problem(statusCode: (int)response.StatusCode, title: response.ReasonPhrase);
                }

                var invoiceId = await response.Content.ReadAsAsync<Guid?>();
                if (invoiceId == null)
                {
                    return Problem(statusCode: 500, title: "Unexpected server-side error");
                }

                await _invoiceAttachmentService.AddInvoiceAttachmentsAsync(invoiceId.Value, invoice.FileIds);

                return CreatedAtAction(nameof(Add), new { id = invoiceId }, invoiceId);
            });
        }
    }
}
