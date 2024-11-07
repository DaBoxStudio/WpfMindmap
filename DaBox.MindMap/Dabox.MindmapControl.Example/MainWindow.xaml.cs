using DaBox.Framework.Helpers;
using DaBox.LSB.Data;
using System.IO;
using System.Windows;

namespace DaBox.MindmapControl.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region [ Fields ]

        /// <summary>
        /// Identifies the <see cref="RootNode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RootNodeProperty = DependencyProperty.Register("RootNode", typeof(Root), typeof(MainWindow));

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += this.MainWindow_Loaded;
        }

        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Gets or sets the RootNode value.
        /// </summary>
        /// <value>
        /// The RootNode value.
        /// </value>
        public Root RootNode
        {
            get
            {
                return (Root)this.GetValue(MainWindow.RootNodeProperty);
            }

            set
            {
                this.SetValue(MainWindow.RootNodeProperty, value);
            }
        }

        #endregion

        #region [ Public Methods ]
        #endregion

        #region [ Protected Methods ]

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Handles the Loaded event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Mock data
            string fileName = "root.json";
            string fileContent = File.ReadAllText(fileName);
            Root root = DataStringHelper.DeserializeObject<Root>(fileContent);
            this.RootNode = root;
        }

        #endregion
    }
}
