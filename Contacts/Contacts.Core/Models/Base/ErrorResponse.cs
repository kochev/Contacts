using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Contacts.Core.Models.Base
{
    public class ErrorResponse
    {
        private Error _errorData;

        public ErrorResponse()
        {
            Message = "Техническая ошибка. Попробуйте еще раз";
        }

        public ErrorResponse(Exception e) : this()
        {
            if (e is SocketException || e is WebException || e is HttpRequestException)
                ErrorException = new NetworkException(e.Message);
            else
                ErrorException = e;
            ErrorData = new Error {{"error", e.Message}};
        }

        public Error ErrorData
        {
            get => _errorData;
            set
            {
                _errorData = value;
                ProcessError();
            }
        }

        public string Code { get; set; }

        [JsonProperty("detail")] public string Message { get; set; }

        [JsonIgnore] public Exception ErrorException { get; set; }

        private void ProcessError()
        {
            if (ErrorData != null)
            {
                var value = ErrorData.OrderBy(kv => kv.Key)?.FirstOrDefault().Value;
                Message = value.GetType().IsArray ? (value as string[]).FirstOrDefault() : value.ToString();
                Message = Regex.Replace(Message, @"[\[\]']+", "").Replace("\"", "");
            }
        }
    }

    public class Error : Dictionary<string, object>
    {
    }
}