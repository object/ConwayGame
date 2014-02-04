#load "FSharpChart.fsx"
#load "../ConwayGame/ConwayGame.fs"
 
open System
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms.DataVisualization.Charting

open MSDN.FSharp.Charting
open MSDN.FSharp.Charting.ChartTypes

open ConwayGame

let showGrid (cells : List<int * int>) =
    cells
    |> FSharpChart.Point 
    |> FSharpChart.WithArea.AxisX(
        Minimum= -20.0, 
        Maximum=20.0, 
        MajorGrid=Grid(Enabled=false), 
        MajorTickMark=new TickMark(Interval=5.0), 
        LabelStyle=new LabelStyle(Interval=5.0))
    |> FSharpChart.WithArea.AxisY(
        Minimum= -20.0, 
        Maximum=20.0, 
        MajorGrid=Grid(Enabled=false), 
        MajorTickMark=new TickMark(Interval=5.0), 
        LabelStyle=new LabelStyle(Interval=5.0))
    |> FSharpChart.WithSeries.Marker(
        Color=Color.DarkBlue,
        Style=MarkerStyle.Star10,
        Size=15)

let pattern = [(-1,-2);(-1,-1);(-1,0);(0,-2);(0,1);(0,2);(1,-1);(1,1)]

let createChart =
    let chart = new Chart()
    chart.Width <- 700
    chart.Height <- 700
    chart.BackColor <- Color.Black
    let area = new ChartArea()
    area.BackColor <- Color.Black
    area.AxisX.Minimum <- -20.0
    area.AxisX.Maximum <- 20.0 
    area.AxisY.Minimum <- -20.0
    area.AxisY.Maximum <- 20.0 
    chart.ChartAreas.Add(area)
    let series = new Series()
    series.ChartType <- SeriesChartType.Point
    series.Color <- Color.White
    series.MarkerStyle <- MarkerStyle.Star10
    series.MarkerSize <- 15
    chart.Series.Add(series)    
    chart, series

let chart, series = createChart

let mainForm = new Form(Visible = true, Width = 720, Height = 720)
mainForm.Controls.Add(chart)

let mutable nextPattern = pattern

let updateLoop = async { 
    while not chart.IsDisposed do
        series.Points.Clear()
        nextPattern <- nextPattern |> nextGeneration
        nextPattern |> List.iter (fun (x,y) -> series.Points.AddXY(x,y) |> ignore)
        do! Async.Sleep(1000) }

Async.StartImmediate updateLoop
