using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MDLSoft.TinyPieChart
{
    /// <summary>
    /// Position for the summary box on the chart.
    /// </summary>
    public enum SummaryBoxPosition
    {
        /// <summary>Top-left corner</summary>
        TopLeft,
        /// <summary>Top-right corner</summary>
        TopRight,
        /// <summary>Bottom-left corner</summary>
        BottomLeft,
        /// <summary>Bottom-right corner</summary>
        BottomRight,
        /// <summary>Top center spanning full width</summary>
        TopCenter,
        /// <summary>Bottom center spanning full width</summary>
        BottomCenter
    }

    /// <summary>
    /// A lightweight pie chart generator that exports to JPG format.
    /// </summary>
    public class PieChart
    {
        private readonly List<PieSlice> slices = [];

        /// <summary>
        /// Gets or sets the title of the pie chart.
        /// </summary>
        public string Title { get; set; } = "Pie Chart";

        /// <summary>
        /// Gets or sets the width of the chart in pixels (default: 800).
        /// </summary>
        public int Width { get; set; } = 800;

        /// <summary>
        /// Gets or sets the height of the chart in pixels (default: 600).
        /// </summary>
        public int Height { get; set; } = 600;

        /// <summary>
        /// Gets or sets whether to show a legend (default: true).
        /// </summary>
        public bool ShowLegend { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to show percentages on slices (default: true).
        /// </summary>
        public bool ShowPercentages { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to show labels directly on slices (default: false).
        /// </summary>
        public bool ShowLabels { get; set; }

        /// <summary>
        /// Gets or sets whether to show a summary box (default: false).
        /// </summary>
        public bool ShowSummaryBox { get; set; }

        /// <summary>
        /// Gets or sets the position of the summary box (default: TopRight).
        /// </summary>
        public SummaryBoxPosition SummaryBoxPosition { get; set; } = SummaryBoxPosition.TopRight;


        /// <summary>
        /// Adds a slice to the pie chart.
        /// </summary>
        /// <param name="label">The label for the slice.</param>
        /// <param name="value">The value for the slice (must be positive).</param>
        /// <param name="color">Optional color for the slice.</param>
        public void AddSlice(string label, float value, Color? color = null)
        {
            slices.Add(new PieSlice(label, value, color));
        }

        /// <summary>
        /// Adds multiple slices to the pie chart.
        /// </summary>
        /// <param name="slices">The slices to add.</param>
        public void AddSlices(params PieSlice[] slices)
        {
            this.slices.AddRange(slices);
        }

        /// <summary>
        /// Clears all slices from the pie chart.
        /// </summary>
        public void Clear()
        {
            slices.Clear();
        }

        /// <summary>
        /// Gets the number of slices in the pie chart.
        /// </summary>
        public int SliceCount => slices.Count;

        /// <summary>
        /// Creates a bitmap of the pie chart.
        /// </summary>
        /// <returns>A bitmap representing the pie chart.</returns>
        public Bitmap CreateChart()
        {
            if (slices.Count == 0)
                throw new InvalidOperationException("Cannot export pie chart with no slices.");

            Bitmap bitmap = new(Width, Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                DrawChart(g);
            }

            return bitmap;
        }

        /// <summary>
        /// Exports the pie chart to a JPG file.
        /// </summary>
        /// <param name="filePath">The path where to save the JPG file.</param>
        public void ExportToJpg(string filePath)
        {
            using (Bitmap bitmap = CreateChart())
            {
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Exports the pie chart to a PNG file.
        /// </summary>
        /// <param name="filePath">The path where to save the PNG file.</param>
        public void ExportToPng(string filePath)
        {
            using (Bitmap bitmap = CreateChart())
            {
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void DrawChart(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            float totalValue = slices.Sum(s => s.Value);

            // Draw title
            DrawTitle(g);

            // Pre-calculate summary box height if present to adjust pie positioning
            int summaryBoxHeight = 0;
            bool isFullWidthSummary = ShowSummaryBox &&
                (SummaryBoxPosition == SummaryBoxPosition.TopCenter ||
                 SummaryBoxPosition == SummaryBoxPosition.BottomCenter);

            if (isFullWidthSummary)
            {
                summaryBoxHeight = CalculateSummaryBoxHeight();
            }

            // Calculate pie dimensions with summary box adjustment
            int availableHeight = Height - 40; // Space for title
            if (isFullWidthSummary)
            {
                availableHeight -= (summaryBoxHeight + 20); // Space for summary box
            }

            int pieSize = Math.Min(Width, availableHeight) - 100;
            int pieX = (Width - pieSize) / 2;
            int pieY = 35 + (availableHeight - pieSize) / 2;

            // Assign default colors if needed
            AssignDefaultColors();

            // Draw pie slices
            float currentAngle = 0;
            for (int i = 0; i < slices.Count; i++)
            {
                float slicePercentage = slices[i].Value / totalValue;
                float sweepAngle = slicePercentage * 360f;

                DrawSlice(g, slices[i], currentAngle, sweepAngle, pieX, pieY, pieSize);
                currentAngle += sweepAngle;
            }

            // Draw legend if needed
            if (ShowLegend)
            {
                DrawLegend(g, totalValue, pieX, pieY + pieSize + 20);
            }

            // Draw summary box if needed
            if (ShowSummaryBox)
            {
                DrawSummaryBox(g, totalValue, summaryBoxHeight);
            }
        }

        private void DrawTitle(Graphics g)
        {
            using (Font font = new("Arial", 16, FontStyle.Bold))
            {
                SizeF titleSize = g.MeasureString(Title, font);
                float x = (Width - titleSize.Width) / 2;
                g.DrawString(Title, font, Brushes.Black, x, 10);
            }
        }

        private void DrawSlice(Graphics g, PieSlice slice, float startAngle, float sweepAngle,
            int pieX, int pieY, int pieSize)
        {
            Color color = slice.Color ?? Color.Black;

            using (Brush brush = new SolidBrush(color))
            using (Pen pen = new(Color.DarkGray, 1.5f))
            {
                g.FillPie(brush, pieX, pieY, pieSize, pieSize, startAngle, sweepAngle);
                g.DrawPie(pen, pieX, pieY, pieSize, pieSize, startAngle, sweepAngle);
            }

            // Calculate text position
            float midAngle = startAngle + (sweepAngle / 2f);
            float midAngleRad = (float)(midAngle * Math.PI / 180f);
            int textRadius = pieSize / 3;
            float textX = pieX + (pieSize / 2f) + (float)Math.Cos(midAngleRad) * textRadius;
            float textY = pieY + (pieSize / 2f) + (float)Math.Sin(midAngleRad) * textRadius;

            // Determine what text to display
            List<string> textLines = [];

            if (ShowLabels && sweepAngle > 8)
            {
                textLines.Add(slice.Label);
            }

            if (ShowPercentages && sweepAngle > 10)
            {
                float percentage = (sweepAngle / 360f) * 100f;
                textLines.Add($"{percentage:F1}%");
            }

            // Draw text lines
            if (textLines.Count > 0)
            {
                using (Font font = new("Arial", 9, FontStyle.Bold))
                {
                    float totalHeight = 0;
                    foreach (var line in textLines)
                    {
                        totalHeight += g.MeasureString(line, font).Height;
                    }

                    float currentY = textY - (totalHeight / 2);
                    foreach (var line in textLines)
                    {
                        SizeF textSize = g.MeasureString(line, font);

                        // Draw semi-transparent background for better readability
                        using (Brush bgBrush = new SolidBrush(Color.FromArgb(200, Color.Black)))
                        {
                            g.FillRectangle(bgBrush, textX - (textSize.Width / 2) - 2,
                                currentY - 1, textSize.Width + 4, textSize.Height + 2);
                        }

                        g.DrawString(line, font, Brushes.White,
                            textX - (textSize.Width / 2), currentY);

                        currentY += textSize.Height;
                    }
                }
            }
        }

        private void DrawLegend(Graphics g, float totalValue, int startX, int startY)
        {
            int legendX = startX;
            int legendY = startY;
            int boxSize = 15;
            int spacing = 5;
            int columnWidth = 250;

            using (Font font = new("Arial", 9))
            {
                int currentX = legendX;
                int currentY = legendY;
                int maxHeight = Height - legendY - 20;

                for (int i = 0; i < slices.Count; i++)
                {
                    float percentage = (slices[i].Value / totalValue) * 100f;
                    string legendText = $"{slices[i].Label}: {percentage:F1}%";

                    // Draw color box
                    Color color = slices[i].Color ?? Color.Black;
                    using (Brush brush = new SolidBrush(color))
                    {
                        g.FillRectangle(brush, currentX, currentY, boxSize, boxSize);
                    }
                    g.DrawRectangle(Pens.Black, currentX, currentY, boxSize, boxSize);

                    // Draw text
                    g.DrawString(legendText, font, Brushes.Black,
                        currentX + boxSize + spacing, currentY + 2);

                    currentY += boxSize + spacing + 3;

                    // Move to next column if running out of space
                    if (currentY + boxSize > maxHeight)
                    {
                        currentX += columnWidth;
                        currentY = legendY;
                    }
                }
            }
        }

        private void AssignDefaultColors()
        {
            Color[] defaultColors =
            [
                Color.FromArgb(255, 99, 132),    // Red
                Color.FromArgb(54, 162, 235),    // Blue
                Color.FromArgb(75, 192, 75),     // Green
                Color.FromArgb(255, 206, 86),    // Yellow
                Color.FromArgb(153, 102, 255),   // Purple
                Color.FromArgb(255, 159, 64),    // Orange
                Color.FromArgb(199, 199, 199),   // Grey
                Color.FromArgb(83, 102, 255),    // Indigo
                Color.FromArgb(255, 99, 255),    // Magenta
                Color.FromArgb(0, 200, 200),     // Cyan
            ];

            for (int i = 0; i < slices.Count; i++)
            {
                if (!slices[i].Color.HasValue)
                {
                    slices[i].Color = defaultColors[i % defaultColors.Length];
                }
            }
        }

        private int CalculateSummaryBoxHeight()
        {
            int padding = 10;
            int itemHeight = 18;

            // Calculate items per row based on average text width
            int itemsPerRow = Math.Max(1, (Width - (padding * 2)) / 200);
            int numRows = (slices.Count + itemsPerRow - 1) / itemsPerRow;

            return (itemHeight * numRows) + (padding * 2);
        }

        private void DrawSummaryBox(Graphics g, float totalValue, int preCalculatedHeight = 0)
        {
            int padding = 10;
            int boxSize = 12;
            int spacing = 5;
            int itemHeight = 18;

            // Determine position and dimensions
            int boxX, boxY, boxWidth, boxHeight;
            bool isFullWidth = SummaryBoxPosition == SummaryBoxPosition.TopCenter ||
                               SummaryBoxPosition == SummaryBoxPosition.BottomCenter;

            if (isFullWidth)
            {
                // Use pre-calculated height
                boxHeight = preCalculatedHeight > 0 ? preCalculatedHeight : CalculateSummaryBoxHeight();
                boxWidth = Width - (padding * 2);
                boxX = padding;

                if (SummaryBoxPosition == SummaryBoxPosition.TopCenter)
                    boxY = 35 + padding;
                else
                    boxY = Height - boxHeight - padding;
            }
            else
            {
                // Corner positions - single column layout
                boxWidth = 250;
                boxHeight = (itemHeight * slices.Count) + (padding * 2);

                switch (SummaryBoxPosition)
                {
                    case SummaryBoxPosition.TopLeft:
                        boxX = padding;
                        boxY = 35 + padding;
                        break;
                    case SummaryBoxPosition.TopRight:
                        boxX = Width - boxWidth - padding;
                        boxY = 35 + padding;
                        break;
                    case SummaryBoxPosition.BottomLeft:
                        boxX = padding;
                        boxY = Height - boxHeight - padding;
                        break;
                    case SummaryBoxPosition.BottomRight:
                    default:
                        boxX = Width - boxWidth - padding;
                        boxY = Height - boxHeight - padding;
                        break;
                }
            }

            // Draw box background
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
            using (Pen borderPen = new(Color.FromArgb(180, 180, 180), 1.5f))
            {
                g.FillRectangle(bgBrush, boxX, boxY, boxWidth, boxHeight);
                g.DrawRectangle(borderPen, boxX, boxY, boxWidth, boxHeight);
            }

            // Draw legend items
            using (Font itemFont = new Font("Arial", 12, FontStyle.Bold))
            {
                int currentX = boxX + padding;
                int currentY = boxY + padding;

                if (isFullWidth)
                {
                    // Calculate items per row dynamically based on available width
                    int itemsPerRow = Math.Max(1, (boxWidth - (padding * 2)) / 200);
                    int itemWidth = (boxWidth - (padding * 2)) / itemsPerRow;
                    int currentRow = 0;

                    for (int i = 0; i < slices.Count; i++)
                    {
                        // Calculate position in grid
                        int columnIndex = i % itemsPerRow;
                        currentRow = i / itemsPerRow;

                        currentX = boxX + padding + (columnIndex * itemWidth);
                        currentY = boxY + padding + (currentRow * itemHeight);

                        // Get slice info
                        float percentage = (slices[i].Value / totalValue) * 100f;
                        string itemText = $"{slices[i].Label} {percentage:F1}%";

                        // Draw color box
                        Color color = slices[i].Color ?? Color.Black;
                        using (Brush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, currentX, currentY + 3, boxSize, boxSize);
                        }
                        g.DrawRectangle(Pens.DarkGray, currentX, currentY + 3, boxSize, boxSize);

                        // Draw text
                        g.DrawString(itemText, itemFont, Brushes.Black,
                            currentX + boxSize + spacing, currentY);
                    }
                }
                else
                {
                    // Single column layout for corners
                    for (int i = 0; i < slices.Count; i++)
                    {
                        currentY = boxY + padding + (i * itemHeight);

                        float percentage = (slices[i].Value / totalValue) * 100f;
                        string itemText = $"{slices[i].Label} {percentage:F1}%";

                        // Draw color box
                        Color color = slices[i].Color ?? Color.Black;
                        using (Brush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, currentX, currentY + 3, boxSize, boxSize);
                        }
                        g.DrawRectangle(Pens.DarkGray, currentX, currentY + 3, boxSize, boxSize);

                        // Draw text
                        g.DrawString(itemText, itemFont, Brushes.Black,
                            currentX + boxSize + spacing, currentY);
                    }
                }
            }
        }
    }
}
