using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Enums.Hideout;
using SPTarkov.Server.Core.Services;

namespace Softcore.Changers;

[Injectable]
public class StashOptionsChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    private static readonly MongoId StandardStash = ItemTpl.STASH_STANDARD_STASH_10X30;
    private static readonly MongoId LeftBehindStash = ItemTpl.STASH_LEFT_BEHIND_STASH_10X40;
    private static readonly MongoId PrepareStash = ItemTpl.STASH_PREPARE_FOR_ESCAPE_STASH_10X50;
    private static readonly MongoId EodStash = ItemTpl.STASH_EDGE_OF_DARKNESS_STASH_10X68;
    private static readonly MongoId UnheardStash = ItemTpl.STASH_THE_UNHEARD_EDITION_STASH_10X72;

    private static readonly HashSet<MongoId> AllStartingStashes = new()
    {
        StandardStash, LeftBehindStash, PrepareStash,
        EodStash, UnheardStash,
    };

    private static readonly Bonus BasicStashBonusTemplate = new()
    {
        Id = "64f5b9e5fa34f11b380756c0",
        TemplateId = StandardStash,
        Type = BonusType.StashSize,
    };

    private static readonly MongoId Roubles = ItemTpl.MONEY_ROUBLES;
    private static readonly MongoId Euros = ItemTpl.MONEY_EUROS;

    public void Apply(StashOptions config)
    {
        if (!config.Enabled) return;

        if (config.ProgressiveStash)
        {
            try
            {
                DoProgressiveStash();
            }
            catch (Exception e)
            {
                log.Warning($"StashOptions: DoProgressiveStash failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.BiggerStash)
        {
            try
            {
                DoBiggerStash();
            }
            catch (Exception e)
            {
                log.Warning($"StashOptions: DoBiggerStash failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.LessCurrencyForConstruction)
        {
            try
            {
                DoLessCurrencyForConstruction();
            }
            catch (Exception e)
            {
                log.Warning($"StashOptions: DoLessCurrencyForConstruction failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.EasierLoyalty)
        {
            try
            {
                DoEasierLoyalty();
            }
            catch (Exception e)
            {
                log.Warning($"StashOptions: DoEasierLoyalty failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }
    }

    private void DoProgressiveStash()
    {
        var profileTemplates = databaseService.GetTemplates().Profiles;

        foreach (var (profileId, profileSides) in profileTemplates)
        {
            foreach (var side in new[] { profileSides.Bear, profileSides.Usec })
            {
                if (side?.Character == null) continue;

                var stashArea = side.Character.Hideout?.Areas
                    ?.Find(a => a.Type == HideoutAreas.Stash);
                if (stashArea == null)
                {
                    log.Warning($"DoProgressiveStash: hideoutArea not found for profile {profileId}");
                    continue;
                }
                stashArea.Level = 1;

                var stashItems = side.Character.Inventory?.Items
                    ?.Where(i => AllStartingStashes.Contains(i.Template))
                    .ToList() ?? new();
                foreach (var item in stashItems)
                    item.Template = StandardStash;

                side.Character.Bonuses = side.Character.Bonuses
                    ?.Where(b => b.Type != BonusType.StashSize)
                    .ToList() ?? new();
                side.Character.Bonuses.Add(new Bonus
                {
                    Id = BasicStashBonusTemplate.Id,
                    TemplateId = BasicStashBonusTemplate.TemplateId,
                    Type = BasicStashBonusTemplate.Type
                });
            }
        }

        log.DebugLog($"DoProgressiveStash: profile templates processed: {profileTemplates.Count}");
    }

    private void DoBiggerStash()
    {
        var stashUpdates = new Dictionary<MongoId, (string EditionName, int Rows)>
        {
            [new(StandardStash)] = ("Standard", 50),
            [new(LeftBehindStash)] = ("Left Behind", 100),
            [new(PrepareStash)] = ("Prepare for Escape", 150),
            [new(EodStash)] = ("Edge of Darkness", 200),
            [new(UnheardStash)] = ("The Unheard Edition", 250),
        };

        var items = databaseService.GetTemplates().Items;
        int updated = 0;

        foreach (var (stashId, update) in stashUpdates)
        {
            var (editionName, newRows) = update;
            if (!items.TryGetValue(stashId, out var item) ||
                item.Properties?.Grids == null || !item.Properties.Grids.Any())
            {
                log.Warning($"DoBiggerStash: {editionName} stash not found or has no grids.");
                continue;
            }

            item.Properties.Grids.First().Properties.CellsV = newRows;
            log.DebugLog($"DoBiggerStash: {editionName} stash -> {newRows} rows");

            updated++;
        }

        log.DebugLog($"DoBiggerStash: stash templates resized: {updated}");
    }

    private void DoLessCurrencyForConstruction()
    {
        var hideout = databaseService.GetHideout();
        var stashArea = hideout.Areas.Find(a => a.Type == HideoutAreas.Stash);
        if (stashArea == null)
        {
            log.Warning("DoLessCurrencyForConstruction: stash area not found.");
            return;
        }

        int reqs = 0;

        foreach (var stage in stashArea.Stages.Values)
        {
            var currencyReqs = stage.Requirements
                .FindAll(r => r.TemplateId == Roubles || r.TemplateId == Euros);
            foreach (var req in currencyReqs)
                if (req.Count.HasValue)
                {
                    req.Count = req.Count.Value / 10;
                    reqs++;
                }
        }

        log.DebugLog($"DoLessCurrencyForConstruction: currency requirements reduced (/10): {reqs}");
    }

    private void DoEasierLoyalty()
    {
        var hideout = databaseService.GetHideout();
        var stashArea = hideout.Areas.Find(a => a.Type == HideoutAreas.Stash);
        if (stashArea == null)
        {
            log.Warning("DoEasierLoyalty: stash area not found.");
            return;
        }

        int reqs = 0;
        foreach (var stage in stashArea.Stages.Values)
        {
            var loyaltyReqs = stage.Requirements.FindAll(r => r.LoyaltyLevel.HasValue);
            foreach (var req in loyaltyReqs)
                if (req.LoyaltyLevel.HasValue)
                {
                    req.LoyaltyLevel = req.LoyaltyLevel.Value - 1;
                    reqs++;
                }
        }

        log.DebugLog($"DoEasierLoyalty: loyalty level requirements reduced (-1): {reqs}");
    }
}
