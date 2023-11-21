using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Send_HTML_TemplateOnEmail.Features.Command;

namespace Send_HTML_TemplateOnEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailTemplateController : BaseApiController
    {
        [HttpPost("Send-HTML-Template-On-Email")]
        public async Task<IActionResult> SendAbsentReportEmail([FromBody] SendTemplateOnEmailCommand sendEmailCommand)
        {
            if (!string.IsNullOrWhiteSpace(sendEmailCommand.RecipientEmail))
            {
                // Valid recipient email is provided, proceed with sending the email.
                var response = await Mediator.Send(sendEmailCommand);
                return Ok(response);
            }
            else
            {
                return BadRequest("RecipientEmail is not specified.");
            }
        }
    }
}
