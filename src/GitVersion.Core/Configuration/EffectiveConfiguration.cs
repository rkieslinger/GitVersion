using GitVersion.Extensions;
using GitVersion.VersionCalculation;

namespace GitVersion.Configuration;

/// <summary>
///     Configuration can be applied to different things, effective configuration is the result after applying the
///     appropriate configuration
/// </summary>
public class EffectiveConfiguration
{
    public EffectiveConfiguration(GitVersionConfiguration configuration, BranchConfiguration currentBranchConfig)
    {
        configuration.NotNull();
        currentBranchConfig.NotNull();

        var name = currentBranchConfig.Name;

        if (!currentBranchConfig.VersioningMode.HasValue)
            throw new Exception($"Configuration value for 'Versioning mode' for branch {name} has no value. (this should not happen, please report an issue)");

        if (!currentBranchConfig.Increment.HasValue)
            throw new Exception($"Configuration value for 'Increment' for branch {name} has no value. (this should not happen, please report an issue)");

        if (!configuration.AssemblyVersioningScheme.HasValue)
            throw new Exception("Configuration value for 'AssemblyVersioningScheme' has no value. (this should not happen, please report an issue)");

        if (!configuration.AssemblyFileVersioningScheme.HasValue)
            throw new Exception("Configuration value for 'AssemblyFileVersioningScheme' has no value. (this should not happen, please report an issue)");

        if (!configuration.CommitMessageIncrementing.HasValue)
            throw new Exception("Configuration value for 'CommitMessageIncrementing' has no value. (this should not happen, please report an issue)");

        if (!configuration.LabelPreReleaseWeight.HasValue)
            throw new Exception("Configuration value for 'LabelPreReleaseWeight' has no value. (this should not happen, please report an issue)");

        AssemblyVersioningScheme = configuration.AssemblyVersioningScheme.Value;
        AssemblyFileVersioningScheme = configuration.AssemblyFileVersioningScheme.Value;
        AssemblyInformationalFormat = configuration.AssemblyInformationalFormat;
        AssemblyVersioningFormat = configuration.AssemblyVersioningFormat;
        AssemblyFileVersioningFormat = configuration.AssemblyFileVersioningFormat;
        VersioningMode = currentBranchConfig.VersioningMode.Value;
        LabelPrefix = configuration.LabelPrefix;
        Label = currentBranchConfig.Label ?? string.Empty;
        NextVersion = configuration.NextVersion;
        Increment = currentBranchConfig.Increment.Value;
        BranchPrefixToTrim = currentBranchConfig.Regex;
        PreventIncrementOfMergedBranchVersion = currentBranchConfig.PreventIncrementOfMergedBranchVersion ?? false;
        LabelNumberPattern = currentBranchConfig.LabelNumberPattern;
        ContinuousDeploymentFallbackLabel = configuration.ContinuousDeploymentFallbackLabel;
        TrackMergeTarget = currentBranchConfig.TrackMergeTarget ?? false;
        MajorVersionBumpMessage = configuration.MajorVersionBumpMessage;
        MinorVersionBumpMessage = configuration.MinorVersionBumpMessage;
        PatchVersionBumpMessage = configuration.PatchVersionBumpMessage;
        NoBumpMessage = configuration.NoBumpMessage;
        CommitMessageIncrementing = currentBranchConfig.CommitMessageIncrementing ?? configuration.CommitMessageIncrementing.Value;
        VersionFilters = configuration.Ignore.ToFilters();
        TracksReleaseBranches = currentBranchConfig.TracksReleaseBranches ?? false;
        IsReleaseBranch = currentBranchConfig.IsReleaseBranch ?? false;
        IsMainline = currentBranchConfig.IsMainline ?? false;
        CommitDateFormat = configuration.CommitDateFormat;
        UpdateBuildNumber = configuration.UpdateBuildNumber ?? true;
        SemanticVersionFormat = configuration.SemanticVersionFormat;
        PreReleaseWeight = currentBranchConfig.PreReleaseWeight ?? 0;
        LabelPreReleaseWeight = configuration.LabelPreReleaseWeight.Value;
    }

