# MDLSoft.TinyPieChart

A lightweight, dependency-free C# library for creating pie charts and exporting them to JPG/PNG format.

## Features

- **No external dependencies** - Uses only System.Drawing.Common
- **.NET Standard 2.0 support** - Compatible with .NET Framework 4.8+ and .NET Core
- **Simple API** - Easy to configure and customize
- **Custom colors** - Set specific colors for each slice or use auto-generated defaults
- **Legend support** - Display slice labels and percentages
- **Configurable slice labels** - Show labels directly on pie slices
- **Summary box** - Display total, item count, and average values with configurable position
- **Percentages display** - Show percentage values directly on pie slices
- **Multiple export formats** - Export to JPG or PNG
- **Cross-platform** - Works on Windows, Linux, and macOS

## Installation

1. Add the MDLSoft.TinyPieChart project to your solution
2. Reference it in your project file or via `dotnet add reference`

## Quick Start

```csharp
using MDLSoft.TinyPieChart;
using System.Drawing;

// Create a pie chart
var chart = new PieChart { Title = "Sales by Product" };
chart.AddSlice("Product A", 350);
chart.AddSlice("Product B", 280);
chart.ExportToJpg("chart.jpg");
```

## Advanced Usage

### Custom Colors

```csharp
var chart = new PieChart { Title = "Market Share" };

// Add slices with specific colors
chart.AddSlice("Company A", 1500, Color.FromArgb(255, 99, 132));
chart.AddSlice("Company B", 1200, Color.FromArgb(54, 162, 235));
chart.AddSlice("Company C", 800, Color.FromArgb(75, 192, 75));
chart.AddSlice("Others", 500, Color.FromArgb(255, 206, 86));

chart.ExportToJpg("market.jpg");
```

## Advanced Usage

### Custom Colors

```csharp
var chart = new PieChart { Title = "Market Share" };

// Add slices with specific colors
chart.AddSlice("Company A", 1500, Color.FromArgb(255, 99, 132));
chart.AddSlice("Company B", 1200, Color.FromArgb(54, 162, 235));
chart.AddSlice("Company C", 800, Color.FromArgb(75, 192, 75));
chart.AddSlice("Others", 500, Color.FromArgb(255, 206, 86));

chart.ExportToJpg("market.jpg");
```

### Display Labels on Slices

```csharp
var chart = new PieChart
{
    Title = "Expense Categories",
    ShowLabels = true,      // Display slice labels directly on pie
    ShowPercentages = true, // Also show percentages
    ShowLegend = false      // Hide legend since labels are on slices
};

chart.AddSlice("Housing", 1200, Color.FromArgb(255, 107, 107));
chart.AddSlice("Food", 400, Color.FromArgb(74, 144, 226));
chart.AddSlice("Transport", 300, Color.FromArgb(56, 142, 60));

chart.ExportToJpg("expenses.jpg");
```

### Add a Summary Box with Legend and Percentages

```csharp
var chart = new PieChart
{
    Title = "Website Traffic",
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.TopRight  // Corner position
};

chart.AddSlice("Organic Search", 3500);
chart.AddSlice("Direct", 2100);
chart.AddSlice("Referral", 1800);
chart.AddSlice("Social Media", 1200);

chart.ExportToJpg("traffic.jpg");
```

The summary box displays:
- **Color box** - Colored square matching the slice
- **Label** - Name of the slice
- **Percentage** - Percentage of total value

#### Full-Width Summary Box (Top Center)

```csharp
var chart = new PieChart
{
    Title = "Department Budget",
    ShowLabels = true,
    ShowLegend = false,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.TopCenter  // Spans full width
};

chart.AddSlice("Engineering", 5000);
chart.AddSlice("Sales", 3500);
chart.AddSlice("Marketing", 2500);
chart.AddSlice("Operations", 2000);

chart.ExportToJpg("budget.jpg");
```

#### Full-Width Summary Box (Bottom Center)

```csharp
var chart = new PieChart
{
    Title = "Product Sales",
    ShowLabels = false,
    ShowLegend = false,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.BottomCenter  // Spans full width at bottom
};

chart.AddSlice("Product A", 4200);
chart.AddSlice("Product B", 3800);
chart.AddSlice("Product C", 2900);
chart.AddSlice("Product D", 2100);
chart.AddSlice("Product E", 1800);

chart.ExportToJpg("sales.jpg");
```

## API Reference

### PieChart Class

