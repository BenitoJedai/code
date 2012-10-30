using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AssetsLibraryDesignerExperiment.Forms.Library.Components
{
    public class MyButton : System.Windows.Forms.Button, INotifyPropertyChanged
    {
        int _bExternalOn = 0;
        [System.ComponentModel.Bindable(true)]
        public int ExternalCmd
        {
            set
            {
                _bExternalOn = value;
                RaisePropertyChanged("ExternalCmd");
            }
            get
            {
                return _bExternalOn;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
