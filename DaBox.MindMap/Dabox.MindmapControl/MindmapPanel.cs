using Dabox.MindmapControl.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using Dabox.MindmapControl;

namespace Dabox.MindmapControl
{
    /// <summary>
    /// The mindmap panel
    /// </summary>
    /// <seealso cref="Panel" />
    public class MindMapPanel : Panel
    {
        #region [ Fields ]

        /// <summary>
        /// Identifies the <see cref="HorizontalPadding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalPaddingProperty = DependencyProperty.Register("HorizontalPadding", typeof(double), typeof(MindMapPanel), new PropertyMetadata(40.0));

        /// <summary>
        /// Identifies the <see cref="VerticalPadding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalPaddingProperty = DependencyProperty.Register("VerticalPadding", typeof(double), typeof(MindMapPanel), new PropertyMetadata(40.0));

        /// <summary>
        /// The is measure dirty
        /// </summary>
        private bool isMeasureDirty = false;

        #endregion

        #region [ Constructors ]
        #endregion

        #region [ Attacted Properties ]

        /// <summary>
        /// The central point property
        /// </summary>
        public static readonly DependencyProperty CentralPointProperty = DependencyProperty.RegisterAttached("CentralPoint", typeof(Point), typeof(MindNodeControl), new PropertyMetadata(new Point(0, 0)));

        /// <summary>
        /// The absolute central point property
        /// </summary>
        public static readonly DependencyProperty AbsoluteCentralPointProperty = DependencyProperty.RegisterAttached("AbsoluteCentralPoint", typeof(Point), typeof(MindNodeControl), new PropertyMetadata(new Point(0, 0)));

        /// <summary>
        /// The anchor point property
        /// </summary>
        public static readonly DependencyProperty AnchorPointProperty = DependencyProperty.RegisterAttached("AnchorPoint", typeof(Point), typeof(MindNodeControl), new PropertyMetadata(new Point(0, 0)));

        /// <summary>
        /// The cover size property
        /// </summary>
        public static readonly DependencyProperty CoverSizeProperty = DependencyProperty.RegisterAttached("CoverSize", typeof(Size), typeof(MindNodeControl));

        #endregion

        #region [ Public Properties ]

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
                return (double)this.GetValue(MindMapPanel.VerticalPaddingProperty);
            }

            set
            {
                this.SetValue(MindMapPanel.VerticalPaddingProperty, value);
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
                return (double)this.GetValue(MindMapPanel.HorizontalPaddingProperty);
            }

            set
            {
                this.SetValue(MindMapPanel.HorizontalPaddingProperty, value);
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
        /// Gets or sets the root node control.
        /// </summary>
        /// <value>
        /// The root node control.
        /// </value>
        internal MindNodeControl RootNodeControl { get; set; }

        #endregion

        #region [ Attacted Properties Methods ]

        /// <summary>
        /// Gets the central point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Point GetCentralPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(CentralPointProperty);
        }

