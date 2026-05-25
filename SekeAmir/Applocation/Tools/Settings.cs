namespace Application.Tools;

public static class Settings
{
    public static bool IsDebugMode()
    {
        #if DEBUG
                return true;
        #else
            return false;
        #endif
    }
}