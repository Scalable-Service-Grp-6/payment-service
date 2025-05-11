using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Interfaces;
using System.Collections.Generic;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{

    private readonly IPaymentService _paymentService;
    private readonly IAuthService _authService;

    public PaymentController(IPaymentService paymentService, IAuthService authService)
    {
        _paymentService = paymentService;
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAsync([FromBody] PaymentRequest request)
    {
        //validate whether the incoming request is valid
        var authResponse = await _authService.ValidateToken(HttpContext.Request);
        if (authResponse.IsValidToken)
        {
            
            var response = await _paymentService.ProcessPaymentAsync(request);
            return Ok(response);
        }
        else {

            return Unauthorized("User is not authorized to do the transaction.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        var authResponse = await _authService.ValidateToken(HttpContext.Request);
        if (authResponse.IsValidToken)
        {

            var response = await _paymentService.GetPaymentsAsync();
            return Ok(response);
        }
        else
        {

            return Unauthorized("User is not authorized to do the transaction.");
        }
    }
}