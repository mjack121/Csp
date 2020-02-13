﻿using Csp;
using Microsoft.Extensions.Options;
using Mt.Ask.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public AuthService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/u/api/v1";
        }

        public async Task<OptResult> BindCell(string cell, int userId)
        {
            string uri = API.Auth.BindCell(_remoteServiceBaseUrl, cell,userId);

            var response = await _httpClient.PutAsync(uri, null);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OptResult>(responseString);
        }

        public async Task<string> GetAuthUrl(string redirectUrl)
        {
            string uri = API.Auth.GetAuthUrl($"{_settings.Value.OcelotUrl}/api/v1/wx", redirectUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }

        public async Task<User> GetUser(string code)
        {
            string uri = API.Auth.GetUser(_remoteServiceBaseUrl, code);

            var response = await _httpClient.PostAsync(uri,null);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User>(responseString) ;
        }

        public async Task<User> SignByPwd(LoginModel model)
        {
            string uri = API.Auth.UserLogin(_remoteServiceBaseUrl);
            
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8);

            var response = await _httpClient.PostAsync(uri, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User>(responseString);
        }
    }
}
