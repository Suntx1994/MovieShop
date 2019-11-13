using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Service
{
    public interface ICryptoService
    {
        string CreateSalt();

        string HashPassword(string password, string salt);
    }
}
