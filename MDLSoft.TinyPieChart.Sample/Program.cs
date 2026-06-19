using MDLSoft.TinyPieChart;
using System.Drawing;

Console.WriteLine("=== MDLSoft.TinyPieChart Library Demo ===\n");

// Create output directory if it doesn't exist
string outputDir = "output";
if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

// Example 1: Simple pie chart with default colors
Console.WriteLine("Example 1: Creating a simple pie chart...");
var chart1 = new PieChart
{
    Title = "Sales Distribution by Product",
    Width = 1000,
    Height = 700,
    ShowLegend = true,
    ShowPercentages = true
};

chart1.AddSlice("Product A", 350);
chart1.AddSlice("Product B", 280);
chart1.AddSlice("Product C", 220);
chart1.AddSlice("Product D", 150);

string file1 = Path.Combine(outputDir, "example1_simple.jpg");
chart1.ExportToJpg(file1);
Console.WriteLine($"✓ Saved to: {file1}\n");

// Example 2: Custom colors
Console.WriteLine("Example 2: Creating a pie chart with custom colors...");
var chart2 = new PieChart
{
    Title = "Market Share by Company",
    Width = 900,
    Height = 650,
    ShowLegend = true,
    ShowPercentages = true
};

chart2.AddSlice("Company A", 4500, Color.FromArgb(255, 99, 132));
chart2.AddSlice("Company B", 3200, Color.FromArgb(54, 162, 235));
chart2.AddSlice("Company C", 2100, Color.FromArgb(75, 192, 75));
chart2.AddSlice("Company D", 1800, Color.FromArgb(255, 206, 86));
chart2.AddSlice("Others", 900, Color.FromArgb(153, 102, 255));

string file2 = Path.Combine(outputDir, "example2_custom_colors.jpg");
chart2.ExportToJpg(file2);
Console.WriteLine($"✓ Saved to: {file2}\n");

// Example 3: Using PieSlice objects
Console.WriteLine("Example 3: Creating a pie chart with PieSlice objects...");
var slices = new[]
{
    new PieSlice("Q1 2024", 2500, Color.FromArgb(200, 100, 255)),
    new PieSlice("Q2 2024", 3200, Color.FromArgb(100, 200, 255)),
    new PieSlice("Q3 2024", 2800, Color.FromArgb(100, 255, 200)),
    new PieSlice("Q4 2024", 3500, Color.FromArgb(255, 200, 100))
};

var chart3 = new PieChart
{
    Title = "Quarterly Revenue 2024",
    Width = 900,
    Height = 650
};
chart3.AddSlices(slices);

string file3 = Path.Combine(outputDir, "example3_quarterly.jpg");
chart3.ExportToJpg(file3);
Console.WriteLine($"✓ Saved to: {file3}\n");

// Example 4: Budget allocation without percentages on slices
Console.WriteLine("Example 4: Budget allocation chart...");
var chart4 = new PieChart
{
    Title = "Budget Allocation",
    Width = 900,
    Height = 650,
    ShowLegend = true,
    ShowPercentages = false
};

chart4.AddSlice("Development", 4500);
chart4.AddSlice("Marketing", 2500);
chart4.AddSlice("Operations", 2000);
chart4.AddSlice("HR", 1000);

string file4 = Path.Combine(outputDir, "example4_budget.jpg");
chart4.ExportToJpg(file4);
Console.WriteLine($"✓ Saved to: {file4}\n");

// Example 5: Export to PNG
Console.WriteLine("Example 5: Exporting to PNG format...");
var chart5 = new PieChart
{
    Title = "Technology Stack Usage",
    Width = 800,
    Height = 600
};

chart5.AddSlice("C#", 4000);
chart5.AddSlice("JavaScript", 2500);
chart5.AddSlice("Python", 1500);
chart5.AddSlice("Go", 1000);

string file5 = Path.Combine(outputDir, "example5_tech_stack.png");
chart5.ExportToPng(file5);
Console.WriteLine($"✓ Saved to: {file5}\n");

// Example 6: Chart with labels on slices
Console.WriteLine("Example 6: Chart with labels displayed on slices...");
var chart6 = new PieChart
{
    Title = "Expense Categories",
    Width = 1000,
    Height = 700,
    ShowLegend = false,
    ShowLabels = true,
    ShowPercentages = true
};

chart6.AddSlice("Housing", 1200, Color.FromArgb(255, 107, 107));
chart6.AddSlice("Food", 400, Color.FromArgb(74, 144, 226));
chart6.AddSlice("Transport", 300, Color.FromArgb(56, 142, 60));
chart6.AddSlice("Entertainment", 200, Color.FromArgb(251, 188, 52));
chart6.AddSlice("Utilities", 150, Color.FromArgb(171, 71, 188));

string file6 = Path.Combine(outputDir, "example6_labels_on_slices.jpg");
chart6.ExportToJpg(file6);
Console.WriteLine($"✓ Saved to: {file6}\n");

