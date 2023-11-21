namespace Send_HTML_TemplateOnEmail.DTOs.ResponseDTOs
{
    public class ApiResponse
    {
        public int Status { get; set; }    // it will be 0 Or 1
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; } 
    }
}
