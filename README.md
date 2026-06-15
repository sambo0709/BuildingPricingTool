# Building Pricing Tool

## Overview

Building Pricing Tool is a C#/.NET project that calculates basic building measurements and creates output files that can be used for estimating and 3D visualization.

The program asks for building details such as width, length, wall height, roof pitch, number of doors, and number of windows. It then calculates building areas, creates an estimate report, and exports building data to a CSV file that can be read by a Fusion 360 Python script.

## Features

* Calculates floor area
* Calculates wall area
* Calculates roof area using roof pitch
* Accepts door and window counts
* Generates an estimate report
* Exports building data to a CSV file
* Supports a Fusion 360 workflow for creating a simple 3D building model

## Technologies Used

* C#
* .NET
* Object-Oriented Programming
* File input/output
* CSV data export
* Python scripting for Fusion 360
* Autodesk Fusion 360

## How It Works

The C# program collects building information from the user and uses that data to calculate the building estimate.

Example inputs:

```text
Width: 40
Length: 60
Wall Height: 14
Roof Pitch: 4
Doors: 1
Windows: 4
```

The program calculates:

```text
Floor Area
Wall Area
Roof Area
Material Cost
Labor Cost
Total Cost
```

It also creates a CSV file named:

```text
fusion_building_data.csv
```

That CSV file can be used by a Fusion 360 Python script to generate a basic 3D building model.

## Project Files

```text
Program.cs
```

Main C# program file. This contains the logic for getting user input, calculating the estimate, and writing output files.

```text
BuildingPricingTool.csproj
```

The .NET project file.

```text
estimate_report.txt
```

Generated report file containing the building estimate results.

```text
fusion_building_data.csv
```

Generated CSV file used to pass building data from the C# app to Fusion 360.

## Fusion 360 Workflow

The project connects C# with Fusion 360 by using a CSV file.

The workflow is:

```text
C# app → CSV file → Fusion 360 Python script → 3D building model
```

This allows the building dimensions entered in the C# app to be visualized as a 3D model in Fusion 360.

## Skills Demonstrated

* C# programming
* Object-oriented design
* Mathematical calculations
* File handling
* CSV generation
* Basic CAD automation
* Connecting software development with 3D modeling

## Future Improvements

* Add a web-based user interface with ASP.NET Core
* Add more detailed pricing options
* Add rooms and interior walls to the 3D model
* Add materials and colors in Fusion 360
* Add doors and windows as more detailed 3D features
* Export the finished 3D model as an STL file

## Author

Samuel Boye
