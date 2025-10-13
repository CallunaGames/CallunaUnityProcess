using Calluna.DI;
using UnityEngine;

namespace Calluna.Process
{
    public class ProcessorLogger : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private ProcessLogger _logger;
        private ProcessLoggerSettings _settings;
        private Processor _processor;
        
        
        public void Inject(Resolver resolver)
        {
            _processor = resolver.Resolve<Processor>();
            _settings = resolver.ResolveOptional<ProcessLoggerSettings>() ?? new ProcessLoggerSettings();
        }

        public void Initialize()
        {
            _processor.CurrentProcess.OnChangedWithValues += OnProcessChanged;
            CreateLogger(_processor.CurrentProcess.Value);
        }

        public void Clean()
        {
            _processor.CurrentProcess.OnChangedWithValues -= OnProcessChanged;
        }
        
        private void Update()
        {
            _logger?.Tick();
        }

        private void OnProcessChanged(Process formerValue, Process newValue)
        {
            _logger?.Dispose();
            CreateLogger(newValue);
        }

        private void CreateLogger(Process process)
        {
            if (process != null)
            {
                _logger = new ProcessLogger(process, _settings);
            }
        }
    }
}