#### Properties

- `Title` (string) - The title of the pie chart (default: "Pie Chart")
- `Width` (int) - Width in pixels (default: 800, minimum: 300)
- `Height` (int) - Height in pixels (default: 600, minimum: 300)
- `ShowLegend` (bool) - Display legend with labels and percentages (default: true)
- `ShowPercentages` (bool) - Display percentage values on slices (default: true)
- `ShowLabels` (bool) - Display slice labels directly on pie slices (default: false)
- `ShowSummaryBox` (bool) - Display summary box with totals and averages (default: false)
- `SummaryBoxPosition` (SummaryBoxPosition) - Position of the summary box (default: TopRight)
- `SliceCount` (int) - Read-only property returning the number of slices

#### Methods

- `AddSlice(string label, float value, Color? color = null)` - Add a single slice
- `AddSlices(params PieSlice[] slices)` - Add multiple slices at once
- `Clear()` - Remove all slices
- `ExportToJpg(string filePath)` - Export chart to JPG file
- `ExportToPng(string filePath)` - Export chart to PNG file

### SummaryBoxPosition Enum

- `TopLeft` - Summary box in top-left corner
- `TopRight` - Summary box in top-right corner (default)
- `BottomLeft` - Summary box in bottom-left corner
- `BottomRight` - Summary box in bottom-right corner
- `TopCenter` - Full-width summary box at top
- `BottomCenter` - Full-width summary box at bottom

### PieSlice Class

#### Constructor

```csharp
PieSlice(string label, float value, Color? color = null)
```

- `label` - The label/name for the slice
- `value` - The numeric value (must be positive)
- `color` - Optional color; if not provided, a default color will be assigned

#### Properties

- `Label` (string) - The label for this slice
- `Value` (float) - The numeric value of the slice
- `Color` (Color?) - The color of the slice (nullable)

## Color Palette

If colors are not explicitly provided, the library uses this default palette (cycles for more than 10 slices):

1. Red (#FF6384)
2. Blue (#36A2EB)
3. Green (#4BC04B)
4. Yellow (#FFCE56)
5. Purple (#9966FF)
6. Orange (#FF9F40)
7. Grey (#C7C7C7)
8. Indigo (#5366FF)
9. Magenta (#FF63FF)
10. Cyan (#00C8C8)

## Configuration Options

### Display Options

| Option | Default | Purpose |
|--------|---------|---------|
| ShowLegend | true | Show legend with labels and percentages |
| ShowPercentages | true | Show percentage on each slice |
| ShowLabels | false | Show slice labels directly on pie |
| ShowSummaryBox | false | Show summary statistics box |

### Summary Box Positions

- **TopLeft** - Upper-left corner with vertical legend items
- **TopRight** - Upper-right corner with vertical legend items (default)
- **BottomLeft** - Lower-left corner with vertical legend items
- **BottomRight** - Lower-right corner with vertical legend items
- **TopCenter** - Top spanning full width with horizontal legend items
- **BottomCenter** - Bottom spanning full width with horizontal legend items

The summary box displays each slice with:
- Color box matching the slice color
- Slice label text
- Percentage of total value

## Requirements

- .NET Standard 2.0 or higher
- .NET Framework 4.8+ or .NET Core 2.0+
- System.Drawing.Common (included as dependency)

## Error Handling

The library validates input:

- **PieSlice value must be positive** - Throws `ArgumentException` if value <= 0
- **Cannot export empty chart** - Throws `InvalidOperationException` if no slices are added
- **Minimum chart dimensions** - Width and Height are automatically adjusted to minimum 300px

## Examples

The library includes 12 sample demonstrations:

1. Simple pie chart with default colors
2. Custom colors
3. Using PieSlice objects
4. Budget allocation
5. PNG export format
6. Labels displayed on slices
7. Summary box (top-right corner)
8. Summary box with labels (bottom-left corner)
9. Summary box at top center (full width)
10. Summary box at bottom center (full width)
11. Corner summary box with legend items
12. Full-width summary at bottom with many items

Run the sample with:
```bash
cd MDLSoft.TinyPieChart.Sample
dotnet run
```

All examples generate JPG/PNG files demonstrating:
- Various positioning options (corners and full-width)
- Legend display with color boxes and percentages
- Different combinations of labels, percentages, and legend
- Chart dimensions and positioning

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/mdallago/MDLSoft.TinyPieChart/issues) on GitHub.
