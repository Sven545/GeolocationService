using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.IO;
using System;

namespace GeolocationService.PresentationLayer.Middlewares
{
    public class RequestResponseConsoleLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseConsoleLoggingMiddleware(RequestDelegate next,
                                            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseConsoleLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }


        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} - Http Request Information:{Environment.NewLine} " +
                                   $"   Method:{context.Request.Method}{Environment.NewLine} " +
                                   $"   Schema:{context.Request.Scheme}{Environment.NewLine} " +
                                   $"   Host: {context.Request.Host}{Environment.NewLine} " +
                                   $"   Path: {context.Request.Path}{Environment.NewLine} " +
                                   $"   Request Body: {ReadStreamInChunks(requestStream)}{Environment.NewLine}");
            context.Request.Body.Position = 0;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} - Http Request Information:{Environment.NewLine} " +
                                  $"   Schema:{context.Request.Scheme}{Environment.NewLine} " +
                                  $"   Host: {context.Request.Host}{Environment.NewLine} " +
                                  $"   Path: {context.Request.Path}{Environment.NewLine} " +
                                  $"   Status Code: {context.Response.StatusCode}{Environment.NewLine} " +
                                  $"   Response Body: {text}{Environment.NewLine}");
           
            await responseBody.CopyToAsync(originalBodyStream);
        }

    }
}
