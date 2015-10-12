using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sort2015.Web.Extensions
{
    public class CustomActionResult<T> : IHttpActionResult
    {
        private readonly T _content;
        private readonly HttpStatusCode _statusCode;

        public CustomActionResult(HttpStatusCode statusCode, T content)
        {
            _statusCode = statusCode;
            _content = content;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(CustomResponse(_content, _statusCode));
        }

        public HttpResponseMessage CustomResponse(T content, HttpStatusCode statusCode)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            HttpResponseMessage response = request.CreateResponse<T>(statusCode, content);
            return response;
        }
    }
}
