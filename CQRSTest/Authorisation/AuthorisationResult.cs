namespace CQRSTest.Authorisation
{
    public record AuthorisationResult
    {
        public bool IsAuthorised { get; set; }
        public string Message { get; set; }

        public static AuthorisationResult Authorised => new AuthorisationResult { IsAuthorised = true };
        public static AuthorisationResult Unauthorised => new AuthorisationResult();
        public static AuthorisationResult UnAuthorised(string message) => new AuthorisationResult { Message = message };
    }
}
