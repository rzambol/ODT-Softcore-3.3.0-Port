using System.Text.Json.Serialization;

namespace Softcore;

public record Configuration
{
    [JsonPropertyName("general")] public General General { get; init; } = new();
    [JsonPropertyName("secureContainersOptions")] public SecureContainerOptions SecureContainersOptions { get; init; } = new();
    [JsonPropertyName("hideoutOptions")] public HideoutOptions HideoutOptions { get; init; } = new();
    [JsonPropertyName("economyOptions")] public EconomyOptions EconomyOptions { get; init; } = new();
    [JsonPropertyName("traderChanges")] public TraderChanges TraderChanges { get; init; } = new();
    [JsonPropertyName("craftingChanges")] public CraftingChanges CraftingChanges { get; init; } = new();
    [JsonPropertyName("insuranceChanges")] public InsuranceChanges InsuranceChanges { get; init; } = new();
    [JsonPropertyName("otherTweaks")] public OtherTweaks OtherTweaks { get; init; } = new();
}

public record General
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("debug")] public bool Debug { get; init; } = false;
}

public record SecureContainerOptions
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("progressiveContainers")] public ProgressiveContainers ProgressiveContainers { get; init; } = new();
    [JsonPropertyName("biggerContainers")] public bool BiggerContainers { get; init; } = true;
}

public record ProgressiveContainers
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("collectorQuestRedone")] public bool CollectorQuestRedone { get; init; } = true;
}

public record HideoutOptions
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("stashOptions")] public StashOptions StashOptions { get; init; } = new();
    [JsonPropertyName("hideoutContainers")] public HideoutContainers HideoutContainers { get; init; } = new();
    [JsonPropertyName("fasterBitcoinFarming")] public FasterBitcoinFarming FasterBitcoinFarming { get; init; } = new();
    [JsonPropertyName("fasterCraftingTime")] public FasterCraftingTime FasterCraftingTime { get; init; } = new();
    [JsonPropertyName("fasterHideoutConstruction")] public FasterHideoutConstruction FasterHideoutConstruction { get; init; } = new();
    [JsonPropertyName("fuelConsumption")] public FuelConsumption FuelConsumption { get; init; } = new();
    [JsonPropertyName("scavCaseOptions")] public ScavCaseOptions ScavCaseOptions { get; init; } = new();
    [JsonPropertyName("allowGymTrainingWithMusclePain")] public bool AllowGymTrainingWithMusclePain { get; init; } = true;
}

public record StashOptions
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("progressiveStash")] public bool ProgressiveStash { get; init; } = true;
    [JsonPropertyName("biggerStash")] public bool BiggerStash { get; init; } = true;
    [JsonPropertyName("lessCurrencyForConstruction")] public bool LessCurrencyForConstruction { get; init; } = true;
    [JsonPropertyName("easierLoyalty")] public bool EasierLoyalty { get; init; } = true;
}

public record HideoutContainers
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("biggerHideoutContainers")] public bool BiggerHideoutContainers { get; init; } = true;
    [JsonPropertyName("siccCaseBuff")] public bool SiccCaseBuff { get; init; } = true;
}

public record FasterBitcoinFarming
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("setBitcoinPriceTo100k")] public bool SetBitcoinPriceTo100k { get; init; } = true;
    [JsonPropertyName("baseBitcoinTimeMultiplier")] public double BaseBitcoinTimeMultiplier { get; init; } = 2.0;
    [JsonPropertyName("gpuEfficiency")] public double GpuEfficiency { get; init; } = 0.15;
}

public record FasterCraftingTime
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("baseCraftingTimeMultiplier")] public double BaseCraftingTimeMultiplier { get; init; } = 2.0;
    [JsonPropertyName("hideoutSkillExpFix")] public HideoutSkillExpFix HideoutSkillExpFix { get; init; } = new();
    [JsonPropertyName("fasterMoonshineProduction")] public FasterProduction FasterMoonshineProduction { get; init; } = new();
    [JsonPropertyName("fasterPurifiedWaterProduction")] public FasterProduction FasterPurifiedWaterProduction { get; init; } = new();
    [JsonPropertyName("fasterCultistCircle")] public FasterProduction FasterCultistCircle { get; init; } = new();
}

public record FasterProduction
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("baseCraftingTimeMultiplier")] public double BaseCraftingTimeMultiplier { get; init; } = 2.0;
}

public record HideoutSkillExpFix
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("hideoutSkillExpMultiplier")] public double HideoutSkillExpMultiplier { get; init; } = 2.0;
}

public record FasterHideoutConstruction
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("hideoutConstructionTimeMultiplier")] public double HideoutConstructionTimeMultiplier { get; init; } = 4.0;
}

public record FuelConsumption
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("fuelConsumptionMultiplier")] public double FuelConsumptionMultiplier { get; init; } = 0.5;
}

public record ScavCaseOptions
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("betterRewards")] public bool BetterRewards { get; init; } = true;
    [JsonPropertyName("fasterScavcase")] public FasterScavcase FasterScavcase { get; init; } = new();
    [JsonPropertyName("rebalance")] public bool Rebalance { get; init; } = true;
}

public record FasterScavcase
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("speedMultiplier")] public double SpeedMultiplier { get; init; } = 2.0;
}

public record EconomyOptions
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("disableFleaMarketCompletely")] public bool DisableFleaMarketCompletely { get; init; } = false;
    [JsonPropertyName("priceRebalance")] public PriceRebalance PriceRebalance { get; init; } = new();
    [JsonPropertyName("pacifistFleaMarket")] public PacifistFleaMarket PacifistFleaMarket { get; init; } = new();
    [JsonPropertyName("barterEconomy")] public BarterEconomy BarterEconomy { get; init; } = new();
    [JsonPropertyName("otherFleaMarketChanges")] public OtherFleaMarketChanges OtherFleaMarketChanges { get; init; } = new();
}

