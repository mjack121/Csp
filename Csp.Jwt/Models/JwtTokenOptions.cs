﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Csp.Jwt.Models
{
    public sealed class JwtTokenOptions
    {
        /// <summary>
        /// Token发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 签名秘钥
        /// </summary>
        public string SigningKey { get; set; }

        public SecurityKey Key
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
            }
        }

        /// <summary>
        /// 数据保护配置存储路径
        /// </summary>
        public string KeyStoragePath { get; set; } = "C://key";

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expires { get; set; }
    }

    public struct TokenConstants
    {
        public const string TokenName = "access_token";
    }
}
