using DaBox.Framework.Models;
using DaBox.Framework.WPF.Helpers;
using Dabox.MindmapControl.Enums;
using Dabox.MindmapControl.Helpers;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Dabox.MindmapControl
{
    /// <summary>
    /// The mindmap control
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Control" />
    [TemplatePart(Name = PART_Panel_Name, Type = typeof(MindMapPanel))]
    public class MindMapControl : Control
    {
        #region [ Fields ]

        /// <summary>
        /// The part panel name
        /// </summary>
        private const string PART_Panel_Name = "PART_Panel";

        /// <summary>
        /// Identifies the <see cref="RootNode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RootNodeProperty = DependencyProperty.Register("RootNode", typeof(IHierarchicalNode), typeof(MindMapControl), new FrameworkPropertyMetadata(RootNodePropertyChanged));

        /// <summary>
        /// Identifies the <see cref="MapVersion"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MapVersionProperty = DependencyProperty.Register("MapVersion", typeof(int), typeof(MindMapControl), new PropertyMetadata(0, MapVersionPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="Layout"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register("Layout", typeof(MindMapLayout), typeof(UIElement), new FrameworkPropertyMetadata(MindMapLayout.Tree, FrameworkPropertyMetadataOptions.Inherits, MindMapControl.OnLayoutPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="NodeStyleSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NodeStyleSelectorProperty = DependencyProperty.Register("NodeStyleSelector", typeof(NodeStyleSelector), typeof(MindMapControl));

        /// <summary>
        /// Identifies the <see cref="HorizontalPadding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalPaddingProperty = DependencyProperty.Register("HorizontalPadding", typeof(double), typeof(MindMapControl), new PropertyMetadata(40.0));

        /// <summary>
        /// Identifies the <see cref="VerticalPadding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalPaddingProperty = DependencyProperty.Register("VerticalPadding", typeof(double), typeof(MindMapControl), new PropertyMetadata(40.0));

        /// <summary>
        /// Identifies the <see cref="NodeClickCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NodeClickCommandProperty = DependencyProperty.Register("NodeClickCommand", typeof(ICommand), typeof(MindMapControl));

        /// <summary>
        /// Identifies the <see cref="SelectedNode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedNodeProperty = DependencyProperty.Register("SelectedNode", typeof(MindNodeControl), typeof(MindMapControl), new PropertyMetadata(null));

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes the <see cref="MindMapControl"/> class.
        /// </summary>
        static MindMapControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MindMapControl), new FrameworkPropertyMetadata(typeof(MindMapControl)));
        }

        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Gets or sets the RootNode value.
        /// </summary>
        /// <value>
        /// The RootNode value.
        /// </value>
        public IHierarchicalNode RootNode
        {
            get
            {
                return (IHierarchicalNode)this.GetValue(MindMapControl.RootNodeProperty);
            }

            set
            {
                this.SetValue(MindMapControl.RootNodeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Layout value.
        /// </summary>
        /// <value>
        /// The Layout value.
        /// </value>
        public MindMapLayout Layout
        {
            get
            {
                return (MindMapLayout)this.GetValue(MindMapControl.LayoutProperty);
            }

            set
            {
                this.SetValue(MindMapControl.LayoutProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the NodeStyleSelector value.
        /// </summary>
        /// <value>
        /// The NodeStyleSelector value.
        /// </value>
        public NodeStyleSelector NodeStyleSelector
        {
            get
            {
                return (NodeStyleSelector)this.GetValue(MindMapControl.NodeStyleSelectorProperty);
            }

            set
            {
                this.SetValue(MindMapControl.NodeStyleSelectorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the VerticalPadding value.
        /// </summary>
        /// <value>
        /// The VerticalPadding value.
        /// </value>
        public double VerticalPadding
        {
            get
            {
                return (double)this.GetValue(MindMapControl.VerticalPaddingProperty);
            }

            set
            {
                this.SetValue(MindMapControl.VerticalPaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the HorizontalPadding value.
        /// </summary>
        /// <value>
        /// The HorizontalPadding value.
        /// </value>
        public double HorizontalPadding
        {
            get
            {
                return (double)this.GetValue(MindMapControl.HorizontalPaddingProperty);
            }

            set
            {
                this.SetValue(MindMapControl.HorizontalPaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the ClickCommand value.
        /// </summary>
        /// <value>
        /// The ClickCommand value.
        /// </value>
        public ICommand NodeClickCommand
        {
            get
            {
                return (ICommand)this.GetValue(MindMapControl.NodeClickCommandProperty);
            }

            set
            {
                this.SetValue(MindMapControl.NodeClickCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the SelectedNode value.
        /// </summary>
        /// <value>
        /// The SelectedNode value.
        /// </value>
        public MindNodeControl SelectedNode
        {
            get
            {
                return (MindNodeControl)this.GetValue(MindMapControl.SelectedNodeProperty);
            }

            set
            {
                this.SetValue(MindMapControl.SelectedNodeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the MapVersion value.
        /// </summary>
        /// <value>
        /// The MapVersion value.
        /// </value>
        public int MapVersion
        {
            get
            {
                return (int)this.GetValue(MindMapControl.MapVersionProperty);
            }

            set
            {
                this.SetValue(MindMapControl.MapVersionProperty, value);
            }
        }

        #endregion

        #region [ Internal Properties ]

        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        internal MindMapPanel Panel { get; set; }

        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        internal Dictionary<IHierarchicalNode, MindNodeControl> Nodes { get; set; } = new Dictionary<IHierarchicalNode, MindNodeControl>();

        #endregion

        #region [ Protected Properties ]

        /// <summary>
        /// Gets the node pool.
        /// </summary>
        /// <value>
        /// The node pool.
        /// </value>
        protected UIElementPool<MindNodeControl> NodePool { get; } = new UIElementPool<MindNodeControl>();

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Panel = this.GetTemplateChild(MindMapControl.PART_Panel_Name) as MindMapPanel;

            this.RenderRootNode();
        }

        #endregion

        #region [ Protected Methods ]

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Roots the node property changed callback.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void RootNodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindMapControl control = d as MindMapControl;

            if (control != null)
            {
                control.RenderRootNode();
            }
        }

        /// <summary>
        /// Called when [layoyt property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnLayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindMapControl control = d as MindMapControl;

            if (control != null)
            {
                control.RenderRootNode();
            }
        }

        /// <summary>
        /// Maps the version property changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MapVersionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindMapControl control = d as MindMapControl;

            if (control != null)
            {
                control.RenderRootNode();
            }
        }

        /// <summary>
        /// Renders the root node.
        /// </summary>
        private void RenderRootNode()
        {
            this.Nodes.Clear();
            if (this.Panel != null)
            {
                this.Panel.DisposeChilden();

                if (this.RootNode != null)
                {
                    MindNodeControl control = this.CreateNode(null, this.RootNode);
                    this.Panel.RootNodeControl = control;
                }
            }
        }

        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="node">The root node.</param>
        private MindNodeControl CreateNode(MindNodeControl parent, IHierarchicalNode node)
        {
            if (this.Nodes.ContainsKey(node))
            {
                return this.Nodes[node];
            }

            node.Parent = parent?.Node as IHierarchicalNode;
            this.AddHandler(node);

            MindNodeControl nodeControl = this.GetNodeControl(parent, node);

            BindingOperations.SetBinding(nodeControl, MindNodeControl.ClickCommandProperty, new Binding("NodeClickCommand")
            {
                Source = this,
            });

            this.Nodes.Add(node, nodeControl);

            if (node.Children != null)
            {
                foreach (object child in node.Children)
                {
                    if (child != null)
                    {
                        this.CreateNode(nodeControl, child);
                    }
                }
            }

            return nodeControl;
        }

        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="child">The child.</param>
        private MindNodeControl CreateNode(MindNodeControl parent, object child)
        {
            if (child is IHierarchicalNode)
            {
                return this.CreateNode(parent, (IHierarchicalNode)child);
            }
            else
            {
                return this.GetNodeControl(parent, child);
            }
        }

        /// <summary>
        /// Gets the node control.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private MindNodeControl GetNodeControl(MindNodeControl parent, object node)
        {
            MindNodeControl nodeControl = this.NodePool.Pick();
            this.AddHandler(nodeControl);
            nodeControl.Node = node;

            if (parent != null)
            {
                nodeControl.ParentNode = parent;
                parent.ChildNodes.Add(nodeControl);

                if (nodeControl.LinkControl == null)
                {
                    nodeControl.LinkControl = new MindLinkControl();
                }

                nodeControl.LinkControl.ParentNode = parent;
                nodeControl.LinkControl.ChildNode = nodeControl;
            }

            nodeControl.Position = this.GetChildPosition(parent, nodeControl);

            this.Panel.AddNode(nodeControl);

            if (this.NodeStyleSelector != null)
            {
                Style customStyle = this.NodeStyleSelector.GetStyle(node);
                if (customStyle != null)
                {
                    nodeControl.Style = customStyle;
                }
            }

            return nodeControl;
        }

        /// <summary>
        /// Gets the child position.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        private NodePosition GetChildPosition(MindNodeControl parent, MindNodeControl child)
        {
            switch (this.Layout)
            {
                case MindMapLayout.Star:

                    if (parent == null)
                    {
                        return NodePosition.Center;
                    }

                    if (parent.Position == NodePosition.Center)
                    {
                        return parent.ChildNodes.IndexOf(child) % 2 == 0 ? NodePosition.Left : NodePosition.Right;
                    }

                    return parent.Position;

                case MindMapLayout.CenterLeft:
                    if (parent == null)
                    {
                        return NodePosition.Left;
                    }

                    return NodePosition.Right;

                case MindMapLayout.Tree:
                    if (parent == null)
                    {
                        return NodePosition.Top;
                    }

                    return NodePosition.Bottom;
            }

            return NodePosition.Center;
        }

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RemoveNode(MindNodeControl nodeControl)
        {
            this.RemoveHandler(nodeControl);
            foreach (MindNodeControl childNodeControl in nodeControl.ChildNodes)
            {
                this.RemoveNode(childNodeControl);
            }

            if (!this.NodePool.Push(nodeControl))
            {
                this.Panel.RemoveNode(nodeControl);
            }

            if (nodeControl.Node is IHierarchicalNode)
            {
                IHierarchicalNode node = (IHierarchicalNode)nodeControl.Node;
                if (!this.Nodes.ContainsKey(node))
                {
                    return;
                }

                this.RemoveHandler(node);

                this.Nodes.Remove(node);
            }
        }

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="node">The node.</param>
        private void AddHandler(IHierarchicalNode node)
        {
            this.RemoveHandler(node);

            if (node.Children is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)node.Children).CollectionChanged += this.Node_ChildrenChanged;
            }
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RemoveHandler(IHierarchicalNode node)
        {
            if (node.Children is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)node.Children).CollectionChanged -= this.Node_ChildrenChanged;
            }
        }

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="control">The control.</param>
        private void AddHandler(MindNodeControl control)
        {
            if (control == null) return;
            this.RemoveHandler(control);
            control.OnIsSelectedChanged += Node_OnIsSelectedChanged;
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="control">The control.</param>
        private void RemoveHandler(MindNodeControl control)
        {
            if (control == null) return;

            control.OnIsSelectedChanged -= Node_OnIsSelectedChanged;
        }

        /// <summary>
        /// Nodes the children changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Node_ChildrenChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            IHierarchicalNode node = sender as IHierarchicalNode;
            if (node == null)
            {
                return;
            }

            if (!this.Nodes.ContainsKey(node))
            {
                this.RemoveHandler(node);
                return;
            }

            MindNodeControl nodeControl = this.Nodes[node];
            foreach (MindNodeControl childControl in nodeControl.ChildNodes)
            {
                this.RemoveNode(childControl);
            }

            foreach (object child in node.Children)
            {
                this.CreateNode(nodeControl, child);
            }
        }

        /// <summary>
        /// Handles the OnIsSelectedChanged event of the Node control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Node_OnIsSelectedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.SelectedNode != null)
            {
                this.SelectedNode.IsSelected = false;
            }

            if (e.NewValue is not bool || (bool)e.NewValue != true)
            {
                return;
            }

            this.SelectedNode = sender as MindNodeControl;
        }

        #endregion
    }
}
