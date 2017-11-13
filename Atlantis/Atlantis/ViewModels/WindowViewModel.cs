using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Atlantis
{
    /// <summary>
    /// View model for the main windows frame
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member
        /// <summary>
        /// The windows this view model controls 
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The Margin around the window to allow for drop shadow
        /// </summary>
        private int mOuterMarginSize = 5;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 5;

        #endregion

        #region Public Properties

        /// <summary>
        /// This is the size of the border around the edges of the main window
        /// </summary>
        public int ResizeBorder { get; set; } = 3;

        /// <summary>
        /// The Size of the resize border around the window, taking into account the outermargin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The outer margin size of the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The outer margin Thickness size of the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize);  } }

        /// <summary>
        /// The windows radius at the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// The windows radius at the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The height of the title bar option of the windows
        /// </summary>
        public int TitleHeight { get; set; } = 35;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            //listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                //fire off events for all properties that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };
        }

        #endregion
    }
}
