namespace TodoList.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Command;

    using Model;

    public partial class MainViewModel 
    {
        /// <summary>
        /// filters "Active" 
        /// </summary>
        public ICommand ActiveCommand => new RelayCommand(this.ActiveCommandExecute);

        private void ActiveCommandExecute()
        {
            this.collection = new ObservableCollection<Item>(this.initCollection.Where(x => !x.IsChecked).ToList());

            this.RaisePropertyChanged(CollectionString);
        }

        /// <summary>
        /// filters "Completed" 
        /// </summary>
        public ICommand CompletedCommand => new RelayCommand(this.CompletedCommandExecute);

        private void CompletedCommandExecute()
        {
            this.collection = new ObservableCollection<Item>(this.initCollection.Where(x => x.IsChecked));

            this.RaisePropertyChanged(CollectionString);
        }

        /// <summary>
        /// filters "All" 
        /// </summary>
        public ICommand AllCommand => new RelayCommand(this.AllCommandExecute);

        private void AllCommandExecute()
        {
            this.ShowCollection();
        }

        /// <summary>
        /// shows items collection
        /// </summary>
        private void ShowCollection()
        {
            this.collection = new ObservableCollection<Item>(this.initCollection);

            this.RaisePropertyChanged(CollectionString);
        }

        /// <summary>
        /// clears completed items
        /// </summary>
        public ICommand ClearCheckedCommand => new RelayCommand(this.ClearCheckedCommandExecute);

        private void ClearCheckedCommandExecute()
        {
            this.initCollection = this.initCollection.Where(x => !x.IsChecked).ToList();
            this.CountCheckedItems();
            this.CountItems();
        }

        /// <summary>
        /// Counts and shows items
        /// </summary>
        private void CountItems()
        {
            this.ShowCollection();
            this.itemsCount = this.initCollection.Count();

            this.RaisePropertyChanged(ItemsCountString);
        }

        /// <summary>
        /// mark item as complete
        /// </summary>
        public ICommand OnCheckedCommand => new RelayCommand(this.OnCheckedCommandExecute);

        private void OnCheckedCommandExecute()
        {
            if (this.initCollection.All(x => x.IsChecked))
            {
                this.isCheckAll = true;
                this.RaisePropertyChanged(IsCheckAllString);
            }
            else if (this.isCheckAll)
            {
                this.isCheckAll = false;
                this.RaisePropertyChanged(IsCheckAllString);
            }


            this.CountCheckedItems();
        }

        /// <summary>
        /// counts marked items
        /// </summary>
        private void CountCheckedItems()
        {
            var count = this.initCollection.Count(x => x.IsChecked);
            if (count == 0) this.checkedCount = string.Empty;
            else this.checkedCount = $"Clear Completed ({count})";

            this.PluralizeString();

            this.RaisePropertyChanged(CheckedCountString);
        }
        /// <summary>
        /// pluralizes the items 
        /// </summary>
        private void PluralizeString()
        {
            var count = this.initCollection.Count(x => !x.IsChecked);

            if (count == 1)
            {
                this.pluralizeLeavingCount = "item left";
            }
            else
            {
                this.pluralizeLeavingCount = "items left";
            }

            this.leavingCount = count;

            this.RaisePropertyChanged(LeavingCountString);
            this.RaisePropertyChanged(PluralizeLeavingCountString);
        }

        /// <summary>
        /// deletes item
        /// </summary>
        public ICommand DeleteItemCommand => new RelayCommand<object>(this.DeleteItemCommandExecute);

        private void DeleteItemCommandExecute(object obj)
        {
            this.DeleteItems(obj);
        }

        private void DeleteItems(object obj)
        {
            this.initCollection = this.initCollection.Where(x => x != obj as Item).ToList();

            this.CountItems();
            this.CountCheckedItems();
            if (this.initCollection.All(x => x.IsChecked))
            {
                this.isCheckAll = true;
                this.RaisePropertyChanged(IsCheckAllString);
            }

        }

        /// <summary>
        /// marks all as complete
        /// </summary>
        public ICommand CheckAllCommand => new RelayCommand(this.CheckAllCommandExecute);

        private void CheckAllCommandExecute()
        {
            if (this.initCollection.All(x => x.IsChecked))
            {

                foreach (var el in this.initCollection)
                {
                    el.IsChecked = false;
                }
            }
            else
            {
                foreach (var el in this.initCollection)
                {
                    if (!el.IsChecked)
                    {
                        el.IsChecked = true;
                    }

                }
            }

            this.CountCheckedItems();
        }

        /// <summary>
        /// turns on edit mode
        /// </summary>
        public ICommand DoubleClickCommand => new RelayCommand<object>(this.DoubleClickCommandExecute);

        /// <summary>
        /// text before DoubleClick
        /// </summary>
        private string oldText;

        private void DoubleClickCommandExecute(object obj)
        {
            var item = obj as Item;
            this.oldText = item.Text;
            item.IsReadOnly = false;
            this.RaisePropertyChanged(CollectionString);
        }

        /// <summary>
        /// adds new item
        /// </summary>
        public ICommand EnterCommand => new RelayCommand(this.EnterCommandExecute);

        private bool CheckEmptyItem(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;

            if(string.IsNullOrEmpty(text.Replace(" ", ""))) return true;

            return false;
        }
        private void EnterCommandExecute()
        {
            if (this.CheckEmptyItem(this.text)) return;

            this.initCollection.Add(new Item(this.text));
            this.text = string.Empty;
            this.RaisePropertyChanged(TextString);
            this.CountItems();
            this.PluralizeString();
            this.CountCheckedItems();

            if (this.isCheckAll)
            {
                this.isCheckAll = false;
                this.RaisePropertyChanged(IsCheckAllString);
            }
        }

        
        /// <summary>
        /// edit items and turns off edit mode
        /// </summary>
        public ICommand EditCommand => new RelayCommand<object>(this.EditCommandExecute);

        private void EditCommandExecute(object obj)
        {
            var item = obj as Item;

            if (this.CheckEmptyItem(item.Text ))
            {
                this.DeleteItems(obj);
                this.CountCheckedItems();

                return;
            }

            

            item.IsReadOnly = true;
        }

        /// <summary>
        /// cancels changes and turns off edit mode
        /// </summary>
        public ICommand EscapeCommand => new RelayCommand<object>(this.EscapeCommandExecute);

        private void EscapeCommandExecute(object obj)
        {
            var item = obj as Item;
            item.Text = this.oldText;
            item.IsReadOnly = true;
        }
    }
}