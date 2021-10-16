namespace WebApi.Models
{
    public abstract class AuthenticationResult
    {
        public abstract bool Succeeded { get; }

        public static AuthenticationResult Success(string token) => new SuccessAuthenticationResult(token);
        public static AuthenticationResult Failure(string reason) => new FailureAuthenticationResult(reason);

        public class SuccessAuthenticationResult : AuthenticationResult
        {
            public SuccessAuthenticationResult(string accessToken)
            {
                AccessToken = accessToken;
            }

            public override bool Succeeded => true;
            public string AccessToken { get; }
        }

        public class FailureAuthenticationResult : AuthenticationResult
        {
            public FailureAuthenticationResult(string reason)
            {
                Reason = reason;
            }
            public override bool Succeeded => false;
            public string Reason { get; }
        }
    }
}
