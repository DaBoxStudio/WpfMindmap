using System.Windows;

namespace Dabox.MindmapControl.Helpers
{
    /// <summary>
    /// The node style selector.
    /// </summary>
    public class NodeStyleSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeStyleSelector"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public virtual Style GetStyle(object node)
        {
            return null;
        }
    }
}
