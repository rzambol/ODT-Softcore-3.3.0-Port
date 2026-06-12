using System.Text.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Utils;

namespace Softcore.Servers;

/// <summary>
/// Lightweight config loader for this mods config/config.json5 file.
///
/// This is intentionally not a full JSON5 parser. It only supports the subset
/// used by this mod config:
///   - single-line comments
///   - block comments
///   - trailing commas
///   - unquoted object keys
///
/// The TS ConfigServer architecture does not map 1:1 to SPT 4 C#, so this
/// class exists only to load this mods local config file into typed records.
/// </summary>
[Injectable]
public class ConfigServer(JsonUtil jsonUtil)
{
    private const string ConfigRelativePath = "config/config.json5";

    /// <summary>
    /// Load and deserialize this mods config/config.json5 relative to the mod folder.
    /// </summary>
    public T LoadConfig<T>(string modFolderPath)
    {
        var fullPath = Path.Combine(modFolderPath, ConfigRelativePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException(
                $"[Softcore] ConfigServer: config file not found at {fullPath}");

        var raw = File.ReadAllText(fullPath);
        var clean = StripJson5Extensions(raw);

        try
        {
            return jsonUtil.Deserialize<T>(clean)
                ?? throw new InvalidOperationException(
                    "[Softcore] ConfigServer: config deserialized to null");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"[Softcore] ConfigServer: failed to parse config - {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Convert the limited JSON5/JSONC syntax used by this mods config into
    /// plain JSON consumable by System.Text.Json.
    /// </summary>
    private static string StripJson5Extensions(string json5)
    {
        // Remove single-line comments first so they do not interfere with key quoting.
        var noLineComments = Regex.Replace(json5, @"//[^\n]*", "");

        // Remove block comments.
        var noBlockComments = Regex.Replace(noLineComments, @"/\*.*?\*/", "",
            RegexOptions.Singleline);

        // Quote unquoted object keys.
        var quotedKeys = Regex.Replace(noBlockComments,
            @"(?<=[{,\[]?\s*)([a-zA-Z_][a-zA-Z0-9_]*)(\s*:)",
            "\"$1\"$2");

        // Remove trailing commas before } or ].
        var noTrailingCommas = Regex.Replace(quotedKeys, @",\s*([}\]])", "$1");

        return noTrailingCommas;
    }
}
