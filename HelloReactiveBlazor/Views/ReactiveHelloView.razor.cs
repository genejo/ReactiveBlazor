using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using HelloReactiveBlazor.ViewModels;
using ReactiveUI;

namespace HelloReactiveBlazor.Views {
    public partial class ReactiveHelloView {
        public ReactiveHelloView()
        {
            ViewModel = new ReactiveHelloViewModel();

            this.WhenActivated(disposables =>
            {
                Console.WriteLine("view activated");
                Disposable
                    .Create(() => Console.WriteLine("view deactivated"))
                    .DisposeWith(disposables);

                this
                    .WhenAnyValue(v => v.ViewModel.Greeting)
                    .Do(greeting => { Console.WriteLine($"view: {greeting}"); })
                    .Subscribe();
            });
        }
    }
}
