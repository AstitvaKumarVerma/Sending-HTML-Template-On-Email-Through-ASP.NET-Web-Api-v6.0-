using MediatR;
using Send_HTML_TemplateOnEmail.Common;
using Send_HTML_TemplateOnEmail.DTOs;
using Send_HTML_TemplateOnEmail.DTOs.ResponseDTOs;
using Send_HTML_TemplateOnEmail.Interfaces;

namespace Send_HTML_TemplateOnEmail.Features.Command
{
    public class SendTemplateOnEmailCommand : TestDTO,IRequest<ApiResponse>
    {
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public class SendTemplateOnEmailCommandHandler : IRequestHandler<SendTemplateOnEmailCommand, ApiResponse>
        {
            private readonly IEmailService _email;
            private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

            public SendTemplateOnEmailCommandHandler(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IEmailService emailService)
            {
                _email = emailService;
                _env = env;
            }
            public async Task<ApiResponse> Handle(SendTemplateOnEmailCommand request, CancellationToken cancellationToken)
            {
                ApiResponse apiResponse = new ApiResponse();

                // Implement email sending logic here
                var emailHtml = System.IO.File.ReadAllText(_env.WebRootPath + "/Template.html");
                emailHtml = emailHtml.Replace("{{recipient_name}}", request.RecipientName);
                

                var response = _email.SendTemplateToEmails(request.RecipientEmail, $"Testing Email Report of {request.DeveloperName}", emailHtml);

                if (response == "Successful")
                {
                    apiResponse.Status = 1;
                    apiResponse.StatusCode = ResponseCode.Ok;
                    apiResponse.Message = ResponseMessage.SuccessfulSendMessage;

                    return apiResponse;
                }
                else
                {
                    apiResponse.Status = 0;
                    apiResponse.StatusCode = ResponseCode.ServerError;
                    apiResponse.Message = response;

                    return apiResponse;
                }
            }
        }
    }
}
