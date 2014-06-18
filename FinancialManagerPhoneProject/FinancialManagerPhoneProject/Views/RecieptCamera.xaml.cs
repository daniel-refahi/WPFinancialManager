

using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace FinancialManagerPhoneProject.Views
{
    public partial class RecieptCamera : PhoneApplicationPage
    {

        #region Zooming
        //const double MaxScale = 10;

        //double _scale = 1.0;
        //double _minScale;
        //double _coercedScale;
        //double _originalScale;

        //Size _viewportSize;
        //bool _pinching;
        //Point _screenMidpoint;
        //Point _relativeMidpoint;

        //BitmapImage _bitmap;

        //void viewport_ViewportChanged(object sender, System.Windows.Controls.Primitives.ViewportChangedEventArgs e)
        //{
        //    Size newSize = new Size(viewport.Viewport.Width, viewport.Viewport.Height);
        //    if (newSize != _viewportSize)
        //    {
        //        _viewportSize = newSize;
        //        CoerceScale(true);
        //        ResizeImage(false);
        //    }
        //}

        //void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        //{
        //    _pinching = false;
        //    _originalScale = _scale;
        //}

        //void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        //{
        //    if (e.PinchManipulation != null)
        //    {
        //        e.Handled = true;

        //        if (!_pinching)
        //        {
        //            _pinching = true;
        //            Point center = e.PinchManipulation.Original.Center;
        //            _relativeMidpoint = new Point(center.X / __Image.ActualWidth, center.Y / __Image.ActualHeight);

        //            var xform = __Image.TransformToVisual(viewport);
        //            _screenMidpoint = xform.Transform(center);
        //        }

        //        _scale = _originalScale * e.PinchManipulation.CumulativeScale;

        //        CoerceScale(false);
        //        ResizeImage(false);
        //    }
        //    else if (_pinching)
        //    {
        //        _pinching = false;
        //        _originalScale = _scale = _coercedScale;
        //    }
        //}

        //void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        //{
        //    _pinching = false;
        //    _scale = _coercedScale;
        //}

        //void ResizeImage(bool center)
        //{            
        //    if (_coercedScale != 0 && _bitmap != null)
        //    {
        //        double newWidth = canvas.Width = Math.Round(_bitmap.PixelWidth * _coercedScale);
        //        double newHeight = canvas.Height = Math.Round(_bitmap.PixelHeight * _coercedScale)+200;

        //        xform.ScaleX = xform.ScaleY = _coercedScale;

        //        viewport.Bounds = new Rect(0, 0, newWidth, newHeight);

        //        if (center)
        //        {
        //            double firstPoint = Math.Round((newWidth - viewport.ActualWidth) / 2);
        //            double secondPoint = Math.Round((newHeight - viewport.ActualHeight) / 2);
        //            viewport.SetViewportOrigin(
        //                new Point(firstPoint, secondPoint));
        //        }
        //        else
        //        {
        //            Point newImgMid = new Point(newWidth * _relativeMidpoint.X, newHeight * _relativeMidpoint.Y);
        //            Point origin = new Point(newImgMid.X - _screenMidpoint.X, newImgMid.Y - _screenMidpoint.Y); 
        //            viewport.SetViewportOrigin(origin);
        //        }
        //    }
        //}

        //void CoerceScale(bool recompute)
        //{
        //    if (recompute && _bitmap != null && viewport != null)
        //    {
        //        double minX = viewport.ActualWidth / _bitmap.PixelWidth + 0.05;
        //        double minY = viewport.ActualHeight / _bitmap.PixelHeight + 0.05;

        //        _minScale = Math.Min(minX, minY);
        //    }

        //    _coercedScale = Math.Min(MaxScale, Math.Max(_scale, _minScale));

        //}
        #endregion

        public RecieptCamera()
        {
            InitializeComponent();
            this.Loaded += RecieptCamera_Loaded;
        }

        void RecieptCamera_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?caller=recieptcamera", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string imageName = string.Empty;
            byte[] data;
            NavigationContext.QueryString.TryGetValue("image", out imageName);
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile("Receipts/"+imageName, FileMode.Open, FileAccess.Read))
                    {
                        data = new byte[isfs.Length];
                        isfs.Read(data, 0, data.Length);
                        isfs.Close();
                    }
                }



                MemoryStream stream = new MemoryStream(data);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                image.Source = bitmapImage;


                #region Zooming
                //__Image.Source = bi;

                //_bitmap = (BitmapImage)__Image.Source;
                //_scale = 0;
                //CoerceScale(true);
                //_scale = _coercedScale;

                //ResizeImage(true);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, Couldn't get the Receipt image!");
            }
        }

        private bool SaveImageToPhotoHub(WriteableBitmap bmp)
        {

            using (var mediaLibrary = new MediaLibrary())
            {
                using (var stream = new MemoryStream())
                {
                    var fileName = string.Format("Gs{0}.jpg", Guid.NewGuid());
                    bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
                    stream.Seek(0, SeekOrigin.Begin);
                    var picture = mediaLibrary.SavePicture(fileName, stream);
                    if (picture.Name.Contains(fileName)) return true;
                }
            }
            return false;
        }

        private void __btSave_Click(object sender, EventArgs e)
        {
            __LoadingLayer.Visibility = System.Windows.Visibility.Visible;
            WriteableBitmap bmp = new WriteableBitmap(image, null);
            bool result = false;
            Task backgroundTask = Task.Factory.StartNew(() =>
            {
                result = SaveImageToPhotoHub(bmp);
            });

            Task UITask = backgroundTask.ContinueWith((t) =>
            {
                __LoadingLayer.Visibility = System.Windows.Visibility.Collapsed;
                if (result)
                {
                    MessageBox.Show("Your receipt Saved in to your image gallery.", "Information", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Error : Your receipt could not be saved in your gallery!", "Information", MessageBoxButton.OK);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());

        }
        
    }
}