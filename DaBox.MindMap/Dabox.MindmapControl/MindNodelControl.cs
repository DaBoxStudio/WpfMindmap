using DaBox.Framework.Models;
using Dabox.MindmapControl.Enums;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Dabox.MindmapControl
{
    /// <summary>
    /// The Mind Node control
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Control" />
    public class MindNodeControl : Control
    {
        #region [ Fields ]

        /// <summary>
        /// Identifies the <see cref="Node" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty NodeProperty = DependencyProperty.Register("Node", typeof(object), typeof(MindNodeControl), new PropertyMetadata(NodePropertyChangedCallback));

        /// <summary>
        /// Identifies the <see cref="Position"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(NodePosition), typeof(MindNodeControl), new PropertyMetadata(NodePosition.Center, PositionPropertyChangedCallback));

        /// <summary>
        /// Identifies the <see cref="Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MindNodeControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(MindNodeControl));

        /// <summary>
        /// Identifies the <see cref="ClickCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(MindNodeControl));

        /// <summary>
        /// Identifies the <see cref="IsSelected"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(MindNodeControl), new PropertyMetadata(false, IsSelectedPropertyChangedCallback));

        /// <summary>
        /// Identifies the <see cref="CommandBar"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandBarProperty = DependencyProperty.Register("CommandBar", typeof(ContextMenu), typeof(MindNodeControl), new PropertyMetadata(null, OnCommandBarPropertyChanged));

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes the <see cref="MindNodeControl"/> class.
        /// </summary>
        static MindNodeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MindNodeControl), new FrameworkPropertyMetadata(typeof(MindNodeControl)));
        }

        #endregion

        #region [ Events ]

        /// <summary>
        /// Occurs when [on is selected changed].
        /// </summary>
        public event DependencyPropertyChangedEventHandler OnIsSelectedChanged;

        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Gets or sets the Node value.
        /// </summary>
        /// <value>
        /// The Node value.
        /// </value>
        public object Node
        {
            get
            {
                return (object)this.GetValue(MindNodeControl.NodeProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.NodeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Position value.
        /// </summary>
        /// <value>
        /// The Position value.
        /// </value>
        public NodePosition Position
        {
            get
            {
                return (NodePosition)this.GetValue(MindNodeControl.PositionProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.PositionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Title value.
        /// </summary>
        /// <value>
        /// The Title value.
        /// </value>
        public string Title
        {
            get
            {
                return (string)this.GetValue(MindNodeControl.TitleProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.TitleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Icon value.
        /// </summary>
        /// <value>
        /// The Icon value.
        /// </value>
        public object Icon
        {
            get
            {
                return (object)this.GetValue(MindNodeControl.IconProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.IconProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the ClickCommand value.
        /// </summary>
        /// <value>
        /// The ClickCommand value.
        /// </value>
        public ICommand ClickCommand
        {
            get
            {
                return (ICommand)this.GetValue(MindNodeControl.ClickCommandProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.ClickCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets the logical parent  element of this element.
        /// </summary>
        [Bindable(BindableSupport.No)]
        public MindNodeControl ParentNode { get; set; }

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        [Bindable(BindableSupport.No)]
        public List<MindNodeControl> ChildNodes { get; } = new List<MindNodeControl>();

        /// <summary>
        /// Gets or sets the link control.
        /// </summary>
        /// <value>
        /// The link control.
        /// </value>
        public MindLinkControl LinkControl { get; set; }

        /// <summary>
        /// Gets or sets the IsSelected value.
        /// </summary>
        /// <value>
        /// The IsSelected value.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return (bool)this.GetValue(MindNodeControl.IsSelectedProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.IsSelectedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the CommandBar value.
        /// </summary>
        /// <value>
        /// The CommandBar value.
        /// </value>
        public ContextMenu CommandBar
        {
            get
            {
                return (ContextMenu)this.GetValue(MindNodeControl.CommandBarProperty);
            }

            set
            {
                this.SetValue(MindNodeControl.CommandBarProperty, value);
            }
        }

        #endregion

        #region [ Protected Methods ]

        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.ContextMenu != null)
            {
                this.ContextMenu.PlacementTarget = this;
            }

            if (this.ContextMenu != null)
            {
                this.ContextMenu.IsOpen = true;
            }
            this.IsSelected = !this.IsSelected;

        }

        /// <summary>
        /// Raises the <see cref="E:MouseDoubleClick" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (this.ClickCommand == null || !this.ClickCommand.CanExecute(this.Node))
            {
                return;
            }

            this.ClickCommand.Execute(this.Node);
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Called when [command bar property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCommandBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //MindNodeControl control = d as MindNodeControl;
            //if (e.NewValue != null)
            //{
            //    control.AddVisualChild(e.NewValue as Visual);
            //}

            //if (e.OldValue != null)
            //{
            //    control.RemoveVisualChild(e.OldValue as Visual);
            //}
        }

        /// <summary>
        /// Nodes the property changed callback.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void NodePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindNodeControl control = d as MindNodeControl;

            control?.RemoveHandler(e.OldValue as IHierarchicalNode);
            control?.AddHandler(e.NewValue as IHierarchicalNode);
        }

        /// <summary>
        /// Positions the property changed callback.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void PositionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Determines whether [is selected property changed callback] [the specified d].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void IsSelectedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MindNodeControl control = d as MindNodeControl;

            control.OnIsSelectedChanged?.Invoke(d, e);
        }

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="node">The node.</param>
        private void AddHandler(IHierarchicalNode node)
        {
            if (node != null)
            {
                if (node.Children != null && node.Children is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)node.Children).CollectionChanged += this.Node_ChildrenCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RemoveHandler(IHierarchicalNode node)
        {
            if (node != null)
            {
                if (node.Children != null && node.Children is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)node.Children).CollectionChanged -= this.Node_ChildrenCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Notify when children collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Node_ChildrenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        #endregion
    }
}