    protected EffectiveConfiguration(AssemblyVersioningScheme assemblyVersioningScheme,
        AssemblyFileVersioningScheme assemblyFileVersioningScheme,
        string? assemblyInformationalFormat,
        string? assemblyVersioningFormat,
        string? assemblyFileVersioningFormat,
        VersioningMode versioningMode,
        string? labelPrefix,
        string label,
        string? nextVersion,
        IncrementStrategy increment,
        string? branchPrefixToTrim,
        bool preventIncrementOfMergedBranchVersion,
        string? labelNumberPattern,
        string? continuousDeploymentFallbackLabel,
        bool trackMergeTarget,
        string? majorVersionBumpMessage,
        string? minorVersionBumpMessage,
        string? patchVersionBumpMessage,
        string? noBumpMessage,
        CommitMessageIncrementMode commitMessageIncrementing,
        IEnumerable<IVersionFilter> versionFilters,
        bool tracksReleaseBranches,
        bool isReleaseBranch,
        bool isMainline,
        string? commitDateFormat,
        bool updateBuildNumber,
        SemanticVersionFormat semanticVersionFormat,
        int preReleaseWeight,
        int labelPreReleaseWeight)
    {
        AssemblyVersioningScheme = assemblyVersioningScheme;
        AssemblyFileVersioningScheme = assemblyFileVersioningScheme;
        AssemblyInformationalFormat = assemblyInformationalFormat;
        AssemblyVersioningFormat = assemblyVersioningFormat;
        AssemblyFileVersioningFormat = assemblyFileVersioningFormat;
        VersioningMode = versioningMode;
        LabelPrefix = labelPrefix;
        Label = label;
        NextVersion = nextVersion;
        Increment = increment;
        BranchPrefixToTrim = branchPrefixToTrim;
        PreventIncrementOfMergedBranchVersion = preventIncrementOfMergedBranchVersion;
        LabelNumberPattern = labelNumberPattern;
        ContinuousDeploymentFallbackLabel = continuousDeploymentFallbackLabel;
        TrackMergeTarget = trackMergeTarget;
        MajorVersionBumpMessage = majorVersionBumpMessage;
        MinorVersionBumpMessage = minorVersionBumpMessage;
        PatchVersionBumpMessage = patchVersionBumpMessage;
        NoBumpMessage = noBumpMessage;
        CommitMessageIncrementing = commitMessageIncrementing;
        VersionFilters = versionFilters;
        TracksReleaseBranches = tracksReleaseBranches;
        IsReleaseBranch = isReleaseBranch;
        IsMainline = isMainline;
        CommitDateFormat = commitDateFormat;
        UpdateBuildNumber = updateBuildNumber;
        SemanticVersionFormat = semanticVersionFormat;
        PreReleaseWeight = preReleaseWeight;
        LabelPreReleaseWeight = labelPreReleaseWeight;
    }

    public bool TracksReleaseBranches { get; }
    public bool IsReleaseBranch { get; }
    public bool IsMainline { get; }
    public VersioningMode VersioningMode { get; }
    public AssemblyVersioningScheme AssemblyVersioningScheme { get; }
    public AssemblyFileVersioningScheme AssemblyFileVersioningScheme { get; }
    public string? AssemblyInformationalFormat { get; }
    public string? AssemblyVersioningFormat { get; }
    public string? AssemblyFileVersioningFormat { get; }

    /// <summary>
    ///     Git tag prefix
    /// </summary>
    public string? LabelPrefix { get; }

    /// <summary>
    ///     Label to use when calculating SemVer
    /// </summary>
    public string Label { get; }

    public string? NextVersion { get; }

    public IncrementStrategy Increment { get; }

    public string? BranchPrefixToTrim { get; }

    public bool PreventIncrementOfMergedBranchVersion { get; }

    public string? LabelNumberPattern { get; }

    public string? ContinuousDeploymentFallbackLabel { get; }

    public bool TrackMergeTarget { get; }

    public string? MajorVersionBumpMessage { get; }

    public string? MinorVersionBumpMessage { get; }

    public string? PatchVersionBumpMessage { get; }

    public string? NoBumpMessage { get; }

    public CommitMessageIncrementMode CommitMessageIncrementing { get; }

    public IEnumerable<IVersionFilter> VersionFilters { get; }

    public string? CommitDateFormat { get; }

    public bool UpdateBuildNumber { get; }

    public SemanticVersionFormat SemanticVersionFormat { get; set; } = SemanticVersionFormat.Strict;

    public int PreReleaseWeight { get; }

    public int LabelPreReleaseWeight { get; }
}
