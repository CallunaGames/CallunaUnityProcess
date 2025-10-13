using Calluna.DI;
using UnityEngine;

namespace Calluna.Process.Samples
{
    public class TimerProcessTest : MonoBehaviour, Injectable
    {
        [SerializeField, Header("Options")] private float _timerDuration = 3;
        [SerializeField] private string _timerName = "Timer Process Test";
        [SerializeField] private bool _useDefaultName = false;

        private Processor _processor;

        public void Inject(Resolver resolver)
        {
            _processor = resolver.Resolve<Processor>();
        }
        
        private void Start()
        {
            ControllableProcess process = _useDefaultName
                ? new TimerProcess(_timerDuration)
                : new TimerProcess(_timerDuration, _timerName);
            _processor.Process(process);
        }
    }
}