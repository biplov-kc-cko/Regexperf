using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace RegexPerf
{
    [MemoryDiagnoser]
    public partial class RegexBenchmark
    {
        public const string AuthorizationPathRegex = @"^[/a-zA-Z0-9-_]*/authorizations/[a-zA-Z0-9-_]+/?$";
        public const string GeneralPathRegex = @"^[/a-zA-Z0-9-_]*/authorizations/([a-zA-Z0-9-_]+)/?[a-zA-Z0-9-_]*$";

        private static readonly Regex SAuthorizationPathRegex = new(AuthorizationPathRegex, RegexOptions.Compiled);
        private static readonly Regex SGeneralPathRegex = new(GeneralPathRegex, RegexOptions.Compiled);

        [Params(
            "/authorizations/pay_weslgbe5fvfubohris4g3wbqya",
            "authorizations/pay_zwvgrxqkfelunetid6xkrwxxgq/captures",
            "process/amex")]
        public string UrlPath { get; set; } = default!;

        [Benchmark]
        public bool IsAuthorizationPathRegex_Compiled() => SAuthorizationPathRegex.IsMatch(UrlPath);

        [Benchmark]
        public bool IsGeneralPathRegex_Compiled() => SGeneralPathRegex.IsMatch(UrlPath);

        [Benchmark]
        public bool IsAuthorizationPathRegex_NonCompiled() => Regex.IsMatch(UrlPath, AuthorizationPathRegex);

        [Benchmark]
        public bool IsGeneralPathRegex_NonCompiled() => Regex.IsMatch(UrlPath, GeneralPathRegex);
    }
}