public record PriceRebalance
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("itemFixes")] public bool ItemFixes { get; init; } = true;
}

public record PacifistFleaMarket
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("whitelist")] public EconomyToggles Whitelist { get; init; } = new();
    [JsonPropertyName("questKeys")] public EconomyToggles QuestKeys { get; init; } = new();
    [JsonPropertyName("markedKeys")] public EconomyToggles MarkedKeys { get; init; } = new();
}

public record EconomyToggles
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("priceMultiplier")] public double PriceMultiplier { get; init; } = 1.0;
}

public record BarterEconomy
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("cashOffersPercentage")] public int CashOffersPercentage { get; init; } = 10;
    [JsonPropertyName("barterPriceVariance")] public int BarterPriceVariance { get; init; } = 20;
    [JsonPropertyName("offerItemCount")] public MinMaxInt OfferItemCount { get; init; } = new() { Min = 1, Max = 2 };
    [JsonPropertyName("nonStackableCount")] public MinMaxInt NonStackableCount { get; init; } = new() { Min = 1, Max = 2 };
    [JsonPropertyName("itemCountMax")] public int ItemCountMax { get; init; } = 3;
    [JsonPropertyName("unbanBitcoinsForBarters")] public bool UnbanBitcoinsForBarters { get; init; } = true;
}

public record MinMaxInt
{
    [JsonPropertyName("min")] public int Min { get; init; }
    [JsonPropertyName("max")] public int Max { get; init; }
}

public record OtherFleaMarketChanges
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("sellingOnFlea")] public bool SellingOnFlea { get; init; } = false;
    [JsonPropertyName("fleaMarketOpenAtLevel")] public int FleaMarketOpenAtLevel { get; init; } = 15;
    [JsonPropertyName("fleaPricesIncreased")] public double FleaPricesIncreased { get; init; } = 1.0;
    [JsonPropertyName("fleaPristineItems")] public bool FleaPristineItems { get; init; } = true;
    [JsonPropertyName("onlyFoundInRaidItemsAllowedForBarters")] public bool OnlyFoundInRaidItemsAllowedForBarters { get; init; } = false;
}

public record TraderChanges
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("betterSalesToTraders")] public bool BetterSalesToTraders { get; init; } = true;
    [JsonPropertyName("alternativeCategories")] public bool AlternativeCategories { get; init; } = true;
    [JsonPropertyName("pacifistFence")] public PacifistFence PacifistFence { get; init; } = new();
    [JsonPropertyName("reasonablyPricedCases")] public bool ReasonablyPricedCases { get; init; } = true;
    [JsonPropertyName("skierUsesEuros")] public bool SkierUsesEuros { get; init; } = true;
    [JsonPropertyName("biggerLimits")] public BiggerLimits BiggerLimits { get; init; } = new();
}

public record PacifistFence
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("numberOfFenceOffers")] public int NumberOfFenceOffers { get; init; } = 30;
}

public record BiggerLimits
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("multiplier")] public double Multiplier { get; init; } = 3.0;
}

public record CraftingChanges
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("craftingRebalance")] public bool CraftingRebalance { get; init; } = true;
    [JsonPropertyName("additionalCraftingRecipes")] public bool AdditionalCraftingRecipes { get; init; } = true;
}

public record InsuranceChanges
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("praporInsuranceChanges")] public TraderInsuranceChanges PraporInsuranceChanges { get; init; } = new();
    [JsonPropertyName("therapistInsuranceChanges")] public TraderInsuranceChanges TherapistInsuranceChanges { get; init; } = new();
}

public record TraderInsuranceChanges
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("returnChance")] public int ReturnChance { get; init; } = 90;
    [JsonPropertyName("returnTime")] public MinMaxInt ReturnTime { get; init; } = new() { Min = 1, Max = 4 };
    [JsonPropertyName("insuranceCostPercentage")] public int InsuranceCostPercentage { get; init; } = 5;
}

public record OtherTweaks
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("skillExpBuffs")] public bool SkillExpBuffs { get; init; } = true;
    [JsonPropertyName("signalPistolInSpecialSlots")] public bool SignalPistolInSpecialSlots { get; init; } = true;
    [JsonPropertyName("unexaminedItemsAreBack")] public bool UnexaminedItemsAreBack { get; init; } = true;
    [JsonPropertyName("fasterExamineTime")] public bool FasterExamineTime { get; init; } = true;
    [JsonPropertyName("removeBackpackRestrictions")] public bool RemoveBackpackRestrictions { get; init; } = true;
    [JsonPropertyName("removeDiscardLimit")] public bool RemoveDiscardLimit { get; init; } = true;
    [JsonPropertyName("reshalaAlwaysHasGoldenTT")] public bool ReshalaAlwaysHasGoldenTT { get; init; } = true;
    [JsonPropertyName("biggerAmmoStacks")] public BiggerAmmoStacks BiggerAmmoStacks { get; init; } = new();
    [JsonPropertyName("questChanges")] public bool QuestChanges { get; init; } = true;
    [JsonPropertyName("removeRaidItemLimits")] public bool RemoveRaidItemLimits { get; init; } = true;
    [JsonPropertyName("biggerCurrencyStacks")] public bool BiggerCurrencyStacks { get; init; } = true;
    [JsonPropertyName("smallContainersInSpecialSlots")] public bool SmallContainersInSpecialSlots { get; init; } = true;
}

public record BiggerAmmoStacks
{
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = true;
    [JsonPropertyName("stackMultiplier")] public int StackMultiplier { get; init; } = 5;
    [JsonPropertyName("botAmmoStackFix")] public bool BotAmmoStackFix { get; init; } = true;
}
