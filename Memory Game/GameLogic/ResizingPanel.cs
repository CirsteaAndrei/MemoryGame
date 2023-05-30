using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Memory_Game.GameLogic
{
    class ResizingPanel : StackPanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // Measure each child element, giving it all the available size
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
            }

            // Return the available size, since we don't need to reserve any space
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // Determine the size of each child element based on the available space
            double cellWidth = finalSize.Width / Children.Count;
            double cellHeight = finalSize.Height;

            // Arrange each child element in its own cell
            for (int i = 0; i < Children.Count; i++)
            {
                UIElement child = Children[i];

                double x = i * cellWidth;
                double y = 0;

                child.Arrange(new Rect(x, y, cellWidth, cellHeight));
            }

            // Return the final arranged size
            return finalSize;
        }
    }
}
