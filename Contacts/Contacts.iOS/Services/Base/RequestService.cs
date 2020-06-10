using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contacts.Core.Extensions;
using Contacts.Core.Models.Base;
using Contacts.Core.Services.Interfaces;
using ModernHttpClient;
using Newtonsoft.Json;

namespace Contacts.iOS.Services.Base
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;

        public RequestService()
        {
            _httpClient = new HttpClient(new NativeMessageHandler
            {
                ServerCertificateCustomValidationCallback = ServerCertificateValidationCallback
            });
        }

        public async Task<ClientResponse<T>> Get<T>(string path, TimeSpan timeout, bool needAuthorization = false)
        {
            var result = new ClientResponse<T>();
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);
                cts.Token.ThrowIfCancellationRequested();

                if (needAuthorization)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {GetToken()}");
                }

                var response = await _httpClient.GetAsync(path, cts.Token);
                result.StatusCode = response.StatusCode;

                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    result.Content = json.FromJson<T>();
                else
                    result.Error = new ErrorResponse {Code = response.StatusCode.ToString(), Message = json};
            }
            catch (OperationCanceledException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (Exception ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }

            return result;
        }

        public async Task<ClientResponse<bool>> GetWithoutContent<T>(string path, TimeSpan timeout, bool needAuthorization = false)
        {
            var result = new ClientResponse<bool>();
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);
                cts.Token.ThrowIfCancellationRequested();

                if (needAuthorization)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {GetToken()}");
                }

                var response = await _httpClient.GetAsync(path, cts.Token);
                result.StatusCode = response.StatusCode;

                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    result.Content = true;
                else
                    result.Error = new ErrorResponse {Code = response.StatusCode.ToString(), Message = json};
            }
            catch (OperationCanceledException ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (Exception ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }

            return result;
        }

        public async Task<ClientResponse<T>> Post<T, IT>(string path, IT data, TimeSpan timeout, bool needAuthorization = false)
        {
            var result = new ClientResponse<T>();
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);
                cts.Token.ThrowIfCancellationRequested();
                var body = JsonConvert.SerializeObject(data);

                if (needAuthorization)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {GetToken()}");
                }

                var response = await _httpClient.PostAsync(path, new StringContent(body, Encoding.UTF8, "application/json"), cts.Token);
                result.StatusCode = response.StatusCode;

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    result.Content = json.FromJson<T>();
                else
                    result.Error = new ErrorResponse {Code = response.StatusCode.ToString(), Message = json};
            }
            catch (OperationCanceledException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (Exception ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }

            return result;
        }

        public async Task<ClientResponse<T>> Put<T, IT>(string path, IT data, TimeSpan timeout, bool needAuthorization = false)
        {
            var result = new ClientResponse<T>();
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);
                cts.Token.ThrowIfCancellationRequested();
                var body = JsonConvert.SerializeObject(data);

                if (needAuthorization)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {GetToken()}");
                }

                var response = await _httpClient.PutAsync(path, new StringContent(body, Encoding.UTF8, "application/json"), cts.Token);
                result.StatusCode = response.StatusCode;

                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    result.Content = json.FromJson<T>();
                else
                    result.Error = new ErrorResponse {Code = response.StatusCode.ToString(), Message = json};
            }
            catch (OperationCanceledException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }
            catch (Exception ex)
            {
                return new ClientResponse<T> {IsCanceled = true, Error = new ErrorResponse(ex)};
            }

            return result;
        }

        public async Task<ClientResponse<bool>> Delete(string path, TimeSpan timeout, bool needAuthorization = false)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(timeout);
                cts.Token.ThrowIfCancellationRequested();

                if (needAuthorization)
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {GetToken()}");
                }

                var response = await _httpClient.DeleteAsync(path, cts.Token);

                return new ClientResponse<bool> {Content = response.IsSuccessStatusCode};
            }
            catch (OperationCanceledException ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex), Content = false};
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex), Content = false};
            }
            catch (Exception ex)
            {
                return new ClientResponse<bool> {IsCanceled = true, Error = new ErrorResponse(ex), Content = false};
            }
        }

        private string GetToken()
        {
            //TODO: if needed load token from db or local storage etc
            return string.Empty;
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }
    }
}