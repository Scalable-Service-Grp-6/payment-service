using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Interfaces;
using System.Collections.Generic;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{

    private readonly IPaymentService _paymentService;
    private readonly IConfiguration _configuration;
    public PaymentController(IPaymentService paymentService, IConfiguration configuration)
    {
        _paymentService = paymentService;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAsync([FromBody] PaymentRequest request)
    {
        //validate whether the incoming request is valid


        var appSettings = _configuration.GetConnectionString("MongoDb");

        var response = await _paymentService.ProcessPaymentAsync(request);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetPayments()
    {
        return Ok(new PaymentRequest());
    }
}