using System.Net;
using System.Timers;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7Test.Controllers;
[ApiController]
[Route("[controller]")]
public class EndpointController : ControllerBase
{
    private bool endpointCalled = false;
    private HttpListener listener;

    [HttpGet]
    public IActionResult Endpoint1()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:7195/");
        listener.Start();

        // Create a temporary POST endpoint
        listener.BeginGetContext(new AsyncCallback(Endpoint2), listener);

        // Close the endpoint after 20 seconds if it has not been called
        var timer = new System.Timers.Timer(20000);
        timer.Elapsed += CloseEndpoint;
        timer.Start();

        return Ok(new { message = "Endpoint2 created" });
    }

    private void Endpoint2(IAsyncResult result)
    {
        var context = listener.EndGetContext(result);
        var request = context.Request;
        var response = context.Response;

        string responseString = "<HTML><BODY> Hello from Endpoint2!</BODY></HTML>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();

        endpointCalled = true;
    }

    private void CloseEndpoint(object sender, ElapsedEventArgs e)
    {
        if (!endpointCalled)
        {
            listener.Stop();
        }
    }
}