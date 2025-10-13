using System;
using UnityEngine;

namespace Calluna.Process
{
    public class ProcessLogger : IDisposable
    {
        private const float _defaultLogFrequency = 0.5f;
        private const string _whiteHexcode = "FFFFFF";
        
        private readonly float _runningProcessLogFrequency;
        private readonly Process _process;
        private readonly string _processSourceName;
        private readonly string _loggerTypeHexCode;
        private readonly string _statusHexCode;
        private float _nextLogTime = float.MaxValue;

        public ProcessLogger(Process process)
        {
            _process = process;
            _process.Status.OnChangedWithValues += OnStatusChanged;
            _loggerTypeHexCode = _whiteHexcode;
            _statusHexCode = _whiteHexcode;
            _runningProcessLogFrequency = _defaultLogFrequency;
        }

        public ProcessLogger(Process process, ProcessLoggerSettings settings) :
            this(process)
        {
            _processSourceName = settings.LoggerName;
            _runningProcessLogFrequency = settings.RunningProcessLogFrequency;
            _loggerTypeHexCode = ColorUtility.ToHtmlStringRGB(settings.LoggerTypeColor);
            _statusHexCode = ColorUtility.ToHtmlStringRGB(settings.StatusColor);
        }

        public void Tick()
        {
            if (_process.IsRunning && Time.time > _nextLogTime)
            {
                Log();
                UpdateLogTime();
            }
        }

        public void Dispose()
        {
            _process.Status.OnChangedWithValues -= OnStatusChanged;
        }

        private void OnStatusChanged(ProcessStatus formerValue, ProcessStatus newValue)
        {
            Log();
            if (formerValue is not ProcessStatus.Running && newValue is ProcessStatus.Running)
            {
                UpdateLogTime();
            }
        }

        private void Log()
        {
            string progress = _process.IsRunning ? $" ({(int)(_process.Progress.Value * 100)}%)" : string.Empty;
            Debug.Log(
                $"<color=#{_loggerTypeHexCode}>[{_processSourceName}]</color> {_process.Name.Value} <color=#{_statusHexCode}>({_process.Status.Value}{progress})</color>");
        }

        private void UpdateLogTime()
        {
            _nextLogTime = Time.time + _runningProcessLogFrequency;
        }
    }
}