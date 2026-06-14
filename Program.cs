using System;
using System.IO;

namespace BuildingPricingTool
{
    public class Building
    {
        public double Width { get; set; }
        public double Length { get; set; }
        public double WallHeight { get; set; }
        public double RoofPitch { get; set; }

        public Building(double width, double length, double wallHeight, double roofPitch)
        {
            Width = width;
            Length = length;
            WallHeight = wallHeight;
            RoofPitch = roofPitch;
        }

        public double CalculateFloorArea()
        {
            return Width * Length;
        }

        public double CalculateWallArea()
        {
            double longWalls = 2 * (Length * WallHeight);
            double shortWalls = 2 * (Width * WallHeight);

            return longWalls + shortWalls;
        }
        public double CalculateRoofArea()
        {
            double halfWidth = Width / 2;

            double roofRise = halfWidth * (RoofPitch / 12);

            double roofSlopeLength = Math.Sqrt(
                Math.Pow(halfWidth, 2) + Math.Pow(roofRise, 2)
            );

            double roofArea = roofSlopeLength * Length * 2;

            return roofArea;
        }
    }

    public abstract class BuildingFeature
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double UnitCost { get; set; }

        public BuildingFeature(string name, int count, double unitCost)
        {
            Name = name;
            Count = count;
            UnitCost = unitCost;
        }

        public virtual double CalculateCost()
        {
            return Count * UnitCost;
        }
    }

    public class Door : BuildingFeature
    {
        public Door(int count, double unitCost)
            : base("Door", count, unitCost)
        {
        }
    }

    public class Window : BuildingFeature
    {
        public Window(int count, double unitCost)
            : base("Window", count, unitCost)
        {
        }
    }

    public class Estimate
    {
        public Building Building { get; set; }
        public Door Doors { get; set; }
        public Window Windows { get; set;}
        public double MaterialCostPerSquareFoot { get; set; }
        public double LaborCostPerSquareFoot { get; set; }

        public Estimate(
            Building building,
            Door doors,
            Window windows,
            double materialCostPerSquareFoot, 
            double laborCostPerSquareFoot)
        {
            Building = building;
            Doors = doors;
            Windows = windows;
            MaterialCostPerSquareFoot = materialCostPerSquareFoot;
            LaborCostPerSquareFoot = laborCostPerSquareFoot;
        }

        public double CalculateTotalArea()
        {
            return Building.CalculateFloorArea()
                + Building.CalculateWallArea()
                + Building.CalculateRoofArea();
        }

        public double CalculateMaterialCost()
        {
            return CalculateTotalArea() * MaterialCostPerSquareFoot;
        }

        public double CalculateLaborCost()
        { 
            return CalculateTotalArea() * LaborCostPerSquareFoot;
        }

        public double CalculateAddOnCost()
        {
            return Doors.CalculateCost() + Windows.CalculateCost();
        }

        public double CalculateTotalCost()
        {
            return CalculateMaterialCost()
                + CalculateLaborCost()
                + CalculateAddOnCost();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Building Design & Pricing Tool");
            Console.WriteLine();

            Console.WriteLine("Enter customer name: ");
            string customerName = Console.ReadLine() ?? "";

            Console.Write("Enter project name: ");
            string projectName = Console.ReadLine() ?? "";

            Console.Write("Enter building width in feet: ");
            double width = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter building Length in feet: ");
            double length = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter wall height in feet: ");
            double wallHeight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter roof pitch, example 4 for 4/12 pitch: ");
            double roofPitch = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter material cost per square foot: ");
            double materialCostPerSquareFoot = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter labor cost per square foot: ");
            double laborCostPerSquareFoot = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter number of doors: ");
            int doorCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter cost per door: ");
            double doorUnitCost = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter number of windows: ");
            int windowCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter cost per window: ");
            double windowUnitCost = Convert.ToDouble(Console.ReadLine());

            Building building = new Building(width, length, wallHeight, roofPitch);
            Door doors = new Door(doorCount, doorUnitCost);
            Window windows = new Window(windowCount, windowUnitCost);

            Estimate estimate = new Estimate(
                building,
                doors,
                windows,
                materialCostPerSquareFoot,
                laborCostPerSquareFoot

            );
            Console.WriteLine();
            Console.WriteLine("----Building Estimate----");
            Console.WriteLine($"Floor Area: {building.CalculateFloorArea():F2} sq ft");
            Console.WriteLine($"Wall Area: {building.CalculateWallArea():F2} sq ft");
            Console.WriteLine($"Roof Area: {building.CalculateRoofArea():F2} sq ft");
            Console.WriteLine($"Total Estimated Area: {estimate.CalculateTotalArea():F2} sq ft");
            Console.WriteLine();
            Console.WriteLine("---- Cost Breakdown ----");
            Console.WriteLine($"Material Cost: ${estimate.CalculateMaterialCost():F2}");
            Console.WriteLine($"Labor Cost: ${estimate.CalculateLaborCost():F2}");
            Console.WriteLine($"Door Cost: ${doors.CalculateCost():F2}");
            Console.WriteLine($"Window Cost: ${windows.CalculateCost():F2}");
            Console.WriteLine($"Total Estimated Cost: ${estimate.CalculateTotalCost():F2}");

            string report =$@"
            Building Estimate Report
            ------------------------
            
            Customer: {customerName}
            Project: {projectName}
            
            Building Dimensions
            -------------------
            width: {width} ft
            length: {length} ft
            Wall height: {wallHeight} ft
            Roof Pitch: {roofPitch}/12
            
            Area Breakdown
            --------------
            Floor Area: {building.CalculateFloorArea():F2} sq ft
            Wall Area: {building.CalculateWallArea():F2} sq ft
            Roof Area: {building.CalculateRoofArea():F2} sq ft
            Total Estimated Area: {estimate.CalculateTotalArea():F2} sq ft
            
            Cost Breakdown
            --------------
            Material Cost: ${estimate.CalculateMaterialCost():F2}
            Labor Cost: ${estimate.CalculateLaborCost():F2}
            Door Cost: ${doors.CalculateCost():F2}
            Window Cost: ${windows.CalculateCost():F2}

            Total Estimated Cost: ${estimate.CalculateTotalCost():F2}
            ";

            File.WriteAllText("estimate_report.txt", report);

            //Save data for fusion 360 to read later
            string fusionData =
                $"{width},{length},{wallHeight},{roofPitch},{doorCount},{windowCount}";

            File.WriteAllText("fusion_building_data.csv", fusionData);

            Console.WriteLine();
            Console.WriteLine("Report saved as estimate_report.txt");
            Console.WriteLine("Fusion building data saved as fusion_building_data.csv");


        }
    } 
}