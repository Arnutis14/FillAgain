//This took me all day, but I eventually figured out how to get rid of chunking in .Net Core 2.2

//The trick is to read the Response Body into your own MemoryStream so you can get the length. Once you do that, you can set the content-length header, and IIS won't chunk it. I assume this would work for Azure too, but I haven't tested it.

//Here's the middleware:

using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

public class DeChunkerMiddleware
{
    private readonly RequestDelegate _next;

    public DeChunkerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            long length = 0;
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.ContentLength = length;
                return Task.CompletedTask;
            });
            await _next(context);
            //if you want to read the body, uncomment these lines.
            //context.Response.Body.Seek(0, SeekOrigin.Begin);
            //var body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            length = context.Response.Body.Length;
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}


