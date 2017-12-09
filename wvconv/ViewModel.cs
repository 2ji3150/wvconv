using Reactive.Bindings;
using System.Linq;
using System.Reactive.Linq;

namespace wvconv {
    public class ViewModel {
        public ReactiveProperty<int> Index { get; } = new ReactiveProperty<int>();
        public ReactiveProperty<bool> Idle { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Cancelable { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<double> Pvalue { get; } = new ReactiveProperty<double>();
        public ReactiveProperty<string> Ptext { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> DeltaText { get; } = new ReactiveProperty<string>();

        public ViewModel() {
            Idle.Value = true;
            Cancelable = Idle.Select(x => !x).ToReactiveProperty();
        }
    }
}
