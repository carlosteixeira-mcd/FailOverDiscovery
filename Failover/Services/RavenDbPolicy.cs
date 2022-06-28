using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;
using Raven.Client.Exceptions;

namespace Failover.Services
{
    public class RavenDbPolicy
    {
        public RetryPolicy RetryPolicy;
        public readonly FallbackPolicy FallbackPolicy;
        public PolicyWrap PoliciesWrap;

        public RavenDbPolicy()
        {
            this.RetryPolicy = Policy.Handle<RavenException>(exception => true)
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                (result, span, retryCount, ctx) => Console.WriteLine($"Retrying({retryCount})..."));

            this.FallbackPolicy = Policy.Handle<Exception>(exception => true).Fallback(FallbackAction, OnFallBack);

            PoliciesWrap = Policy.Wrap(FallbackPolicy, RetryPolicy);
        }
        private void OnFallBack(Exception obj)
        {
            Console.WriteLine("Starting fallback policy...");
        }

        private void FallbackAction()
        {
            Console.WriteLine("It went bad huh!? Let's try another way.");
            Console.WriteLine("Here, will be called SLQLite to store the events.");
        }
    }
}
