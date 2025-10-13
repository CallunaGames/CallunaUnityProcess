using System;
using UnityEngine;

namespace Calluna.Process
{
    [Serializable]
    public class ProcessLoggerSettings
    {
        [field: SerializeField, Header("Options")] public string LoggerName { get; private set; } = "Process Logger";
        [field: SerializeField] public float RunningProcessLogFrequency { get; private set; } = 0.5f;
        [field: SerializeField, Header("Text colors")] public Color LoggerTypeColor { get; private set; } = new Color(0, 0.3f, 0.7f);
        [field: SerializeField] public Color StatusColor { get; private set; } = Color.white;
    }
}