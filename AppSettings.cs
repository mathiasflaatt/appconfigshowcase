namespace AzureAppConfigurationTest;

public sealed record AppSettings
{
    public string? ValueOne { get; init; }
    public string? ValueTwo { get; init; }
    public string? KeyVaultValue { get; init; }
}