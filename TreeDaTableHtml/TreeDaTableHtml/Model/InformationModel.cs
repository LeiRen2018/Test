using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeDaTableHtml.Model
{
    public  class InformationModel:PropertyChangedBase
    {
        private  ObservableCollection<Information> _InformationList = new ObservableCollection<Information>();
        private readonly Informations _Informations = new Informations();
        public ObservableCollection<Information> InformationListModel
        {
            get { return _InformationList; }
            set
            {
                _InformationList = value;
                OnPropertyChanged("InformationModel");
            }
        }
        public InformationModel()
        {
            _InformationList = _Informations.GetInformations();
        }
    }
}
