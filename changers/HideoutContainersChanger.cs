using System;
using System.Linq;
using System.Collections.Generic;
using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Models.Enums;

namespace Softcore.Changers
{
    [Injectable]
    public class HideoutContainersChanger
    {
        private readonly PrefixLogger log;
        private readonly DatabaseService databaseService;

        private static readonly Dictionary<MongoId, string> tplNames = new()
        {
            { ItemTpl.CONTAINER_MEDICINE_CASE, nameof(ItemTpl.CONTAINER_MEDICINE_CASE) },
            { ItemTpl.CONTAINER_MR_HOLODILNICK_THERMAL_BAG, nameof(ItemTpl.CONTAINER_MR_HOLODILNICK_THERMAL_BAG) },
            { ItemTpl.CONTAINER_MAGAZINE_CASE, nameof(ItemTpl.CONTAINER_MAGAZINE_CASE) },
            { ItemTpl.CONTAINER_ITEM_CASE, nameof(ItemTpl.CONTAINER_ITEM_CASE) },
            { ItemTpl.CONTAINER_WEAPON_CASE, nameof(ItemTpl.CONTAINER_WEAPON_CASE) },
            { ItemTpl.CONTAINER_KEY_TOOL, nameof(ItemTpl.CONTAINER_KEY_TOOL) },
            { ItemTpl.CONTAINER_THICC_WEAPON_CASE, nameof(ItemTpl.CONTAINER_THICC_WEAPON_CASE) },
            { ItemTpl.CONTAINER_DOCUMENTS_CASE, nameof(ItemTpl.CONTAINER_DOCUMENTS_CASE) },
            { ItemTpl.CONTAINER_SICC, nameof(ItemTpl.CONTAINER_SICC) }
        };

        public HideoutContainersChanger(PrefixLogger log, DatabaseService databaseService)
        {
            this.log = log;
            this.databaseService = databaseService;
        }

        public void Apply(HideoutContainers config)
        {
            if (config == null || !config.Enabled)
                return;

            if (config.BiggerHideoutContainers)
            {
                try
                {
                    DoBiggerHideoutContainers();
                }
                catch (Exception e)
                {
                    log.Warning($"HideoutContainers: DoBiggerHideoutContainers failed gracefully. Send bug report. Continue safely.\n{e}");
                }
            }

            if (config.SiccCaseBuff)
            {
                try
                {
                    DoSiccCaseBuff();
                }
                catch (Exception e)
                {
                    log.Warning($"HideoutContainers: DoSiccCaseBuff failed gracefully. Send bug report. Continue safely.\n{e}");
                }
            }
        }

        private void DoBiggerHideoutContainers()
        {
            var items = databaseService.GetTemplates()?.Items;
            if (items == null)
            {
                log.Warning("DoBiggerHideoutContainers: Items database not found.");
                return;
            }

            var containersToModify = new (MongoId Tpl, int CellsH, int CellsV)[]
            {
                (ItemTpl.CONTAINER_MEDICINE_CASE, 10, 10),
                (ItemTpl.CONTAINER_MR_HOLODILNICK_THERMAL_BAG, 10, 10),
                (ItemTpl.CONTAINER_MAGAZINE_CASE, 10, 7),
                (ItemTpl.CONTAINER_ITEM_CASE, 10, 10),
                (ItemTpl.CONTAINER_WEAPON_CASE, 6, 15),
                (ItemTpl.CONTAINER_KEY_TOOL, 5, 5),
                (ItemTpl.CONTAINER_THICC_WEAPON_CASE, 14, 15)
            };

            int resized = 0;
            foreach (var (tpl, cellsH, cellsV) in containersToModify)
            {
                if (!items.TryGetValue(tpl, out var item))
                {
                    log.Warning($"DoBiggerHideoutContainers: {tpl} not found.");
                    continue;
                }

                var grid = item.Properties?.Grids?.FirstOrDefault();
                if (grid?.Properties == null)
                {
                    log.Warning($"DoBiggerHideoutContainers: {tpl} has no grid.");
                    continue;
                }

                var name = tplNames.TryGetValue(tpl, out var n) ? n : tpl.ToString();
                grid.Properties.CellsH = cellsH;
                grid.Properties.CellsV = cellsV;
                resized++;
            }

            log.DebugLog($"BiggerHideoutContainers: containers resized: {resized}");
        }

        private void DoSiccCaseBuff()
        {
            var items = databaseService.GetTemplates()?.Items;
            if (items == null)
            {
                log.Warning("DoSiccCaseBuff: Items database not found.");
                return;
            }

            if (!items.TryGetValue(ItemTpl.CONTAINER_DOCUMENTS_CASE, out var docs) ||
                !items.TryGetValue(ItemTpl.CONTAINER_SICC, out var sicc))
            {
                log.Warning("DoSiccCaseBuff: docs or sicc case not found.");
                return;
            }

            var docsGrid = docs.Properties?.Grids?.FirstOrDefault();
            var siccGrid = sicc.Properties?.Grids?.FirstOrDefault();

            if (docsGrid?.Properties?.Filters == null || siccGrid?.Properties?.Filters == null)
            {
                log.Warning("DoSiccCaseBuff: grids or filters missing.");
                return;
            }

            var docsFilter = docsGrid.Properties.Filters.First().Filter;
            var siccFilter = siccGrid.Properties.Filters.First().Filter;

            if (docsFilter == null || siccFilter == null)
            {
                log.Warning("DoSiccCaseBuff: filter not found.");
                return;
            }

            var merged = new HashSet<MongoId>(docsFilter.Concat(siccFilter).Append(ItemTpl.CONTAINER_KEY_TOOL));
            siccGrid.Properties.Filters.First().Filter = merged;

            log.DebugLog("DoSiccCaseBuff: SICC filter merged with Docs case and KeyTool");
        }
    }
}
