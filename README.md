<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1174035)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WPF DiagramControl - Register FactoryItemTools for Regular and Custom Shapes

This example demonstrates how to register `FactoryItemTools` for regular shapes and shapes created from SVG images or `ShapeTemplates`. The [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) allows you to add pre-configured or customized shapes and their descendants to stencils. The [DiagramDesignerControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramDesignerControl) displays shapes specified by registered `FactoryItemTools` in the [Shapes Panel](https://docs.devexpress.com/WPF/116504/controls-and-libraries/diagram-control/diagram-designer-control/shapes-panel).

## Implementation Details

Follow the steps below to register a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) for a regular shape:

1. [Create](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.DiagramStencil.Create.overloads) a stencil or use one of the existing stencils:

   ```cs
   void RegisterStencil() {
       var stencil = new DiagramStencil("CustomStencil", "Custom Shapes");
       RegularFactoryItemTool(stencil);
       FactoryItemToolForCustomShape(stencil);
       DiagramToolboxRegistrator.RegisterStencil(stencil);
       diagramControl1.SelectedStencils = new StencilCollection() { "CustomStencil" };
   }
   ```

2. Create a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) that specifies a diagram shape.
3. Pass your [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) instance to the [DiagramStencil.RegisterTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.DiagramStencil.RegisterTool(DevExpress.Diagram.Core.ItemTool)) method:

   ```cs
   public void RegularFactoryItemTool(DiagramStencil stencil) {
       var itemTool = new FactoryItemTool("CustomShape1",
           () => "Custom Shape 1",
           diagram => new DiagramShape() { Content = "Predefined text" },
           new System.Windows.Size(200, 200),
           false
       );

       stencil.RegisterTool(itemTool);
   }
   ```

You should create two stencils to register a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) for a custom shape:

1. Create an [invisible stencil](https://docs.devexpress.com/WPF/116504/controls-and-libraries/diagram-control/diagram-designer-control/shapes-panel#create-hidden-stencils) that contains your custom shape.
2. Create a visible stencil that contains the custom tool. This tool should access your custom shape by its `ID` from the invisible stencil:

```cs
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
```

## Files to Review

- [MainWindow.xaml.cs](./CS/WpfApp13/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/WpfApp13/MainWindow.xaml.vb))
- [CustomShapes.xaml](./CS/WpfApp13/CustomShapes.xaml)

## Documentation

- [Use Shape Templates to Create Shapes and Containers](https://docs.devexpress.com/WPF/117037/controls-and-libraries/diagram-control/diagram-items/creating-shapes-and-containers-using-shape-templates)
- [SVG Shapes](https://docs.devexpress.com/WPF/117321/controls-and-libraries/diagram-control/diagram-items/svg-shapes)
- [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool)

## More Examples

- [WPF DiagramControl - Create Custom Shapes with Connection Points](https://github.com/DevExpress-Examples/wpf-diagramdesigner-create-custom-shapes-with-connection-points)