// Example 7: Chart with summary box
Console.WriteLine("Example 7: Chart with summary box (Top-Right)...");
var chart7 = new PieChart
{
    Title = "Website Traffic Sources",
    Width = 1000,
    Height = 700,
    ShowLegend = true,
    ShowPercentages = true,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.TopRight
};

chart7.AddSlice("Organic Search", 3500);
chart7.AddSlice("Direct", 2100);
chart7.AddSlice("Referral", 1800);
chart7.AddSlice("Social Media", 1200);
chart7.AddSlice("Paid Ads", 900);

string file7 = Path.Combine(outputDir, "example7_summary_topright.jpg");
chart7.ExportToJpg(file7);
Console.WriteLine($"✓ Saved to: {file7}\n");

// Example 8: Chart with summary box (Bottom-Left) and labels
Console.WriteLine("Example 8: Chart with summary box (Bottom-Left) and labels...");
var chart8 = new PieChart
{
    Title = "Project Task Distribution",
    Width = 1000,
    Height = 700,
    ShowLegend = false,
    ShowLabels = true,
    ShowPercentages = false,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.BottomLeft
};

chart8.AddSlice("Backend", 4500);
chart8.AddSlice("Frontend", 3500);
chart8.AddSlice("Testing", 2000);
chart8.AddSlice("Documentation", 1000);

string file8 = Path.Combine(outputDir, "example8_summary_bottomleft_labels.jpg");
chart8.ExportToJpg(file8);
Console.WriteLine($"✓ Saved to: {file8}\n");

// Example 9: Chart with summary box at top center (full width)
Console.WriteLine("Example 9: Chart with summary at top center (full width)...");
var chart9 = new PieChart
{
    Title = "Department Budget",
    Width = 1200,
    Height = 700,
    ShowLegend = false,
    ShowPercentages = false,
    ShowLabels = true,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.TopCenter
};

chart9.AddSlice("Engineering", 5000);
chart9.AddSlice("Sales", 3500);
chart9.AddSlice("Marketing", 2500);
chart9.AddSlice("Operations", 2000);
chart9.AddSlice("HR", 1500);

string file9 = Path.Combine(outputDir, "example9_summary_topcenter.jpg");
chart9.ExportToJpg(file9);
Console.WriteLine($"✓ Saved to: {file9}\n");

// Example 10: Chart with summary box at bottom center (full width)
Console.WriteLine("Example 10: Chart with summary at bottom center (full width)...");
var chart10 = new PieChart
{
    Title = "Product Sales 2024",
    Width = 1200,
    Height = 700,
    ShowLegend = false,
    ShowPercentages = false,
    ShowLabels = false,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.BottomCenter
};

chart10.AddSlice("Product A", 4200);
chart10.AddSlice("Product B", 3800);
chart10.AddSlice("Product C", 2900);
chart10.AddSlice("Product D", 2100);
chart10.AddSlice("Product E", 1800);
chart10.AddSlice("Product F", 1200);

string file10 = Path.Combine(outputDir, "example10_summary_bottomcenter.jpg");
chart10.ExportToJpg(file10);
Console.WriteLine($"✓ Saved to: {file10}\n");

// Example 11: Corner summary box with legend items
Console.WriteLine("Example 11: Corner summary box (top-right) with legend items...");
var chart11 = new PieChart
{
    Title = "Revenue by Region",
    Width = 1100,
    Height = 700,
    ShowLegend = false,
    ShowPercentages = false,
    ShowLabels = false,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.TopRight,
    SummaryBoxFontSize = 16
};

chart11.AddSlice("North America", 5500);
chart11.AddSlice("Europe", 4200);
chart11.AddSlice("Asia Pacific", 3800);
chart11.AddSlice("Latin America", 1500);
chart11.AddSlice("Asia Pacific 2", 3800);
chart11.AddSlice("Latin America 2", 1500);

string file11 = Path.Combine(outputDir, "example11_summary_corner.jpg");
chart11.ExportToJpg(file11);
Console.WriteLine($"✓ Saved to: {file11}\n");

// Example 12: Full-width summary at bottom with many items
Console.WriteLine("Example 12: Full-width summary at bottom with many items...");
var chart12 = new PieChart
{
    Title = "Customer Distribution by Age Group",
    Width = 1400,
    Height = 700,
    ShowLegend = false,
    ShowPercentages = false,
    ShowLabels = true,
    ShowSummaryBox = true,
    SummaryBoxPosition = SummaryBoxPosition.BottomCenter
};

chart12.AddSlice("18-25", 2500);
chart12.AddSlice("26-35", 4200);
chart12.AddSlice("36-45", 3800);
chart12.AddSlice("46-55", 2900);
chart12.AddSlice("56-65", 1800);
chart12.AddSlice("65+", 1200);

string file12 = Path.Combine(outputDir, "example12_summary_fullwidth.jpg");
chart12.ExportToJpg(file12);
Console.WriteLine($"✓ Saved to: {file12}\n");
