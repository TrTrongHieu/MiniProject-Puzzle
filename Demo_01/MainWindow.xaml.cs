using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;

namespace Demo_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region khai báo các biến toàn cục phục vụ cho các hàm thành phần
        //Khai báo chơi n * n and khoảng cách giữa các ô bị cắt.
        private static int n = 2, distance = 0;

        //Tạo mảng 2 chiều chứa các ảnh sau khi cắt.
        List<Image> _samples = new List<Image>();
        Image[,] _images = new Image[n, n];
        int shareSizeWidth;
        int shareSizeHeight;

        //Đặt cờ hiệu khi chương trình bắt đầu chạy.
        bool _isStart = false;
        int _spaceI;
        int _spaceJ;

        bool _isDragging = false;

        Point _lastPos;
        //tọa độ vị trí sau khi move
        int _lastI;
        int _lastJ;
        bool loaded = false; //
        int counter = 2;

        #endregion

        /// <summary>
        /// Window_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "N Puzzle Game";
            #region Chia Hinh Anh Demo
            //var actualwidth = ActualWidth;
            //var actualheight = ActualHeight;

            //var shareWidth = (((actualwidth) / 5) * 3) / n;
            //shareWidth -= distance * n;
            // Title = shareWidth + " " + actualwidth + "  " + actualheight;
            //var lastfile = ConfigurationManager.AppSettings["last_file"];
            ////MessageBox.Show(lastfile);

            //var screem = new OpenFileDialog();
            //screem.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            //if (screem.ShowDialog() == true)
            //{
            //    var filename = screem.FileName;
            //    var OreginalImages = new BitmapImage(new Uri(filename));
            //    var image = new Image()
            //    {
            //        Source = OreginalImages
            //    };
            //    dashboard.Children.Add(image);
            //    #region Chia Hinh Anh 

            //    int width = OreginalImages.PixelWidth / 3;
            //    int height = OreginalImages.PixelHeight / 3;
            //    //Title = width + "  " + height;
            //    List<Image> images = new List<Image>();
            //    var newHight = shareWidth * height / width;
            //    for(int i = 0; i < 3; i++)
            //    {
            //        for(int j = 0; j < 3; j++)
            //        {
            //            var croped = new CroppedBitmap(
            //                OreginalImages, new Int32Rect(i * width, j * height, width, height));

            //            var img = new Image()
            //            {
            //                Source = croped,
            //                Width = shareWidth,
            //                Height = newHight,
            //                Tag = new Tuple<int, int>(i, j)
            //            };
            //            images.Add(img);
            //        }
            //    }

            //    var indices = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8};
            //    var rd = new Random();
            //    for (int i = 0; i < 3; i++)
            //    {
            //        for(int j = 0; j < 3; j++)
            //        {
            //            if(!(i == 2 && j == 2))
            //            {
            //                int index = rd.Next(indices.Count);
            //            var img = images[indices[index]];
            //            board.Children.Add(img);

            //           // var tag = img.Tag as Tuple<int, int>;
            //            Canvas.SetLeft(img, i * (images[index].Width + distance));
            //            Canvas.SetTop(img, j * (images[index].Height + distance));

            //            indices.RemoveAt(index);
            //            }
            //        }
            //    }


            //    #endregion

            //}    


            //var lastwidth = int.Parse(ConfigurationManager.AppSettings["last_width"]);
            //var lastwidth = App.LastWidth;
            //this.Width = lastwidth;

            //var lastUser = ConfigurationManager.AppSettings["last_name"];
            //this.Name = lastUser;

            loaded = true;
            //var username = App.LastUser;
            //this.Title = "Wecom Name" + username;
            #endregion
        }

        /// <summary>
        /// Window_SizeChanged
        /// </summary>

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (loaded)
            {
                var newwidth = e.NewSize.Width;
                App.LastWidth = (int)newwidth;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isStart)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_images[i, j] != null)
                        {

                        }
                    }
                }
            }
        }

        /// <summary>
        /// chooseImageButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseImageButton_Click(object sender, RoutedEventArgs e)
        {

            var actualwidth = ActualWidth;
            var actualheight = ActualHeight;
            var shareWidth = (int)((actualwidth - 200) / n) - 1;
            shareWidth -= distance * n;
            shareSizeWidth = shareWidth;
            //Title = shareWidth + " " + actualwidth + "  " + actualheight;
            var screem = new OpenFileDialog();
            screem.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            if (screem.ShowDialog() == true)
            {
                var filename = screem.FileName;
                var OreginalImages = new BitmapImage(new Uri(filename));
                //Ảnh cho người chơi coi để rap.
                sampleImage.Source = OreginalImages;
                #region Chia Hinh Anh

                int width = OreginalImages.PixelWidth / n;
                int height = OreginalImages.PixelHeight / n;
                //Title = width + "  " + height;
                //List<Image> images = new List<Image>();
                var newHight = shareWidth * height / width;
                shareSizeHeight = newHight;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        var croped = new CroppedBitmap(
                            OreginalImages, new Int32Rect(i * width, j * height, width, height));

                        var imgPart = new Image()
                        {
                            Source = croped,
                            Width = shareWidth,
                            Height = newHight,
                            Tag = new Tuple<int, int>(i, j)
                        };
                        imgPart.MouseLeftButtonDown += mouseLeftButtonDown;
                        imgPart.MouseLeftButtonUp += mouseLeftButtonUp;
                        _samples.Add(imgPart);
                    }
                }
                //Image[,] _images = new Image[n, n];
                _samples.RemoveAt(_samples.Count - 1);
                var rng = new Random();
                _spaceI = rng.Next(2);
                _spaceJ = rng.Next(2);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!(i == _spaceI && j == _spaceJ))
                        {
                            int index = rng.Next(_samples.Count);
                            _images[i, j] = _samples[index];
                            mainContainer.Children.Add(_images[i, j]);

                            // var tag = img.Tag as Tuple<int, int>;
                            Canvas.SetLeft(_images[i, j], i * (_images[i, j].Width + distance));
                            Canvas.SetTop(_images[i, j], j * (_images[i, j].Height + distance));
                            _samples.RemoveAt(index);
                        }
                    }
                }
                #endregion
                startButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// mouseLeftButtonDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isStart)
            {
                _lastPos = e.GetPosition(this);
                _lastI = (int)(_lastPos.X / shareSizeWidth);
                _lastJ = (int)(_lastPos.Y / shareSizeHeight);
                // Title = shareSizeWidth + "+" + shareSizeHeight + "+" + _lastPos.X + "+" + _lastPos.Y + "+" + _lastI + "+" + _lastJ;
                if (
                    ((_lastI == _spaceI + 1 || _lastI == _spaceI - 1) && _lastJ == _spaceJ)
                    || ((_lastJ == _spaceJ + 1 || _lastJ == _spaceJ - 1) && _lastI == _spaceI)
                   )
                {
                    _isDragging = true;
                }
            }
        }

        /// <summary>
        /// mouseLeftButtonUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (_isDragging && _isStart)
            {
                var curPos = e.GetPosition(this);
                if (curPos.X > n * shareSizeWidth || curPos.Y > n * shareSizeHeight)
                {
                    curPos.X = (_lastPos.X / shareSizeWidth) * shareSizeWidth;
                    curPos.Y = (_lastPos.Y / shareSizeHeight) * shareSizeHeight;
                }
                //x, y: lấy toạ độ để set image
                var x = ((int)curPos.X / shareSizeWidth) * shareSizeWidth;
                var y = ((int)curPos.Y / shareSizeWidth) * shareSizeHeight;
                // Title = x + " " + y + " " + shareSizeWidth + "+" + shareSizeHeight + "+" + _lastPos.X + "+" + _lastPos.Y + "+" + curPos.X + "+" + curPos.Y + "+" + _lastI + "+" + _lastJ;

                if ((x <= ((n - 1) * shareSizeWidth) && y <= ((n - 1) * shareSizeHeight)) && (x / shareSizeWidth == _spaceI && y / shareSizeHeight == _spaceJ))
                {

                    Canvas.SetLeft(_images[_lastI, _lastJ], x);
                    Canvas.SetTop(_images[_lastI, _lastJ], y);


                    _images[x / shareSizeWidth, y / shareSizeHeight] = _images[_lastI, _lastJ];
                    _images[_lastI, _lastJ] = null;

                    _spaceI = _lastI;
                    _spaceJ = _lastJ;


                }
                else
                {
                    Canvas.SetLeft(_images[_lastI, _lastJ], _lastI * shareSizeWidth);
                    Canvas.SetTop(_images[_lastI, _lastJ], _lastJ * shareSizeHeight);
                }
            }
            _isDragging = false;

            //Title = $"{_lastI},{_lastJ}";
        }

        /// <summary>
        /// mouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_isDragging && _isStart)
            {
                var curPos = e.GetPosition(this);
                if (curPos.X > n * shareSizeWidth || curPos.Y > n * shareSizeHeight)
                {
                    curPos.X = _lastPos.X;
                    curPos.Y = _lastPos.Y;
                }
                var dx = curPos.X - _lastPos.X;
                var dy = curPos.Y - _lastPos.Y;

                // Title = shareSizeWidth + "+" + shareSizeHeight + "+" + _lastPos.X + "+" + _lastPos.Y + "+" + curPos.X + "+" + curPos.Y + "+" + _lastI + "+" + _lastJ;
                var oldLeft = Canvas.GetLeft(_images[_lastI, _lastJ]);
                var oldTop = Canvas.GetTop(_images[_lastI, _lastJ]);

                Canvas.SetLeft(_images[_lastI, _lastJ], oldLeft + dx);
                Canvas.SetTop(_images[_lastI, _lastJ], oldTop + dy);

                _lastPos = curPos;
            }
        }

        /// <summary>
        /// startButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            //counter = 30;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            startButton.IsEnabled = false;
            _isStart = true;
        }

        /// <summary>
        /// Timer_Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            if (IsWinner())
            {
                //System.Windows.MessageBox.Show("Win!", MessageBoxButton.OK);
                MessageBoxResult result = MessageBox.Show("Congratulation. You won...", "Notification", MessageBoxButton.OK);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        Environment.Exit(0);
                        break;
                }
                timer.Stop();
            }
            clockLabel.Content = counter--;
            if (counter < 0)
            {
                timer.Stop();
                System.Windows.MessageBox.Show("Time out!");
                System.Windows.MessageBox.Show("Lose!");
                MessageBoxResult result = MessageBox.Show("You are very good but I'm sorry. You lose. Wish you luck next time!!!", "Notification", MessageBoxButton.OK);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        Environment.Exit(0);
                        break;
                }
            }

        }

        /// <summary>
        /// IsWinner
        /// </summary>
        /// <returns></returns>
        bool IsWinner()
        {

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (_images[i, j] != null)
                    {
                        var tag = _images[i, j].Tag as Tuple<int, int>;

                        if (tag.Item1 != i || tag.Item2 != j)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ButtonTop_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBottom_Click(object sender, RoutedEventArgs e)
        {
            if (_isStart)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_images[i, j] == null && j > 0)
                        {
                            _images[i, j] = _images[i, j - 1];
                            Canvas.SetLeft(_images[i, j], i * shareSizeWidth);
                            Canvas.SetTop(_images[i, j], j * shareSizeHeight);
                            _images[i, j - 1] = null;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ButtonBottom_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTop_Click(object sender, RoutedEventArgs e)
        {
            if (_isStart)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_images[i, j] == null && j < (n - 1))
                        {
                            _images[i, j] = _images[i, j + 1];
                            Canvas.SetLeft(_images[i, j], i * shareSizeWidth);
                            Canvas.SetTop(_images[i, j], j * shareSizeHeight);
                            _images[i, j + 1] = null;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ButtonLeft_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            if (_isStart)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_images[i, j] == null && i > 0)
                        {
                            _images[i, j] = _images[i - 1, j];
                            Canvas.SetLeft(_images[i, j], i * shareSizeWidth);
                            Canvas.SetTop(_images[i, j], j * shareSizeHeight);
                            _images[i - 1, j] = null;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ButtonRight_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            if (_isStart)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_images[i, j] == null && i < (n - 1))
                        {
                            _images[i, j] = _images[i + 1, j];
                            Canvas.SetLeft(_images[i, j], i * shareSizeWidth);
                            Canvas.SetTop(_images[i, j], j * shareSizeHeight);
                            _images[i + 1, j] = null;
                            return;
                        }
                    }
                }
            }
        }


    }
}
