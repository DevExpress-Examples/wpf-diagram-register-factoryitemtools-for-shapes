using DevExpress.Diagram.Core;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Diagram;
using System;
using System.Windows;

namespace WpfApp13 {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            RegisterStencil();
        }

        void RegisterStencil() {
            var stencil = new DiagramStencil("CustomStencil", "Custom Shapes");
            RegularFactoryItemTool(stencil);
            FactoryItemToolForCustomShape(stencil);

            DiagramToolboxRegistrator.RegisterStencil(stencil);

            diagramControl1.SelectedStencils = new StencilCollection() { "CustomStencil" };
        }

        public void RegularFactoryItemTool(DiagramStencil stencil) {
            var itemTool = new FactoryItemTool("CustomShape1",
                () => "Custom Shape 1",
                diagram => new DiagramShape() { Content = "Predefined text" },
                new System.Windows.Size(200, 200),
                false
            );

            stencil.RegisterTool(itemTool);
        }


        public void FactoryItemToolForCustomShape(DiagramStencil stencil) {
            DiagramControl.ItemTypeRegistrator.Register(typeof(DiagramShapeEx));
            ResourceDictionary customShapesDictionary = new ResourceDictionary() { Source = new Uri("CustomShapes.xaml", UriKind.Relative) };
            var invisibleStencil = DiagramStencil.Create("InvisibleStencil", "Invisible Stencil", customShapesDictionary, shapeName => shapeName, false);
            DiagramToolboxRegistrator.RegisterStencil(invisibleStencil);

            var itemTool = new FactoryItemTool("CustomShape2",
                () => "Custom Shape 2",
                diagram => new DiagramShapeEx() { 
                    Shape = DiagramToolboxRegistrator.GetStencil("InvisibleStencil").GetShape("Shape1"), 
                    CustomProperty = "Some value" 
                },
                new System.Windows.Size(200, 200), 
                false
            );

            stencil.RegisterTool(itemTool);
        }
    }

    public class DiagramShapeEx : DiagramShape {
        [XtraSerializableProperty]
        public string CustomProperty { get; set; }
    }
}
