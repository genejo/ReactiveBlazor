using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using ReactiveUI;

namespace HelloReactiveBlazor.ViewModels {
    public class ReactiveHelloViewModel : ReactiveObject, IActivatableViewModel {
        public ViewModelActivator Activator { get; }

        private string _greeting;
        public string Greeting {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }

        public ReactiveHelloViewModel() {
            Activator = new ViewModelActivator();
 
            this.WhenActivated(
                disposables => {
                    Observable
                        .Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                        .Take(5)
                        .Do(
                            t => { Greeting = $"Hello, {t}!"; },
                            () => Console.WriteLine("Those are all the greetings, folks! ")
                        )
                        .Subscribe()
                        .DisposeWith(disposables);
                });
        }
    }
}
