namespace TodoList.Model
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media.TextFormatting;

    public class Item : INotifyPropertyChanged
    {
        public Item(bool isChecked, string text)
        {
            this.IsChecked = isChecked;
            this.Text = text;
          //  this.IsReadOnly = true;
        }

        public Item(string text)
        {
            this.Text = text;
        }

        public Item()
        {
        }

        private const string IsCheckedString = "IsChecked";

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
                this.OnPropertyChanged(IsCheckedString);
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
                this.OnPropertyChanged(TextString);
            }
        }

        private const string IsReadOnlyString = "IsReadOnly";
        private bool isReadOnly = true;
        /// <summary>
        /// for edit mode
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return this.isReadOnly;
            }
            set
            {
                this.isReadOnly = value;
                this.OnPropertyChanged(IsReadOnlyString);
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}