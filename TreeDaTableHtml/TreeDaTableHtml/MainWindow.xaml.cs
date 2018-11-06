
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeDaTableHtml.Model;

namespace TreeDaTableHtml
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly ObservableCollection<Information> _list = new ObservableCollection<Information>();
        private readonly InformationModel _im = new InformationModel();
        public MainWindow()
        {
            Form2 a = new Form2();
            a.Show();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Information> _item1 = new ObservableCollection<Information>();
            _item1.Add(new Information
            {
                StudentName = "Student1",
                StudentAge = 1,
                StudentAddress = "中国1",
                TeacherID = 1,
                TeacherName = "Teacher1",
                
            });
            //TreeList1.ItemsSource = _im.InformationListModel;
            List<Information> items = new List<Information>();
            items.Add(new Information
            {
                StudentName = "Student1" ,
                StudentAge = 1,
                StudentAddress = "中国1",
                TeacherID = 1,
                TeacherName = "Teacher1" ,
                _item= _item1
            });
            items.Add(new Information
            {
                StudentName = "Student2",
                StudentAge = 2,
                StudentAddress = "中国2",
                TeacherID = 2,
                TeacherName = "Teacher2",
                _item = _item1
            });
            GridTab.ItemsSource = items;
        }

        private void departmentTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }

    public class Information
    {
        public  int StudentID { get; set; }
        public int NameID { get; set; }
        public string StudentName { get; set; }

        public int StudentAge { get; set; }
        public string Sex { get; set; }
        public string StudentAddress { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public ObservableCollection<Information> _item = new ObservableCollection<Information>();
        public ObservableCollection<Information> Children
        {
            get { return _item; }
        }
    }
    public class ListName
    {
        public string Name { get; set; }
    }
    public class Informations
    {
        private readonly ObservableCollection<ListName> itemList = new ObservableCollection<ListName>();
        private readonly ObservableCollection<Information> _list = new ObservableCollection<Information>();
        public Informations()
        {
            itemList.Add(new ListName
            {
                Name = "wode"
            });
            int nTeacher;
            for (int i = 0; i < 20; i++)
            {
                nTeacher = i / 5;

                _list.Add(new Information
                {
                    StudentID =i,
                    StudentName = "Student"+i.ToString (),
                    StudentAge=i+1,
                    StudentAddress ="中国",
                    TeacherID = nTeacher,
                    TeacherName= "Teacher" + nTeacher.ToString(),
                    //_item= itemList,
                });
            }
            //ObservableCollection<Information> item = new ObservableCollection<Information>();
            //item.Add(new Information()
            //{
            //    Name = "规格qq",
            //    Sex = "男weqwe",
            //    Address = "wooooo"
            //});
            //Information i1 = new Information();
            //_list.Add(new Information
            //{
            //    Name = "规格",
            //    Sex = "男",
            //    Address = "asf",
            //    item =item 
            //});
            //_list.Add(new Information
            //{
            //    Name = "里",
            //    Sex = "男",
            //    Address = "asf",
            //    item = item
            //});
            //_list.Add(new Information
            //{
            //    Name = "撒",
            //    Sex = "女",
            //    Address = "asf",
            //    item = item
            //});
            //_list.Add(new Information
            //{
            //    Name = "规格",
            //    Sex = "男",
            //    Address = "asf",
            //    item = item
            //});
        }
        public ObservableCollection<Information> GetInformations()
        {
            return (_list != null) ? _list : null;
        }
    }
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
