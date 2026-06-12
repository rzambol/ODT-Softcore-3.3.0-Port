using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;

namespace Softcore.Util;

[Injectable]
public class PrefixLogger(ISptLogger<PrefixLogger> inner)
{
    private static bool _debug;
    private const string Prefix = "[Softcore]";

    public void SetDebug(bool enabled) => _debug = enabled;
    public bool IsDebug => _debug;

    public void Info(string message) => inner.Info($"{Prefix} {message}");
    public void Warning(string message) => inner.Warning($"{Prefix} {message}");
    public void Error(string message) => inner.Error($"{Prefix} {message}");

    public void DebugLog(string message)
    {
        if (_debug)
            inner.Info($"{Prefix} [DEBUG] {message}");
    }
}
