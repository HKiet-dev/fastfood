﻿using Azure;
using BackEnd.Models.Dtos;
using BackEnd.Models.MOMO;
using BackEnd.Models.VNPAY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
#pragma warning disable 1591
namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        readonly HttpClient _client;
        public PaymentController()
        {
            _client = new HttpClient();
        }
        [HttpPost("momo")]
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

        [HttpPost("vnpay")]
        public async Task<ResponseDto> VnPayment(int amount)
        {
            //Get Config Info
            string vnp_Returnurl = "https://localhost:7192/vnpay-return"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "2DQGKKDA"; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = "UMTCVNGYSCWSWVEJCADPUEECDUCVHVNU"; //Secret Key

            //Get payment input
            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = amount; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            order.CreatedDate = DateTime.Now;
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", ":1");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return new ResponseDto { IsSuccess = true, Message = paymentUrl };
        }
    }
}