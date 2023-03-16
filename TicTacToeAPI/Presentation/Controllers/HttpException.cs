namespace TicTacToeAPI.Presentation.Controllers
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }
        public string? Details { get; set; }
    }
}
