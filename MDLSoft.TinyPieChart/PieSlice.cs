using System;
using System.Drawing;

namespace MDLSoft.TinyPieChart
{
    /// <summary>
    /// Represents a single slice in a pie chart.
    /// </summary>
    public class PieSlice
    {
        /// <summary>
        /// Gets or sets the label for this slice.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value for this slice.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets the color for this slice. If null, a default color will be assigned.
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the PieSlice class.
        /// </summary>
        /// <param name="label">The label for the slice.</param>
        /// <param name="value">The value for the slice (must be positive).</param>
        /// <param name="color">Optional color for the slice.</param>
        public PieSlice(string label, float value, Color? color = null)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be positive.", nameof(value));

            Label = label ?? string.Empty;
            Value = value;
            Color = color;
        }
    }
}
