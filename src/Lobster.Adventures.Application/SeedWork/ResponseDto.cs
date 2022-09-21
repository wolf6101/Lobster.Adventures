namespace Lobster.Adventures.Application.SeedWork
{
    public class ResponseDto
    {
        public ResponseDto() : this(false, null)
        {
        }
        public ResponseDto(bool errorOccured, Exception? error)
        {
            this.ErrorOccured = errorOccured;
            this.Error = error;
        }
        public bool ErrorOccured { get; }
        public Exception? Error { get; }
        public string? Message { get; set; }
    }
}