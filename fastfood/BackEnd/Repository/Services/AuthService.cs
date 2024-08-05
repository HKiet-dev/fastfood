using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
#pragma warning disable 1591
namespace BackEnd.Repository.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public AuthService(ApplicationDbContext context, 
                           UserManager<User> userManager, 
                           RoleManager<IdentityRole> roleManager, 
                           IJwtTokenGenerator jwtTokenGenrator, 
                           IMapper mapper,SignInManager<User> signInManager,
                           IHelper helper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenrator;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<User> CreateUserFromGoogleLogin(User user)
        {
            var eUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (eUser != null)
            {
                await _signInManager.SignInAsync(eUser,false);
                return eUser;
            }

            user = new User
            {

                Avatar = user.Avatar,
                UserName = user.Email,
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
            };

            string role = "CUSTOMER";
            var result = await _userManager.CreateAsync(user);
            if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }

            await _userManager.AddToRoleAsync(user, role);
            await _signInManager.SignInAsync(user,false);
            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }

		public async Task<string> ForgotPassword(string email)
		{
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            
            var newPass = GenerateRandomString(20);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPass);
            if (!result.Succeeded)
            {
                return "Đổi mật khẩu thất bạu"; // Thay đổi mật khẩu không thành công
            }

            var sendMail = await SendNewPasswordToMail(email,"FastFood Service", newPass);

            return sendMail;
		}

		public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> GetUserId(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public async Task<IList<string>> GetUserRole(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDTO)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDTO.UserName);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = null, Role = null };
            }
            // first params : người dùng có tên đăng nhập là loginRequestDTO.UserName (object)
            // second params : mật khẩu mà người dùng nhập  (string)
            // third params : ghi nhớ trạng thái đăng nhập (bool)
            // fourth params : mặc định nếu đăng nhập sai 5 lần liên tiếp sẽ bị khoá đăng nhập trong 5 phút
            var result = await _signInManager.PasswordSignInAsync(user, loginRequestDTO.Password, false, false);

            if (!result.Succeeded)
            {
                return new LoginResponseDto() { User = null, Token = null, Role = null };
            }

            var roles = await GetUserRole(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var userDto = _mapper.Map<UserDto>(user);

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token,
                Role = roles
            };

            return loginResponseDto;
        }

        public async Task<string> Resgister(RegistrationRequestDto registrationRequestDTO)
        {
            User user = new User()
            {
                UserName = registrationRequestDTO.Name,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
                Address = registrationRequestDTO.Address,
                Avatar = registrationRequestDTO.Avatar,
                Gender = (int)registrationRequestDTO.Gender,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await _context.Users.FirstOrDefaultAsync();

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        Address = userToReturn.Address,
                        Avatar = userToReturn.Avatar,
                        Gender = userToReturn.Gender,
                    };

                    string role = "CUSTOMER";

                    if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    return "Tạo tài khoản thành công";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> SendNewPasswordToMail(string to, string subject, string content)
        {
            string from = "chienprivate@gmail.com";
            string pass = "aohm alki wbdn zgeu";
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(to);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = $@"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Mật khẩu mới</title>
    <style>
        .container {{
            width: 100%;
            max-width: 600px;
            margin: auto;
            padding: 20px;
            background-color: #1d0404;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(54, 134, 148, 0.1);
        }}
        .text-center{{
            text-align: center;
        }}
        .text-white{{
            color: white;
        }}
        .bg-white{{
            background-color: white;
        }}
        .fs-1{{
            font-size: large;
        }}
    </style>
</head>
<body>
    <div class=""container bg-white"">
        <h2>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi !</h2>
        <span>Đây là mật khẩu mới của bạn, vui lòng không chia sẽ thông tin này</span>
        <br>
        <span>Hãy đổi mật khẩu trong lần đăng nhập tiếp theo, xin cảm ơn. </span>
    </div>
    <div class=""container"">
        <div class=""text-center"">
            <h1 class=""text-white"">Mật khẩu mới của bạn</h1>
        </div>
        <div class=""text-center bg-white fs-1"">
            <h2>{content}</h2>
        </div>
    </div>
    
</body>

</html>";
            ;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(from, pass);
            try
            {
                client.Send(msg);
                return "gửi mail thành công";
            }
            catch (Exception ex)
            {
                return "Gửi mail thất bại";
            }
        }

        public string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>?";
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                sb.Append(characters[index]);
            }

            return sb.ToString();
        }
    }

    
}
