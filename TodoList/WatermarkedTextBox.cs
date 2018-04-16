namespace TodoList
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// custom control to support prompt (watermark)
    /// </summary>
    public class WatermarkedTextBox : TextBox
    {
        #region Fields

        private const string DefaultWatermark = "What needs to be done?";

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register("WatermarkText", typeof(string), typeof(WatermarkedTextBox), new UIPropertyMetadata(string.Empty, OnWatermarkTextChanged));

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkedTextBox"/> class with default watermark text.
        /// </summary>
        public WatermarkedTextBox()
            : this(DefaultWatermark)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkedTextBox"/> class.
        /// </summary>
        /// <param name="watermark">The watermark to show when value is <c>null</c> or empty.</param>
        public WatermarkedTextBox(string watermark)
        {
            this.WatermarkText = watermark;
        }

        #endregion

        #region Properties

        public string WatermarkText
        {
            get { return (string)this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        #endregion

        #region Methods

        public static void OnWatermarkTextChanged(DependencyObject box, DependencyPropertyChangedEventArgs e)
        {
            //Add changed functionality here
        }
        #endregion
    }

}
