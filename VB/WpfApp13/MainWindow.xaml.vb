Imports DevExpress.Diagram.Core
Imports DevExpress.Utils.Serializing
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace WpfApp13

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            RegisterStencil()
        End Sub

        Private Sub RegisterStencil()
            Dim stencil = New DiagramStencil("CustomStencil", "Custom Shapes")
            RegularFactoryItemTool(stencil)
            FactoryItemToolForCustomShape(stencil)
            DiagramToolboxRegistrator.RegisterStencil(stencil)
            Me.diagramControl1.SelectedStencils = New StencilCollection() From {"CustomStencil"}
        End Sub

        Public Sub RegularFactoryItemTool(ByVal stencil As DiagramStencil)
            Dim itemTool = New FactoryItemTool("CustomShape1", Function() "Custom Shape 1", Function(diagram) New DiagramShape() With {.Content = "Predefined text"}, New Size(200, 200), False)
            stencil.RegisterTool(itemTool)
        End Sub

        Public Sub FactoryItemToolForCustomShape(ByVal stencil As DiagramStencil)
            DiagramControl.ItemTypeRegistrator.Register(GetType(DiagramShapeEx))
            Dim customShapesDictionary As ResourceDictionary = New ResourceDictionary() With {.Source = New Uri("CustomShapes.xaml", UriKind.Relative)}
            Dim invisibleStencil = DiagramStencil.Create("InvisibleStencil", "Invisible Stencil", customShapesDictionary, Function(shapeName) shapeName, False)
            DiagramToolboxRegistrator.RegisterStencil(invisibleStencil)
            Dim itemTool = New FactoryItemTool("CustomShape2", Function() "Custom Shape 2", Function(diagram) New DiagramShapeEx() With {.Shape = DiagramToolboxRegistrator.GetStencil("InvisibleStencil").GetShape("Shape1"), .CustomProperty = "Some value"}, New Size(200, 200), False)
            stencil.RegisterTool(itemTool)
        End Sub
    End Class

    Public Class DiagramShapeEx
        Inherits DiagramShape

        <XtraSerializableProperty>
        Public Property CustomProperty As String
    End Class
End Namespace
