namespace Infrustructure.GuardCluases;

public static class Ensure
{

    public static void NotNullOrEmpty(string? value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty.");
        }
    }
}
