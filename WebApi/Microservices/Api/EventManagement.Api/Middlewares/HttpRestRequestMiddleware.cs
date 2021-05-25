using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Api.Middlewares
{
    /**
   * HttpRestRequestMiddleware class 
   * 
   * @author Hari
   * @license MIT
   * @version 1.0
   */
    public class HttpRestRequestMiddleware
    {
        #region Private Members

        private readonly RequestDelegate next;
        private readonly ILogger<HttpRestRequestMiddleware> logger;
        private readonly RecyclableMemoryStreamManager recyclableMemory;

        #endregion

        #region Constructor

        public HttpRestRequestMiddleware(RequestDelegate next, ILogger<HttpRestRequestMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            recyclableMemory = new RecyclableMemoryStreamManager();
        }

        #endregion
        
        public async Task Invoke(HttpContext context)
        {
            await Request(context);
            await Response(context);
        }

        private async Task Response(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = recyclableMemory.GetStream();
            context.Response.Body = responseBody;
            await next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task Request(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = recyclableMemory.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
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
      
    }
}
