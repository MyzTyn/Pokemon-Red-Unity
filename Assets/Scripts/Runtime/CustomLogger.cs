using System;
using PokemonUnity;
using UnityEngine;

// ToDo: Update the DLLs to use LogManager.Logger
public class CustomLogger : IDebugger
{
    public void Init(string logfilePath, string logBaseName)
    {
        
    }

    public void Log(string message, params object[] param)
    {
        Debug.Log(string.Format(message, param));
    }

    public void LogDebug(string message, params object[] param)
    {
        Debug.Log(string.Format(message, param));
    }

    public void LogWarning(string message, params object[] param)
    {
        Debug.LogWarning(string.Format(message, param));
    }

    public void LogError(string message, params object[] param)
    {
        Debug.LogError(string.Format(message, param));
    }

    public void Shutdown()
    {
    }

    public event EventHandler<OnDebugEventArgs> OnLog;
}