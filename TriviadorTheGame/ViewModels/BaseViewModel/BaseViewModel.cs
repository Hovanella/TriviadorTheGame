using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace TriviadorTheGame.ViewModels.BaseViewModel
{


    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// call this to fire <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="prop"></param>

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}