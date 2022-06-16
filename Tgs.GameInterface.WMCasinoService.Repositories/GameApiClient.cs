using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Tgs.GameInterface.WMCasinoService.Core;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.Logging;

namespace Tgs.GameInterface.WMCasinoService.Repositories
{
    internal class GameApiClient : IGameApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<GameApiClient> _logger;
        private readonly ITgsLog _tgsLog;

        public GameApiClient(HttpClient client, ILogger<GameApiClient> logger, ITgsLog tgsLog)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tgsLog = tgsLog ?? throw new ArgumentNullException(nameof(tgsLog));
        }

        public async ValueTask<ChangeBalanceResponse> ChangeBalanceAsync(Uri apiUrl, ChangeBalanceRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "ChangeBalance",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["user"] = request.User,
                ["money"] = request.Money.ToString(),
                ["order"] = request.Order == null ? string.Empty : request.Order.ToString(),
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            };

            var document = await PostAsync(apiUrl, "", parameters, "ChangeBalance", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new ChangeBalanceResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = JsonSerializer.Deserialize<ChangeBalanceResult>(document.RootElement.GetProperty("result").GetRawText())!
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("ChangeBalance Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Change Balance Failed");
            }
        }

        public async ValueTask<GetBalanceResponse> GetBalanceAsync(Uri apiUrl, GetBalanceRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "GetBalance",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["user"] = request.User,
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            };

            var document = await PostAsync(apiUrl, "", parameters, "GetBalance", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new GetBalanceResponse
                {
                    ErrorCode = document.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = document.RootElement.GetProperty("result").GetDecimal(),
                    IsPlayerExist = true
                };
            }
            else if (errorCode == 10501)
            {
                return new GetBalanceResponse
                {
                    ErrorCode = document.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = null,
                    IsPlayerExist = false
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("GetBalance Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Get Balance Failed");
            }
        }

        public async ValueTask<GetDateTimeReportResponse> GetDateTimeReportAsync(Uri apiUrl, GetDateTimeReportRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "GetDateTimeReport",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["startTime"] = request.StartTime,
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            };

            if (request.User != null)
                parameters.Add("user", request.User);

            if (request.EndTime != null)
                parameters.Add("endTime", request.EndTime);

            if (request.TimeType != null)
                parameters.Add("timetype", request.TimeType.Value.ToString());

            if (request.DataType != null)
                parameters.Add("datatype", request.DataType.Value.ToString());

            if (request.GameNo1 != null)
                parameters.Add("gameno1", request.GameNo1.Value.ToString());

            var document = await PostAsync(apiUrl, "", parameters, "GetDateTimeReport", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new GetDateTimeReportResponse
                {
                    ErrorCode = document.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = JsonSerializer.Deserialize<GetDateTimeReportResult[]>(document.RootElement.GetProperty("result").GetRawText(), new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                        }
                    })!
                };
            }
            else if (errorCode == 107)
            {
                return new GetDateTimeReportResponse
                {
                    ErrorCode = document.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = Array.Empty<GetDateTimeReportResult>()
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("GetDateTimeReport Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Get Date Time Report Failed");
            }
        }

        public async ValueTask<GetMemberTradeReportResponse> GetMemberTradeReportAsync(Uri apiUrl, GetMemberTradeReportRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "GetMemberTradeReport",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            };

            if (request.User != null)
                parameters.Add("user", request.User.ToString());

            if (request.OrderId != null)
                parameters.Add("orderid", request.OrderId);

            if (request.Order != null)
                parameters.Add("order", request.Order);

            if (request.StartTime != null)
                parameters.Add("startTime", request.StartTime.ToString());

            if (request.EndTime != null)
                parameters.Add("endTime", request.EndTime.ToString());

            var document = await PostAsync(apiUrl, "", parameters, "GetMemberTradeReport", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new GetMemberTradeReportResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = JsonSerializer.Deserialize<GetMemberTradeReportResult[]>(document.RootElement.GetProperty("result").GetRawText())!
                };
            }
            else if (errorCode == 107)
            {
                return new GetMemberTradeReportResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = Array.Empty<GetMemberTradeReportResult>()
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("GetMemberTradeReport Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Get Member Trade Report Failed");
            }
        }

        public async ValueTask<GetUnsettleReportResponse> GetUnsettleReportAsync(Uri apiUrl, GetUnsettleReportRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "GetUnsettleReport",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["Time"] = request.Time.ToString("yyyyMMdd"),
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()
            };

            var document = await PostAsync(apiUrl, "", parameters, "GetUnsettleReport", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new GetUnsettleReportResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = JsonSerializer.Deserialize<GetUnsettleReportResult[]>(document.RootElement.GetProperty("result").GetRawText())!
                };
            }
            else if (errorCode == 107)
            {
                return new GetUnsettleReportResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = Array.Empty<GetUnsettleReportResult>()
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("GetUnsettleReport Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Get Unsettle Report Failed");
            }
        }

        public async ValueTask<HelloResponse> HelloAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "Hello",
                ["vendorId"] = vendorId,
                ["signature"] = signature
            };

            var document = await PostAsync(
                apiUrl,
                "",
                parameters,
                "Hello",
                cancellationToken).ConfigureAwait(false);

            return new HelloResponse
            {
                ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                Result = document.RootElement.GetProperty("result").GetString()!
            };
        }

        public async ValueTask<LogoutGameResponse> LogoutGameAsync(Uri apiUrl, LogoutGameRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "LogoutGame",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["user"] = request.User == null ? string.Empty : request.User.ToString(),
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            };
            var document = await PostAsync(apiUrl, "", parameters, "LogoutGame", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new LogoutGameResponse
                {
                    ErrorCode = errorCode,
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("LogoutGame Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Logout Game Failed");
            }
        }

        public async ValueTask<MemberRegisterResponse> MemberRegisterAsync(Uri apiUrl, MemberRegisterRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "MemberRegister",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["user"] = request.User,
                ["password"] = request.Password,
                ["username"] = request.Username,
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            };

            var document = await PostAsync(apiUrl, "", parameters, "MemberRegister", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new MemberRegisterResponse
                {
                    ErrorCode = document.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = document.RootElement.GetProperty("result").GetString()!
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString();
                _logger.LogError("MemberRegister Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Member Register Failed");
            }
        }

        public async ValueTask<SigninGameResponse> SigninGameAsync(Uri apiUrl, SigninGameRequest request, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["cmd"] = "SigninGame",
                ["vendorId"] = request.VendorId,
                ["signature"] = request.Signature,
                ["user"] = request.User,
                ["password"] = request.Password,
                ["lang"] = request.Lang.ToString(),
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            };

            var document = await PostAsync(apiUrl, "", parameters, "SigninGame", cancellationToken);

            if (document is null)
            {
                throw new Exception("Document is empty.");
            }

            var errorCode = document!.RootElement.GetProperty("errorCode").GetInt32();

            if (errorCode == 0)
            {
                return new SigninGameResponse
                {
                    ErrorCode = document!.RootElement.GetProperty("errorCode").GetInt32(),
                    ErrorMessage = document.RootElement.GetProperty("errorMessage").GetString()!,
                    Result = document.RootElement.GetProperty("result").GetString()!
                };
            }
            else
            {
                var errorMessage = document.RootElement.GetProperty("errorMessage").GetString()!;
                _logger.LogError("SigninGame Failed, ErrorCode:{errorCode}, ErrorMessage:{errorMessage}", errorCode, errorMessage);
                throw new Exception("Signin Game Failed");
            }
        }

        private async ValueTask<JsonDocument?> PostAsync(Uri apiUrl, string path, IEnumerable<KeyValuePair<string, string>> parameters, string logPrefix, CancellationToken cancellationToken = default)
        {
            logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

            var sw = Stopwatch.StartNew();
            var url = new Uri(apiUrl, path);

            HttpContent content = new FormUrlEncodedContent(parameters);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
            {
                CharSet = "UTF-8"
            };

            var httpResponseMessage = await _client.PostAsync(url, content, cancellationToken);

            sw.Stop();

            var responseMessage = default(string);

            using (var sr = new StreamReader(httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken).Result))
            {
                responseMessage = await sr.ReadToEndAsync().ConfigureAwait(false) ?? string.Empty;
            }

            var apiLogEvent = new ApiLogEvent
            {
                Path = httpResponseMessage.RequestMessage?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
                Method = httpResponseMessage.RequestMessage?.Method.ToString().NoLongerThan(8) ?? string.Empty,
                Prefix = logPrefix,
                ServerName = Environment.MachineName.NoLongerThan(128),
                RequestHeaders = httpResponseMessage.RequestMessage?.Headers + httpResponseMessage.RequestMessage?.Content?.Headers?.ToString(),
                RequestContent = (await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)).NoLongerThan(4096),
                ResponseHeaders = httpResponseMessage.Headers.ToString(),
                ResponseContent = responseMessage.NoLongerThan(4096),
                ResponseCode = (short)httpResponseMessage.StatusCode,
                ExecutionTimeMs = (int)sw.ElapsedMilliseconds,
                RecordedOn = DateTimeOffset.UtcNow
            };
            _tgsLog.LogApi(apiLogEvent);
            _logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));

            _ = httpResponseMessage.EnsureSuccessStatusCode();

            return JsonDocument.Parse(responseMessage);
        }
    }
}
