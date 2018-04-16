namespace TodoList.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.TextFormatting;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Model;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        private IEnumerable<Item> collection;
        private IList<Item> initCollection;

        private const string CollectionString = "Collection";
        public IEnumerable<Item> Collection => this.collection;

        private const string CheckedCountString = "CheckedCount";
        private string checkedCount;
        public string CheckedCount => this.checkedCount;

        private const string ItemsCountString = "ItemsCount";
        private int itemsCount;
        public int ItemsCount => this.itemsCount;

        private const string LeavingCountString = "LeavingCount";
        private int leavingCount;
        public int LeavingCount => this.leavingCount;

        private const string PluralizeLeavingCountString = "PluralizeLeavingCount";

        private string pluralizeLeavingCount;
        public string PluralizeLeavingCount => this.pluralizeLeavingCount;



        //private bool isTextBoxReadOnly;
        //public bool IsTextBoxReadOnly => this.isTextBoxReadOnly;

        private const string IsCheckAllString = "IsCheckAll";
        private bool isCheckAll;
        public bool IsCheckAll
        {
            get
            {
                return this.isCheckAll;
            }
            set
            {
                this.isCheckAll = value;
            }
        }

        private const string TextString = "Text";
        private string text;
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        private IDataService dataService;
        public MainViewModel(IDataService dataService)
        {
            this.dataService = dataService;
            this.initCollection = new List<Item>()
            {
            };

            this.collection = new ObservableCollection<Item>(this.initCollection);
            //   this.isTextBoxReadOnly = true;
        }

        
    }
}