        /// <summary>
        /// Sets the central point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCentralPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(CentralPointProperty, value);
        }

        /// <summary>
        /// Gets the absolute central point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Point GetAbsoluteCentralPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(AbsoluteCentralPointProperty);
        }

        /// <summary>
        /// Sets the absolute central point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetAbsoluteCentralPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(AbsoluteCentralPointProperty, value);
        }

        /// <summary>
        /// Gets the anchor point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Point GetAnchorPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(AnchorPointProperty);
        }

        /// <summary>
        /// Sets the anchor point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetAnchorPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(AnchorPointProperty, value);
        }

        /// <summary>
        /// Gets the size of the cover.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Size GetCoverSize(DependencyObject obj)
        {
            return (Size)obj.GetValue(CoverSizeProperty);
        }

        /// <summary>
        /// Sets the size of the cover.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCoverSize(DependencyObject obj, Size value)
        {
            obj.SetValue(CoverSizeProperty, value);
        }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Disposes the childen.
        /// </summary>
        public void DisposeChilden()
        {
            foreach (UIElement child in this.Children)
            {
                this.RemoveHandler(child);
            }

            this.Children.Clear();
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddNode(MindNodeControl node)
        {
            this.AddHandler(node);

            this.InternalChildren.Add(node);

            if (node.LinkControl != null)
            {
                this.InternalChildren.Add(node.LinkControl);
            }

            this.InvalidateMeasure();
            this.InvalidateArrange();
            // TODO: Draw node here.
        }

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void RemoveNode(MindNodeControl node)
        {
            this.RemoveHandler(node);
            this.InternalChildren.Remove(node);
            if (node.LinkControl != null)
            {
                this.InternalChildren.Remove(node.LinkControl);
            }
        }

        #endregion

        #region [ Protected Methods ]

        /// <summary>
        /// When overridden in a derived class, measures the size in layout required for child elements and determines a size for the <see cref="T:System.Windows.FrameworkElement" />-derived class.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
        /// <returns>
        /// The size that this element determines it needs during layout, based on its calculations of child element sizes.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.isMeasureDirty = true;
            if (this.RootNodeControl == null)
            {
                return Size.Empty;
            }

            Size rootSize = this.Measure(this.RootNodeControl, availableSize);

            Point topLeft = new Point();
            Point bottomRight = new Point(rootSize.Width + this.HorizontalPadding * 2, rootSize.Height + this.VerticalPadding * 2);

            foreach (MindNodeControl node in this.InternalChildren.OfType<MindNodeControl>())
            {
                Point centralPoint = MindMapPanel.GetAbsoluteCentralPoint(node);

                topLeft.X = Math.Min(topLeft.X, centralPoint.X - node.DesiredSize.Width);
                topLeft.Y = Math.Min(topLeft.Y, centralPoint.Y - node.DesiredSize.Height);

                bottomRight.X = Math.Max(bottomRight.X, centralPoint.X + node.DesiredSize.Width);
                bottomRight.Y = Math.Max(bottomRight.Y, centralPoint.Y + node.DesiredSize.Height);
            }

            return new Rect(topLeft, bottomRight).Size;
        }

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a <see cref="T:System.Windows.FrameworkElement" /> derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>
        /// The actual size used.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.isMeasureDirty)
            {
                this.isMeasureDirty = false;
            }
            else
            {
                return finalSize;
            }

            Point topLeft = new Point();
            Point bottomRight = new Point();

            foreach (MindNodeControl node in this.InternalChildren.OfType<MindNodeControl>())
            {
                if (node.ParentNode == null)
                {
                    continue;
                }

                Point centralPoint = MindMapPanel.GetCentralPoint(node);

                topLeft.X = Math.Min(topLeft.X, centralPoint.X - node.DesiredSize.Width);
                topLeft.Y = Math.Min(topLeft.Y, centralPoint.Y - node.DesiredSize.Height);

                bottomRight.X = Math.Max(bottomRight.X, centralPoint.X + node.DesiredSize.Width);
                bottomRight.Y = Math.Max(bottomRight.Y, centralPoint.Y + node.DesiredSize.Height);
            }

            foreach (MindNodeControl node in this.InternalChildren.OfType<MindNodeControl>().ToList())
            {
                Point centralPoint = MindMapPanel.GetCentralPoint(node);

                if (node.ParentNode != null)
                {
                    centralPoint.Offset(node.ParentNode.DesiredSize.Width / 2, node.ParentNode.DesiredSize.Height / 2);
                    centralPoint = node.ParentNode.TranslatePoint(centralPoint, this);
                }
                else
                {
                    centralPoint.X = finalSize.Width / 2;
                    centralPoint.Y = finalSize.Height / 2;

                    switch (this.Layout)
                    {
                        case MindMapLayout.CenterLeft:
                        case MindMapLayout.Star:
                            centralPoint.Offset(-(topLeft.X + bottomRight.X) / 2, 0);
                            break;

                        case MindMapLayout.Tree:
                            centralPoint = new Point(0, (topLeft.Y + bottomRight.Y) / 2);
                            break;
                    }

                    switch (node.Position)
                    {
                        case NodePosition.Left:
                            centralPoint.X = node.DesiredSize.Width / 2 + this.HorizontalPadding;
                            break;

                        case NodePosition.Right:
                            centralPoint.X = finalSize.Width - this.HorizontalPadding;
                            break;

                        case NodePosition.Top:
                            centralPoint.Y = node.DesiredSize.Height / 2 + this.VerticalPadding;
                            break;

                        case NodePosition.Bottom:
                            centralPoint.Y = finalSize.Height - this.VerticalPadding;
                            break;
                    }
                }

                MindMapPanel.SetAbsoluteCentralPoint(node, centralPoint);

                if (node.LinkControl != null && node.ParentNode != null)
                {
                    Point parentCentral = MindMapPanel.GetAbsoluteCentralPoint(node.ParentNode);

                    Rect rect = new Rect(centralPoint, parentCentral);
                    rect.Width = Math.Max(rect.Width, node.LinkControl.MinWidth) + 10;
                    rect.Height = Math.Max(rect.Height, node.LinkControl.MinHeight) + 10;
                    rect.Offset(-5, -5);

                    node.LinkControl.Arrange(rect);
                }

                centralPoint.Offset(-node.DesiredSize.Width / 2, -node.DesiredSize.Height / 2);

                node.Arrange(new Rect(centralPoint, node.DesiredSize));
            }

            return finalSize;
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="child">The child.</param>
        private void AddHandler(UIElement child)
        {
            this.RemoveHandler(child);
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="child">The child.</param>
        private void RemoveHandler(UIElement child) { }

        /// <summary>
        /// Measures the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        private Size Measure(MindNodeControl node, Size size)
        {
            node.Measure(size);
            Size halfSize = new Size(node.DesiredSize.Width / 2, node.DesiredSize.Height / 2);
            Size parentHalfSize = new Size(0, 0);

            Point centralPoint = new Point(0, 0);
            if (node.ParentNode != null)
            {
                parentHalfSize = new Size(node.ParentNode.DesiredSize.Width / 2, node.ParentNode.DesiredSize.Height / 2);
            }

            switch (node.Position)
            {
                case NodePosition.Left:
                    centralPoint.Offset(-(parentHalfSize.Width + halfSize.Width + this.HorizontalPadding), 0);
                    break;

                case NodePosition.Right:
                    centralPoint.Offset(parentHalfSize.Width + halfSize.Width + this.HorizontalPadding, 0);
                    break;

                case NodePosition.Top:
                    centralPoint.Offset(0, -(parentHalfSize.Height + halfSize.Height + this.VerticalPadding));
                    break;

                case NodePosition.Bottom:
                    centralPoint.Offset(0, parentHalfSize.Height + halfSize.Height + this.VerticalPadding);
                    break;
            }

            if (node.ParentNode != null)
            {
                Point absoluteCentralPoint = MindMapPanel.GetAbsoluteCentralPoint(node.ParentNode);
                absoluteCentralPoint.Offset(halfSize.Width, halfSize.Height);
                absoluteCentralPoint.Offset(centralPoint.X, centralPoint.Y);
                MindMapPanel.SetAbsoluteCentralPoint(node, absoluteCentralPoint);
            }
            else
            {
                MindMapPanel.SetAbsoluteCentralPoint(node, centralPoint);
            }

            MindMapPanel.SetCentralPoint(node, centralPoint);

            if (node.ChildNodes?.Count > 0)
            {
                Dictionary<NodePosition, double> sideSize = new Dictionary<NodePosition, double>()
                    { {NodePosition.Left, -this.VerticalPadding }, {NodePosition.Right, -this.VerticalPadding },{ NodePosition.Top, -this.HorizontalPadding }, {NodePosition.Bottom, -this.HorizontalPadding } };

                foreach (MindNodeControl child in node.ChildNodes)
                {
                    Size childSize = this.Measure(child, size);
                    MindMapPanel.SetCoverSize(child, childSize);

                    switch (child.Position)
                    {
                        case NodePosition.Left:
                        case NodePosition.Right:
                            sideSize[child.Position] += childSize.Height + this.VerticalPadding;
                            break;

                        case NodePosition.Top:
                        case NodePosition.Bottom:
                            sideSize[child.Position] += childSize.Width + this.HorizontalPadding;
                            break;
                    }
                }

                // Measure for children side
                this.MeasureVerticalGroup(node.ChildNodes.Where(o => o.Position == NodePosition.Left), sideSize[NodePosition.Left]);
                this.MeasureVerticalGroup(node.ChildNodes.Where(o => o.Position == NodePosition.Right), sideSize[NodePosition.Right]);
                this.MeasureHorizontalGroup(node.ChildNodes.Where(o => o.Position == NodePosition.Top), sideSize[NodePosition.Top]);
                this.MeasureHorizontalGroup(node.ChildNodes.Where(o => o.Position == NodePosition.Bottom), sideSize[NodePosition.Bottom]);

                return new Size(Math.Max(Math.Max(sideSize[NodePosition.Top], sideSize[NodePosition.Bottom]), node.DesiredSize.Width), Math.Max(Math.Max(sideSize[NodePosition.Left], sideSize[NodePosition.Right]), node.DesiredSize.Height));
            }

            return node.DesiredSize;
        }

        /// <summary>
        /// Vertials the group measure.
        /// </summary>
        /// <param name="nodeControls">The node controls.</param>
        /// <param name="totalHeight">The total height.</param>
        private void MeasureVerticalGroup(IEnumerable<MindNodeControl> nodeControls, double totalHeight)
        {
            double offset = -(totalHeight / 2);
            foreach (MindNodeControl child in nodeControls)
            {
                Size childSize = MindMapPanel.GetCoverSize(child);

                offset += childSize.Height / 2;

                Point childCentralPoint = MindMapPanel.GetCentralPoint(child);

                childCentralPoint.Offset(0, offset);
                MindMapPanel.SetCentralPoint(child, childCentralPoint);

                offset += childSize.Height / 2 + this.VerticalPadding;
            }
        }

        /// <summary>
        /// Measures the horizontal group.
        /// </summary>
        /// <param name="nodeControls">The node controls.</param>
        /// <param name="totalWidth">The total width.</param>
        private void MeasureHorizontalGroup(IEnumerable<MindNodeControl> nodeControls, double totalWidth)
        {
            double offset = -(totalWidth / 2);
            foreach (MindNodeControl child in nodeControls)
            {
                Size childSize = MindMapPanel.GetCoverSize(child);

                offset += childSize.Width / 2;

                Point childCentralPoint = MindMapPanel.GetCentralPoint(child);

                childCentralPoint.Offset(offset, 0);
                MindMapPanel.SetCentralPoint(child, childCentralPoint);

                offset += childSize.Width / 2 + this.HorizontalPadding;
            }
        }

        #endregion
    }
}
