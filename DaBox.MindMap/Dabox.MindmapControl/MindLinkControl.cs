using Dabox.MindmapControl.Enums;
using Dabox.MindmapControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Dabox.MindmapControl
{
    /// <summary>
    /// The Mind Link Control
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Control" />
    [TemplatePart(Name = PART_Link_Name, Type = typeof(Path))]
    public class MindLinkControl : Control
    {
        #region [ Fields ]

        /// <summary>
        /// The part panel name
        /// </summary>
        private const string PART_Link_Name = "PART_Link";

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(double), typeof(MindLinkControl), new PropertyMetadata(5.0));


        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes the <see cref="MindLinkControl"/> class.
        /// </summary>
        static MindLinkControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MindLinkControl), new FrameworkPropertyMetadata(typeof(MindLinkControl)));
        }

        #endregion

        #region [ Public Properties ]

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
        public MindNodeControl ChildNode { get; set; }

        /// <summary>
        /// Gets or sets the CornerRadius value.
        /// </summary>
        /// <value>
        /// The CornerRadius value.
        /// </value>
        public double CornerRadius
        {
            get
            {
                return (double)this.GetValue(MindLinkControl.CornerRadiusProperty);
            }

            set
            {
                this.SetValue(MindLinkControl.CornerRadiusProperty, value);
            }
        }

        #endregion

        #region [ Internal Properties ]

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        internal Path Path { get; set; }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Path = this.GetTemplateChild(MindLinkControl.PART_Link_Name) as Path;
        }

        #endregion

        #region [ Protected Methods ]

        /// <summary>
        /// Arranges the override.
        /// </summary>
        /// <param name="arrangeBounds">The arrange bounds.</param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (this.ParentNode == null || this.ChildNode == null || this.Path == null)
            {
                return base.ArrangeOverride(arrangeBounds);
            }

            Point point1 = MindMapPanel.GetAbsoluteCentralPoint(this.ParentNode);
            Point point2 = MindMapPanel.GetAbsoluteCentralPoint(this.ChildNode);

            Rect rect = new Rect(point1, point2);
            rect.Width = Math.Max(rect.Width, this.MinWidth) + 10;
            rect.Height = Math.Max(rect.Height, this.MinHeight) + 10;
            rect.Offset(-5, -5);

            double minX = -Math.Min(point1.X, point2.X) + 5;
            double minY = -Math.Min(point1.Y, point2.Y) + 5;

            point1.Offset(minX, minY);
            point2.Offset(minX, minY);

            switch (this.ChildNode.Position)
            {
                case NodePosition.Left:
                    point1.Offset(-this.ParentNode.DesiredSize.Width / 2, 0);
                    point2.Offset(this.ChildNode.DesiredSize.Width / 2, 0);
                    break;

                case NodePosition.Right:
                    point1.Offset(this.ParentNode.DesiredSize.Width / 2, 0);
                    point2.Offset(-this.ChildNode.DesiredSize.Width / 2, 0);
                    break;

                case NodePosition.Top:
                    point1.Offset(0, -this.ParentNode.DesiredSize.Height / 2);
                    point2.Offset(0, this.ChildNode.DesiredSize.Height / 2);
                    break;

                case NodePosition.Bottom:
                    point1.Offset(0, this.ParentNode.DesiredSize.Height / 2);
                    point2.Offset(0, -this.ChildNode.DesiredSize.Height / 2);
                    break;
            }

            if (Math.Abs(point1.X - point2.X) < this.CornerRadius)
            {
                this.Path.Data = Geometry.Parse($"M {point1.X.ToParsingString()},{point1.Y.ToParsingString()} L {point1.X.ToParsingString()},{point2.Y.ToParsingString()}");
            }
            else if (Math.Abs(point1.Y - point2.Y) < this.CornerRadius)
            {
                this.Path.Data = Geometry.Parse($"M {point1.X.ToParsingString()},{point1.Y.ToParsingString()} L {point2.X.ToParsingString()},{point1.Y.ToParsingString()}");
            }
            else
            {
                string pathData = $"M {point1.X.ToParsingString()},{point1.Y.ToParsingString()}";
                double xRadius = (point1.X < point2.X) ? this.CornerRadius : -this.CornerRadius;
                double yRadius = (point1.Y < point2.Y) ? this.CornerRadius : -this.CornerRadius;

                switch (this.ChildNode.Position)
                {
                    case NodePosition.Left:
                    case NodePosition.Right:

                        //  this.Path.Data = Geometry.Parse($"M {point1.X},{point1.Y} C {point2.X},{point1.Y} {point1.X},{point2.Y} {point2.X},{point2.Y}");
                        //  this.Path.Data = Geometry.Parse($"M {point1.X},{point1.Y} L {(point1.X + point2.X) / 2},{point1.Y} {(point1.X + point2.X) / 2},{point2.Y} {point2.X},{point2.Y}");

                        double middleX = (point1.X + point2.X) / 2;

                        pathData += $" L {middleX.ToParsingString()},{point1.Y.ToParsingString()}";
                        //   pathData += $" Q {middleX},{point1.Y} {middleX},{point1.Y + yRadius}";
                        pathData += $" L {middleX.ToParsingString()},{(point2.Y - yRadius).ToParsingString()}";
                        pathData += $" Q {middleX.ToParsingString()},{point2.Y.ToParsingString()} {(middleX + xRadius).ToParsingString()},{point2.Y.ToParsingString()}";
                        break;

                    case NodePosition.Top:
                    case NodePosition.Bottom:
                        //this.Path.Data = Geometry.Parse($"M {point1.X},{point1.Y} L {point1.X},{(point1.Y + point2.Y) / 2} {point2.X},{(point1.Y + point2.Y) / 2} {point2.X},{point2.Y}");
                        //this.Path.Data = Geometry.Parse($"M {point1.X},{point1.Y} C {point1.X},{point2.Y} {point2.X},{point1.Y} {point2.X},{point2.Y}");

                        xRadius = -xRadius;
                        double middleY = (point1.Y + point2.Y) / 2;

                        pathData += $" L {point1.X.ToParsingString()},{middleY}.ToParsingString()";
                        //pathData += $" Q {point1.X},{middleY} {point1.X - xRadius},{middleY}";
                        pathData += $" L {(point2.X + xRadius).ToParsingString()},{middleY.ToParsingString()}";
                        pathData += $" Q {point2.X.ToParsingString()},{middleY.ToParsingString()} {point2.X.ToParsingString()},{(middleY + yRadius).ToParsingString()}";
                        break;
                }

                pathData += $" L {point2.X.ToParsingString()},{point2.Y.ToParsingString()}";

                this.Path.Data = Geometry.Parse(pathData);
            }

            return base.ArrangeOverride(rect.Size);
        }

        #endregion

        #region [ Private Methods ]
        #endregion
    }
}
