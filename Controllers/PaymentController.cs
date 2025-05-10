using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Interfaces;
using System.Collections.Generic;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{

    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAsync([FromBody] PaymentRequest request)
    {

        var response = await _paymentService.ProcessPaymentAsync(request);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetPayments()
    {
        return Ok(new PaymentRequest());
    }
}