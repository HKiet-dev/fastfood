using Azure;
using BackEnd.Models.Dtos;
using BackEnd.Models.MOMO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
#pragma warning disable 1591
namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        readonly HttpClient _client;
        public MomoController()
        {
            _client = new HttpClient();
        }
        [HttpPost]
        public async Task<ResponseDto> MomoPayment(string amount)
        {
            MomoRequest momoRequest = new MomoRequest();
            momoRequest.partnerCode = "MOMO5RGX20191128";
            momoRequest.partnerName = "Test Momo API Payment";
            momoRequest.storeId = "Momo Test Store";
            string accessKey = "M8brj9K6E22vXoDB";
            string serectkey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            momoRequest.orderInfo = "Payment with momo";
            momoRequest.redirectUrl = "https://localhost:7192/momo-return";
            momoRequest.ipnUrl = "https://localhost:7192";
            momoRequest.requestType = "captureWallet";

            momoRequest.amount = amount;
            momoRequest.orderId = Guid.NewGuid().ToString();
            momoRequest.requestId = Guid.NewGuid().ToString();
            momoRequest.extraData = "";
            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + momoRequest.amount +
                "&extraData=" + momoRequest.extraData +
                "&ipnUrl=" + momoRequest.ipnUrl +
                "&orderId=" + momoRequest.orderId +
                "&orderInfo=" + momoRequest.orderInfo +
                "&partnerCode=" + momoRequest.partnerCode +
                "&redirectUrl=" + momoRequest.redirectUrl +
                "&requestId=" + momoRequest.requestId +
                "&requestType=" + momoRequest.requestType
                ;
            MomoSecurity crypto = new MomoSecurity();
            //sign signature SHA256
            momoRequest.signature = crypto.signSHA256(rawHash, serectkey);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://test-payment.momo.vn/v2/gateway/api/create");
            request.Content = new StringContent(JsonConvert.SerializeObject(momoRequest), System.Text.Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var MomoResponse = JsonConvert.DeserializeObject<MomoResponse>(data);
                return new ResponseDto { IsSuccess = true, Message = MomoResponse.payUrl };
            }
            return new ResponseDto { IsSuccess = false, Message = "Bad Request" };
        }
    }
}
