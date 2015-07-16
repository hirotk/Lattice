using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lattice {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        int WidthMax, HeightMax;
        int Width, Height;

        const int L = 30; // The length of unit cell
        int Ox, Oy;  // Origin

        private void DrawButton_Click(object sender, RoutedEventArgs e) {
            cvsMain.Children.Clear();

            this.WidthMax = (int)cvsMain.RenderSize.Width;
            this.HeightMax = (int)cvsMain.RenderSize.Height;

            this.Ox = L;
            this.Oy = L;

            LatticeType latticeType;
            SearchType searchType;
            try {
                int h = int.Parse(tbHeight.Text) * L;
                if (h < this.Oy || this.HeightMax < this.Oy + h) {
                    throw new Exception("Height is out of range");
                }
                this.Height = h;

                int w = int.Parse(tbWidth.Text) * L;
                if (w < this.Ox || this.WidthMax < this.Ox + w) {
                    throw new Exception("Width is out of range");
                }
                this.Width = w;

                var item = cbLatticeType.SelectedItem as ComboBoxItem;
                switch (item.Content.ToString()) {
                    case "Square":
                        goto default;
                    case "Triangle":
                        latticeType = LatticeType.Triangle;
                        break;
                    default:
                        latticeType = LatticeType.Square;
                        break;
                }

                item = cbSearchType.SelectedItem as ComboBoxItem;
                switch (item.Content.ToString()) {
                    case "DFS":
                        goto default;
                    case "BFS":
                        searchType = SearchType.BFS;
                        break;
                    default:
                        searchType = SearchType.DFS;
                        break;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }

            this.drawLattice(Ox, Oy, Width/L, Height/L, L, latticeType);

            var searcher = new Searcher(Width/L, Height/L);
            int[,] orders = searcher.Search(0, 0, latticeType, searchType);

            int xOffset = 4, yOffset = 8;

            for (int j = 0; j < Height/L; j++) {
                for (int i = 0; i < Width/L; i++) {
                    this.drawText(L*(i+1) - xOffset, L*(j+1) - yOffset, 
                        string.Format("{0}", orders[j,i]));
                }
            }
        }

        void drawLine(double sx, double sy, double gx, double gy,
           double strokeThickness=1, double opacity= 1, Brush stroke=null) {
            var srk = stroke ?? Brushes.Black;
            
            var line = new Line {
                X1 = sx,
                Y1 = sy,
                X2 = gx,
                Y2 = gy,
                Stroke = stroke,
                Opacity = opacity,
                StrokeThickness = strokeThickness
            };
            cvsMain.Children.Add(line);
        }

        void drawRect(double x, double y, double width, double height, 
           double strokeThickness=1, double opacity= 1, Brush stroke=null) {
            var srk = stroke ?? Brushes.Black;

            var rect = new Rectangle {
                Width = width,
                Height = height, 
                Stroke =  srk,
                StrokeThickness = strokeThickness, 
                Opacity = opacity
            };
            
            cvsMain.AddElementAt(rect, x, y);
        }

        void drawLattice(double x, double y, int width, int height, int leng, LatticeType type) {
            if (width == 1 && height > 1) {
                this.drawLine(x, y, x, y + leng*(height-1),
                    strokeThickness: 0.5, opacity: 0.75, stroke: Brushes.Navy);
                return;
            }

            if (width > 1 && height == 1) {
                this.drawLine(x, y, x + leng*(width-1), y,
                    strokeThickness: 0.5, opacity: 0.75, stroke: Brushes.Navy);
                return;
            }

            for (int j = 0; j < height-1; j++) {
                for (int i = 0; i < width-1; i++) {
                    double sx = x + i*leng, sy = y + j*leng;
                    this.drawRect(sx, sy, width:leng, height:leng, 
                        strokeThickness:0.5, opacity:0.75, stroke:Brushes.Navy);
                    if (type == LatticeType.Triangle) {
                        this.drawLine(sx, sy, sx+leng, sy+leng,
                            strokeThickness: 0.5, opacity: 0.75, stroke: Brushes.Navy);
                    }
                }
            }
        }

        void drawText(double x, double y, string s) {
            var label = new Label {Content = s};
            cvsMain.AddElementAt(label, x, y);
        }
    }

    public static class CanvasEx {
        public static void SetX(this Canvas cvs, UIElement elm, double x) {
            Canvas.SetLeft(elm, x);
        }
        public static void SetY(this Canvas cvs, UIElement elm, double y) {
            Canvas.SetTop(elm, y);
        }
        public static void SetElementPos(this Canvas cvs, UIElement elm, double x, double y) {
            Canvas.SetLeft(elm, x);
            Canvas.SetTop(elm, y);
        }
        public static void AddElementAt(this Canvas cvs, UIElement elm, double x=0, double y=0) {
            cvs.Children.Add(elm);
            cvs.SetElementPos(elm, x, y);
        }
    }
}
