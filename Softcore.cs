using Softcore.Util;
using Softcore.Servers;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Helpers;
using System.Reflection;
using Softcore.Changers;

namespace Softcore;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.odt.softcore";
    public override string Name { get; init; } = "ODT-Softcore";
    public override string Author { get; init; } = "ODT";
    public override List<string>? Contributors { get; init; } = [];
    public override SemanticVersioning.Version Version { get; init; } = new("4.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.13");
    public override List<string>? Incompatibilities { get; init; } = [];
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = [];
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; } = false;
    public override string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class SoftcoreMod(
    PrefixLogger log,
    ModHelper modHelper,
    ConfigServer configServer,
    SecureContainerOptionsChanger secureContainerOptionsChanger,
    HideoutOptionsChanger hideoutOptionsChanger,
    EconomyOptionsChanger economyOptionsChanger,
    TraderChangesChanger traderChangesChanger,
    CraftingChangesChanger craftingChangesChanger,
    InsuranceChangesChanger insuranceChangesChanger,
    OtherTweaksChanger otherTweaksChanger
) : IOnLoad
{
    private Configuration? _config;

    public Task OnLoad()
    {
        var pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());

        try
        {
            _config = configServer.LoadConfig<Configuration>(pathToMod);
        }
        catch (Exception ex)
        {
            _config = null;
            log.Error($"ConfigServer: {ex.Message}");
            return Task.CompletedTask;
        }

        // Can stop if config is either null or not initialized
        if (_config is null)
        {
            return Task.CompletedTask;
        }

        // Can stop if mod not enabled
        if (!_config.General.Enabled)
        {
            log.Info("Config: Mod disabled in the config file");
            _config = null;
            return Task.CompletedTask;
        }

        log.SetDebug(_config.General.Debug);

        // Initialize all the changes and apply them according to the config
        secureContainerOptionsChanger.Apply(_config.SecureContainersOptions);
        hideoutOptionsChanger.Apply(_config.HideoutOptions);
        economyOptionsChanger.Apply(_config.EconomyOptions);
        traderChangesChanger.Apply(_config.TraderChanges);
        craftingChangesChanger.Apply(_config.CraftingChanges);
        insuranceChangesChanger.Apply(_config.InsuranceChanges);
        otherTweaksChanger.Apply(_config.OtherTweaks);

        log.Info("Loaded Changes successfully");
        return Task.CompletedTask;
    }
}
