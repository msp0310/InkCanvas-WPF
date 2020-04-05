using Reactive.Bindings;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Windows.Ink;
using System.Windows.Media;

namespace InkCanvas_WPF
{
    public class MainWindowViewModel
    {
        private static readonly string DrawFileName = "draw.isf";

        public ReactiveProperty<StrokeCollection> Strokes { get; set; }

        public ReactiveCommand LoadCommand { get; set; }
        public ReactiveCommand SaveCommand { get; set; }
        public ReactiveCommand ClearCommand { get; set; }

        public MainWindowViewModel()
        {
            this.Strokes = new ReactiveProperty<StrokeCollection>(new StrokeCollection());

            // Load
            this.LoadCommand = new ReactiveCommand();
            this.LoadCommand.Subscribe(x => Load());

            // Save
            this.SaveCommand = new ReactiveCommand();
            this.SaveCommand.Subscribe(x => Save(Strokes.Value));

            // Clear
            this.ClearCommand = new ReactiveCommand();
            this.ClearCommand.Subscribe(x => Strokes.Value.Clear());
        }

        private void Load()
        {
            using (var stream = new FileStream(DrawFileName, FileMode.Open))
            {
                this.Strokes.Value = new StrokeCollection(stream);
            }
        }

        private void Save(StrokeCollection strokes)
        {
            using (var stream = new FileStream(DrawFileName, FileMode.OpenOrCreate))
            {
                strokes.Save(stream);
            }
        }
    }
}
