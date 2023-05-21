using Domain.Enum;
using Polly.Retry;
using RestSharp;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Factories
{
   
    public class RestClientWithPolly : RestClient
    {
        public RestClientWithPolly(string baseUrl) :base(baseUrl)
        {

        }

        private AsyncRetryPolicy AsyncRetryPolicy { get; set; }

        public void SetPollicy(AsyncRetryPolicy configurePolicy)
        {
            AsyncRetryPolicy = configurePolicy;
        }
        
        public override async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return await ExecuteWithPoly(async () => await base.ExecuteTaskAsync<T>(request));
        }

        private Task<T> ExecuteWithPoly<T>(Func<Task<T>> function)
        {
            if (AsyncRetryPolicy is not null)
            {
                return  AsyncRetryPolicy.ExecuteAsync(async () => await function.Invoke());
            }

            return function.Invoke();
        }
    }
    
    public class RestCllientFactory
    {
        public delegate RestClientWithPolly CreateRestClientDelegate(RestClientType clientType);

        static string GetEnumDescription(Enum enumValue)
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo.First().GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Any() ? ((DescriptionAttribute) attributes[0]).Description : null;
        }
        
        public static CreateRestClientDelegate GetClientDeligate(Action<RestClientWithPolly, RestClientType> configureRestClient = null)
        {
            return (type) =>
            {
                var description = GetEnumDescription(type);
                var client = new RestClientWithPolly(description);
                
                configureRestClient?.Invoke(client, type);

                return client;
            };
        }
    }
}
