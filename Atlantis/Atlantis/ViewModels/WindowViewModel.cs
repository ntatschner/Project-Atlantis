using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Atlantis
{
    /// <summary>
    /// View modal for the main windows frame
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Variable
        /// <summary>
        /// The windows this view model controls 
        /// </summary>
        private Window mWindow;

        #endregion

        #region Public Properties

        public string Test { get; set; } = "Test Binding!";
        

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            mWindow = window;
        }

        #endregion
    }
}
