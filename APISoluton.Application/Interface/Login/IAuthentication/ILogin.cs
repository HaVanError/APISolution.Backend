using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.LoginView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Login.IAuthentication
{
    public interface ILogin
    {
        TokenResponse Authentication(LoginVM model);
        TokenResponse RefreshTokens(string token);
        bool Logout(string refreshToken);
    }
}
