using Esri.ArcGISRuntime.Controls;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArcGISApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyMapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;
            var graphicsLayer = new Esri.ArcGISRuntime.Layers.GraphicsLayer();
            graphicsLayer.ID = "MyGraphics";
            MyMap.Layers.Add(graphicsLayer);

            // define a line symbol (for the fill outline)
            var lineSymbol = new Esri.ArcGISRuntime.Symbology.SimpleLineSymbol();
            lineSymbol.Color = Colors.ForestGreen;
            lineSymbol.Style = Esri.ArcGISRuntime.Symbology.SimpleLineStyle.Dot;
            lineSymbol.Width = 2;

            // define a polygon (fill) symbol     
            var polySymbol = new Esri.ArcGISRuntime.Symbology.SimpleFillSymbol();
            polySymbol.Style = Esri.ArcGISRuntime.Symbology.SimpleFillStyle.DiagonalCross;
            polySymbol.Color = Colors.Crimson;

            polySymbol.Outline = lineSymbol; // apply the outline symbol

            var Coords1 = new Esri.ArcGISRuntime.Geometry.PointCollection();
            Coords1.Add(5577200, 7019500);
            Coords1.Add(5577300, 7019300);
            Coords1.Add(5577300, 7019400);
            Coords1.Add(5577400, 7019400);

            var Coords2 = new Esri.ArcGISRuntime.Geometry.PointCollection();
            Coords2.Add(5577200, 7019500);
            Coords2.Add(5577200, 7019500);
            Coords2.Add(5577200, 7019500);
            Coords2.Add(5577200, 7019500);

            var complexPolygon = new Esri.ArcGISRuntime.Geometry.Polygon(Coords1);
            var polygonBldr = new Esri.ArcGISRuntime.Geometry.PolygonBuilder(complexPolygon);
            polygonBldr.Parts.AddPart(Coords2);

            complexPolygon = polygonBldr.ToGeometry();

            var polyGraphic = new Esri.ArcGISRuntime.Layers.Graphic();
            polyGraphic.Geometry = complexPolygon;
            polyGraphic.Symbol = polySymbol;

            // add the graphic to the graphics layer; zoom to it
            graphicsLayer.Graphics.Add(polyGraphic);
            //MyMapView.SetViewAsync(complexPolygon.Extent);

            // use the MapView's Editor to get polygon geometry from the user
            //var poly = await MyMapView.Editor.RequestShapeAsync(Esri.ArcGISRuntime.Controls.DrawShape.Polygon,
            //    polySymbol, null);

            // create a new graphic; set the Geometry and Symbol
            //var polyGraphic = new Esri.ArcGISRuntime.Layers.Graphic();
            // polyGraphic.Geometry = poly;
            //polyGraphic.Symbol = polySymbol;

            // add the graphic to the graphics layer
            // graphicsLayer.Graphics.Add(polyGraphic);
            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }
        private void GraphicLay(object sender, LayerLoadedEventArgs e)
        {
            
        }

    }
}
