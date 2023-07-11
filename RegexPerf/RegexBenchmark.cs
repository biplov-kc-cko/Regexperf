using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace RegexPerf
{
    [MemoryDiagnoser]
    public partial class RegexBenchmark
    {
        public const string MastercardSchemeIdRegex = "^[0-9A-Z]{9}[0-9]{4}$";
        public const string VisaSchemeIdRegex = "^[0-9]{15}$";
        private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(100);
        private static readonly Regex MastercardSchemeIdPattern = new(pattern: "^[0-9A-Z]{9}[0-9]{4}$", options: RegexOptions.Compiled, matchTimeout: _regexTimeout);
        private static readonly Regex VisaSchemeIdPattern = new(pattern: "^[0-9]{15}$", options: RegexOptions.Compiled, matchTimeout: _regexTimeout);

        [Params(
            "E27T7ELT95641",
            "990965296446484")]
        public string SchemeId { get; set; } = default!;

        [Benchmark]
        public bool MastercardSchemeIdPattern_Compiled() => MastercardSchemeIdPattern.IsMatch(SchemeId);

        [Benchmark]
        public bool VisaSchemeIdPattern_Compiled() => VisaSchemeIdPattern.IsMatch(SchemeId);

        [Benchmark]
        public bool MastercardRegex_NonCompiled() => Regex.IsMatch(SchemeId, MastercardSchemeIdRegex, RegexOptions.None, _regexTimeout);

        [Benchmark]
        public bool VisaRegex_NonCompiled() => Regex.IsMatch(SchemeId, VisaSchemeIdRegex, RegexOptions.None, _regexTimeout);
    }
}
