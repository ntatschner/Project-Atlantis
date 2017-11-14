using Atlantis.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        /// The Minimum Width the window can go
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 500;

        /// <summary>
        /// The Minimum Height the window can go
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// This is the size of the border around the edges of the main window
        /// </summary>
        public int ResizeBorder { get; set; } = 3;

        /// <summary>
        /// The Size of the resize border around the window, taking into account the outermargin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The Size of the resize border around the window, taking into account the outermargin
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }

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

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Setup;

        #endregion


        #region Commands 

        /// <summary>
        /// The command to Minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }
        /// <summary>
        /// The command to Maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }
        /// <summary>
        /// The command to CLose the window
        /// </summary>
        public ICommand CloseCommand { get; set; }
        /// <summary>
        /// The command to Menu the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

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

            // Create Commands

            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix Window resize issue
            var resizer = new WindowResizer(mWindow);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relitive to the window
            var position = Mouse.GetPosition(mWindow);
            // Add the windows position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion


    }
}
