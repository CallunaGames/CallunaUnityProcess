using Calluna.DI;
using UnityEngine;

namespace Calluna.Process
{
    public class DestroyOnProcessFinished : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private GameObject _target;
        private Process _process;
        
        public void Inject(Resolver resolver)
        {
            _process = resolver.Resolve<Process>();
        }

        public void Initialize()
        {
            _process.OnFinished += DestroyObject;
        }

        public void Clean()
        {
            _process.OnFinished -= DestroyObject;
        }

        private void DestroyObject(Process process)
        {
            Destroy(_target);
        }
    }
}