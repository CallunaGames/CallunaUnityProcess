using Calluna.DI;
using UnityEngine;

namespace Calluna.Process.Samples
{
    public class ProcessorInstaller : MonoInstaller
    {
        [SerializeField] private Processor _processor;
        private readonly Observable<float> _progress = new Observable<float>();
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Processor>().ToInstance(_processor);
            binder.BindInstance(_processor.CurrentProcess);
            binder.Bind<Observable<float>>().And<ReadonlyObservable<float>>().ToInstance(_progress);
        }
    }
}
