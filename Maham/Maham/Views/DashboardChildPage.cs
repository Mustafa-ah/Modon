using Microsoft.AppCenter.Crashes;
using Prism.Events;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Maham.CustomControl;
using Maham.Events;
using Maham.Models;
using Maham.Resources;
using Maham.Service.Model.Request.Dashboard;
using Maham.ViewModels;
using Xamarin.Forms;

namespace Maham.Views
{
    public class DashboardChildPage : ContentPage
    {
        #region filds 
        public bool isFiltter;
        DashboardChildPageViewModel dashboardChildPageViewModel;
        StackLayout stmain;
        private ActivityIndicator _aiLoading;
        private StackLayout _stLoading;
        public bool currentPage;
        public bool isRtl = false;
        private DisconnectedView _disconnectedView;
        #endregion
        #region Private Properties
        #endregion
        #region Public Properties 
        List<Chart> pageData;
        public ICommand RefreshCommand { get; set; }

        #endregion
        #region Private Properties 
        int _chartHieght;
        #endregion
        public DashboardChildPage()
        {
            //GetCurrentViewModel();
            //RenderDesign(dashboardChildPageViewModel.dashboardRootObjectModelApi);
            this.BackgroundColor = Color.White;
            Setting.Settings.DashboardtabsPage.Add(this);
            RefreshCommand = new Command(Appear);

            MessagingCenter.Subscribe<DashboardChildPageViewModel, int>(this, "RenderDesign", (sender, arg) => {
                //if (currentPage)
                //{
                //    Appear();
                //}
                //else
                //{
                //    dashboardChildPageViewModel = null;
                //}
                dashboardChildPageViewModel = null;
                Appear();
                return;
            });
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                isRtl = new Helpers.Helper().IsRtl;
                foreach (var item in Setting.Settings.DashboardtabsPage)
                {
                    item.currentPage = false;
                }
                currentPage = true;
                if (dashboardChildPageViewModel == null)
                {
                    Appear();
                }

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DashboardChildPage", "OnAppearing" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        public void Appear()
        {
            try
            {
                base.OnAppearing();
                _chartHieght = 700;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    isRtl = new Helpers.Helper().IsRtl;
                    currentPage = true;
                    InitailLoading();
                    GetCurrentViewModel();
                    if(dashboardChildPageViewModel==null)
                    {
                        RenderDesign();
                        return;
                    }
                    dashboardChildPageViewModel.IsBusy = true;

                    if (this.BindingContext == null)
                        return;
                    int tabId_ = ((DashboardChildModel)this.BindingContext).TabId;

                    await dashboardChildPageViewModel.Init(tabId_);
                    pageData = dashboardChildPageViewModel.ApiData;
                    RenderDesign();
                    dashboardChildPageViewModel.IsBusy = false;
                });
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DashboardChildPage", "appear" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private void InitailLoading()
        {
            #region Loading
            var lblLoading = new Label()
            {
                Text = AppResource.loading,
                FontSize = 12,
                Margin = 0,
                TextColor = Color.FromHex("#fefefe"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };
            _aiLoading = new ActivityIndicator
            {
                Color = Color.FromHex("#69CAF1"),
                WidthRequest = 40,
                Margin = 0,
                HeightRequest = 40,
                IsRunning = true,
                IsVisible = true
            };

            var stCenterLoading = new StackLayout()
            {
                Margin = 0,
                Padding = 0,
                Spacing = 5,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { _aiLoading, lblLoading }
            };

            _stLoading = new StackLayout()
            {
                Margin = 0,
                Padding = 0,
                Spacing = 5,
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.55),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = { stCenterLoading }
            };

            this.Content = _stLoading;

            #endregion
        }

        public void RenderDesign()
        {
            _disconnectedView = new DisconnectedView();
            _disconnectedView.BindingContext = dashboardChildPageViewModel;
            _disconnectedView.SetBinding(DisconnectedView.IsVisibleProperty, nameof(dashboardChildPageViewModel.NotConnected));
            _disconnectedView.SetBinding(DisconnectedView.TextProperty, nameof(dashboardChildPageViewModel.NotConnectedMsg));
            _disconnectedView.HeightRequest = 60;
            _disconnectedView.VerticalOptions = LayoutOptions.End;
            if (Device.RuntimePlatform == Device.iOS)
            {
                _disconnectedView.Margin = new Thickness(0, 0, 0, 85);
            }

            _stLoading.BindingContext = dashboardChildPageViewModel;
            _stLoading.SetBinding(StackLayout.IsVisibleProperty, nameof(dashboardChildPageViewModel.IsBusy));

            ScrollView scrollView = new ScrollView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            stmain = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            if (pageData.Count!=0)
            {
                int sum_ = pageData.Sum(s => s.chartPlottingInfo.Count);
               
                if (sum_ < 1)
                {
                    string noDataMsg = isRtl ? "لا توجد بيانات لهذا الشهر" : "No data for this month";

                    stmain.Children.Add(new Label
                    {
                        Text = noDataMsg,
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    });
                }
                else
                {
                    foreach (var item in pageData)
                    {
                        //Base Bar
                        if (item.chartTypeId == 3)
                        {
                            stmain.Children.Add(BaseBar(item));
                        }
                        //Base Column
                        else if (item.chartTypeId == 1)
                        {
                            stmain.Children.Add(BaseColumn(item));
                        }
                        //Base Stacked Column
                        else if (item.chartTypeId == 2)
                        {
                            stmain.Children.Add(StackedColumn(item));
                        }
                        //Base Stacked bar
                        else if (item.chartTypeId == 4)
                        {
                            stmain.Children.Add(StackedBar(item));
                        }
                        //Base Pie 
                        else if (item.chartTypeId == 5)
                        {
                            stmain.Children.Add(BasePie(item));
                        }
                        //Base  Doughnut
                        else if (item.chartTypeId == 6)
                        {
                            stmain.Children.Add(BaseDoughnut(item));
                        }
                    }
                }
                
            }
            scrollView.Content = stmain;

            var refreshView = new PullToRefreshLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = scrollView,
                RefreshColor = Color.FromHex("#3498db")
            };
            refreshView.BindingContext = this;
            refreshView.SetBinding<DashboardChildPage>(PullToRefreshLayout.IsRefreshingProperty, vm => vm.IsBusy, BindingMode.OneWay);
            refreshView.SetBinding<DashboardChildPage>(PullToRefreshLayout.RefreshCommandProperty, x => x.RefreshCommand);

            var grdPage = new Grid()
            {
                Padding = 0,
                Margin = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Children = { refreshView, _stLoading, _disconnectedView }
            };
            this.Content = grdPage;
        }
        //id=3 base bar
        public SfChart BaseBar(Chart data)
        {
            int labelRotation_ = -90;
            SfChart sfchart = new SfChart();
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;
            sfchart.HeightRequest = GetChartHeight(data); ;
            sfchart.ChartBehaviors = new ChartBehaviorCollection() { new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") } };
            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14
            };
            //sfchart.ColorModel = new ChartColorModel
            //{
            //    Palette = ChartColorPalette.Custom,
            //    CustomBrushes = new ChartColorCollection
            //    {
            //      Color.FromHex(data.chartPlottingInfo[0].seriesColor)
            //    }
            //};
            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };
                data.chartPlottingInfo[0].seriesData.ForEach((d) => {
                    if (string.IsNullOrEmpty(d.x_Ar))
                    {
                        d.x_Ar = d.x;
                    }
                });
                BarSeries barSeries1 = new BarSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo[0].seriesData,
                    XBindingPath = "x_Ar",
                    YBindingPath = "y",
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = new ChartColorCollection()
                        {
                            Color.FromHex(data.chartPlottingInfo[0].seriesColor)
                        }
                    }
                };

                barSeries1.Label = data.chartPlottingInfo[0].seriesNameAr;

                barSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Inner,
                        BackgroundColor = Color.Transparent,
                        FontSize = 12
                    }
                };
                if (data.chartPlottingInfo[0].seriesData.Count < 3)
                {
                    barSeries1.Width = 0.2;
                }
                sfchart.Series.Add(barSeries1);
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                    Title = new ChartAxisTitle { Text = data.primaryAxisTitle }
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };
                BarSeries barSeries1 = new BarSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo[0].seriesData,
                    XBindingPath = "x",
                    YBindingPath = "y",
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = new ChartColorCollection()
                        {
                            Color.FromHex(data.chartPlottingInfo[0].seriesColor)
                        }
                    }
                };
                barSeries1.Label = data.chartPlottingInfo[0].seriesName;
                barSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Outer,
                        BackgroundColor = Color.Transparent,
                     
                        FontSize = 12
                    }
                };
                if (data.chartPlottingInfo[0].seriesData.Count < 3)
                {
                    barSeries1.Width = 0.2;
                }
                sfchart.Series.Add(barSeries1);
            }
            return sfchart;
        }
        //id=1 base Column
        public SfChart BaseColumn(Chart data)
        {
            
            int labelRotation_ =-90;
            SfChart sfchart = new SfChart();
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;
            sfchart.HeightRequest = GetChartHeight(data);
            sfchart.ChartBehaviors = new ChartBehaviorCollection() { new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") } };
            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14
            };

            sfchart.ColorModel = new ChartColorModel
            {
                Palette = ChartColorPalette.Custom,
                CustomBrushes = new ChartColorCollection
                {
                  Color.FromHex("#1BACFB")
                }
            };
            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    Title = new ChartAxisTitle { Text = primaryAxisTitle },
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    RangePadding = NumericalPadding.Normal,
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };
                foreach (var item in data.chartPlottingInfo)
                {
                    item.seriesData.ForEach((d) => {
                        if (string.IsNullOrEmpty(d.x_Ar))
                        {
                            d.x_Ar = d.x;
                        }
                    });
                    ColumnSeries barSeries1 = new ColumnSeries();
                    barSeries1.TooltipTemplate = ToolTipTemplate1();
                    barSeries1.ItemsSource = item.seriesData;
                    barSeries1.XBindingPath = "x_Ar";
                    barSeries1.YBindingPath = "y";
                    if (item.seriesColor !=null)
                    barSeries1.Color = Color.FromHex(item.seriesColor);
                    barSeries1.Label = item.seriesNameAr;
                    barSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Center,
                            BackgroundColor = Color.Transparent,
                            FontSize = 12
                        }
                    };
                    if (item.seriesData.Count < 3)
                    {
                        barSeries1.Width = 0.2;
                    }
                    sfchart.Series.Add(barSeries1);
                }
                
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    Title = new ChartAxisTitle { Text = data.primaryAxisTitle },
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    RangePadding = NumericalPadding.Normal,
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };
                foreach (var item in data.chartPlottingInfo)
                {
                    ColumnSeries barSeries1 = new ColumnSeries();
                    barSeries1.TooltipTemplate = ToolTipTemplate1();
                    barSeries1.ItemsSource = item.seriesData;
                    barSeries1.XBindingPath = "x";
                    barSeries1.YBindingPath = "y";
                    if (item.seriesColor != null)
                        barSeries1.Color = Color.FromHex(item.seriesColor);
                    barSeries1.Label = item.seriesName;
                    barSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Center,
                            BackgroundColor = Color.Transparent,
                            FontSize = 12
                        }
                    };
                    if (item.seriesData.Count < 3)
                    {
                        barSeries1.Width = 0.2;
                    }
                    sfchart.Series.Add(barSeries1);
                }
            }
            return sfchart;
        }
        //id=2 Stacked Column
        public SfChart StackedColumn(Chart data)
        {
            int labelRotation_ = -90;
            SfChart sfchart = new SfChart();
            sfchart.ChartPadding = new Thickness(7);
           // sfchart.
            //sfchart.BackgroundColor = Color.Gray;
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;

            sfchart.HeightRequest = GetChartHeight(data);// + 700;
            sfchart.ChartBehaviors = new ChartBehaviorCollection() { new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") } };

            ChartLegendLabelStyle ChartLegendLabelStyle = new ChartLegendLabelStyle()
            {
                TextColor = Color.Black,
                FontSize = 10,
            };

            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                IsVisible = true,
                LabelStyle = ChartLegendLabelStyle,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14,
            };

            AdjustPlottingInfoa(data.chartPlottingInfo);
            //ChartColorCollection chartColorCollectionnew = new ChartColorCollection() { };
            //foreach (var item in data.chartPlottingInfo)
            //{
            //    chartColorCollectionnew.Add(Color.FromHex(item.seriesColor));
            //}
            //sfchart.ColorModel = new ChartColorModel
            //{
            //    Palette = ChartColorPalette.Custom,
            //    CustomBrushes = chartColorCollectionnew,
            //};
            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Fit,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.None,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    RangePadding = NumericalPadding.Normal,
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };
                foreach (var itemChartPlottingInfo in data.chartPlottingInfo)
                {
                    itemChartPlottingInfo.seriesData.ForEach((d) => {
                        if (string.IsNullOrEmpty(d.x_Ar))
                        {
                            d.x_Ar = d.x;
                        }
                    });
                    var orderdList = data.chartTypeId == 2 ? itemChartPlottingInfo.seriesData.OrderBy(w => w.x) : itemChartPlottingInfo.seriesData.AsEnumerable();
                    StackingColumnSeries columnSeries1 = new StackingColumnSeries()
                    {
                        TooltipTemplate = ToolTipTemplate1(),
                        ItemsSource = orderdList,
                        XBindingPath = "x_Ar",
                        YBindingPath = "y",
                        ColorModel = new ChartColorModel()
                        {
                            Palette = ChartColorPalette.Custom
                        }
                    };
                    if (itemChartPlottingInfo.seriesColor != null)
                        columnSeries1.ColorModel.CustomBrushes = new ChartColorCollection() { Color.FromHex(itemChartPlottingInfo.seriesColor) };

                    columnSeries1.Label = itemChartPlottingInfo.seriesNameAr;

                    columnSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Center,
                            BackgroundColor = Color.Transparent,
                            FontSize = 10
                        }
                    };
                    if (data.chartPlottingInfo.Count < 3)
                    {
                        columnSeries1.Width = 0.2;
                    }
                    sfchart.Series.Add(columnSeries1);
                }
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Fit,
                    Title = new ChartAxisTitle { Text = data.primaryAxisTitle },
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.None,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    RangePadding = NumericalPadding.Normal,
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 10, StrokeColor = Color.Red },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };

                foreach (var itemChartPlottingInfo in data.chartPlottingInfo)
                {
                    var orderdList = data.chartTypeId==2? itemChartPlottingInfo.seriesData.OrderBy(w => w.x): itemChartPlottingInfo.seriesData.AsEnumerable();
                    StackingColumnSeries columnSeries1 = new StackingColumnSeries()
                    {
                        TooltipTemplate = ToolTipTemplate1(),
                        ItemsSource = orderdList,
                        XBindingPath = "x",
                        YBindingPath = "y",
                        ColorModel = new ChartColorModel()
                        {
                            Palette = ChartColorPalette.Custom
                        }
                    };
                    if (itemChartPlottingInfo.seriesColor != null)
                        columnSeries1.ColorModel.CustomBrushes = new ChartColorCollection() { Color.FromHex(itemChartPlottingInfo.seriesColor) };

                    columnSeries1.Label = itemChartPlottingInfo.seriesName;

                    columnSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Center,
                            BackgroundColor = Color.Transparent,
                            FontSize = 10
                        }
                    };
                    if (data.chartPlottingInfo.Count < 3)
                    {
                        columnSeries1.Width = 0.2;
                    }
                    sfchart.Series.Add(columnSeries1);
                }
            }
            return sfchart;
        }
        //id=4 Stacked bar
        public SfChart StackedBar(Chart data)
        {
            int labelRotation_ = -90;
            SfChart sfchart = new SfChart();
           // sfchart.BackgroundColor = Color.Yellow;
           // sfchart.SideBySideSeriesPlacement = false;
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;
            sfchart.HeightRequest = CalculateHeight(data);// 550;
            sfchart.ChartBehaviors = new ChartBehaviorCollection()
            {
                new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") }
            };

            ChartLegendLabelStyle ChartLegendLabelStyle = new ChartLegendLabelStyle()
            {
                TextColor = Color.Black,
                FontSize = 10,
            };

            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                IsVisible = true,
                LabelStyle = ChartLegendLabelStyle,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14,
            };

            AdjustPlottingInfoa(data.chartPlottingInfo);

            //ChartColorCollection chartColorCollectionnew = new ChartColorCollection() { };
            //foreach (var item in data.chartPlottingInfo)
            //{
            //    chartColorCollectionnew.Add(Color.FromHex(item.seriesColor));
            //}
            //sfchart.ColorModel = new ChartColorModel
            //{
            //    Palette = ChartColorPalette.Custom,
            //    CustomBrushes = chartColorCollectionnew,
            //};
            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };
                foreach (var itemChartPlottingInfo in data.chartPlottingInfo)
                {
                   
                    ChartColorCollection barSeriesColorCollection = new ChartColorCollection() { Color.FromHex(itemChartPlottingInfo.seriesColor) };
                    itemChartPlottingInfo.seriesData.ForEach((d) => {
                        if (string.IsNullOrEmpty(d.x_Ar))
                        {
                            d.x_Ar = d.x;
                        }
                    });

                    var orderdList = itemChartPlottingInfo.seriesData.OrderBy(w => w.x);

                    BarSeries barSeries1 = new BarSeries()
                    {
                        TooltipTemplate = ToolTipTemplate1(),
                        ItemsSource = orderdList,
                        XBindingPath = "x_Ar",
                        YBindingPath = "y",
                        ColorModel = new ChartColorModel()
                        {
                            Palette = ChartColorPalette.Custom,
                            CustomBrushes = barSeriesColorCollection
                        }
                    };
                    barSeries1.Label = itemChartPlottingInfo.seriesNameAr;
                    barSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Inner,
                            BackgroundColor = Color.Transparent,
                            FontSize = 10
                        }
                    };
                    //if (data.chartPlottingInfo.Count < 3)
                    //{
                    //    barSeries1.Width = CalculateBarWidth(data);// 0.2;
                    //}
                    barSeries1.Width = CalculateBarWidth(data);// 0.2;
                    sfchart.Series.Add(barSeries1);
                }
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false, MaximumLabels=1,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };
               
                foreach (var itemChartPlottingInfo in data.chartPlottingInfo)
                {
                    var orderdList = itemChartPlottingInfo.seriesData.OrderBy(w => w.x);
                    ChartColorCollection barSeriesColorCollection = new ChartColorCollection() { Color.FromHex(itemChartPlottingInfo.seriesColor) };
                    BarSeries barSeries1 = new BarSeries()
                    {
                        TooltipTemplate = ToolTipTemplate1(),
                        ItemsSource = orderdList,
                        XBindingPath = "x",
                        YBindingPath = "y",
                        ColorModel = new ChartColorModel()
                        {
                            Palette = ChartColorPalette.Custom,
                            CustomBrushes = barSeriesColorCollection
                        }
                    };
                    barSeries1.Label = itemChartPlottingInfo.seriesName;
                    barSeries1.DataMarker = new ChartDataMarker
                    {
                        LabelStyle = new DataMarkerLabelStyle
                        {
                            LabelPosition = DataMarkerLabelPosition.Inner,
                            BackgroundColor = Color.Transparent,
                            FontSize = 10
                        }
                    };
                    //if (data.chartPlottingInfo.Count < 3)
                    //{
                    //    barSeries1.Width = CalculateBarWidth(data);// 0.2;
                    //}
                    barSeries1.Width = CalculateBarWidth(data);// 0.2;
                    sfchart.Series.Add(barSeries1);
                }
            }
            return sfchart;
        }
        //id=5 base Pie
        public SfChart BasePie(Chart data)
        {
            int labelRotation_ = -90;
            //set chart hieght
            //int chartHeight_ = _chartHieght;
            //if (data.chartPlottingInfo.Count > 5)
            //{
            //    chartHeight_ = _chartHieght + 150;
            //}
            SfChart sfchart = new SfChart();
            //sfchart.BackgroundColor = Color.Red;
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;
            sfchart.HeightRequest = _chartHieght + 150;
            sfchart.ChartBehaviors = new ChartBehaviorCollection() { new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") } };
            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14
            };
            ChartColorCollection chartColorCollectionnew = new ChartColorCollection() { };
            foreach (var item in data.chartPlottingInfo)
            {
                chartColorCollectionnew.Add(Color.FromHex(item.color));
            }
            //sfchart.ColorModel = new ChartColorModel
            //{
            //    Palette = ChartColorPalette.Custom,
            //    CustomBrushes = chartColorCollectionnew
            //};

            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };

                PieSeries PieSeries1 = new PieSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo,
                    XBindingPath = "name",
                    YBindingPath = "y",
                    EnableSmartLabels=true,
                    ConnectorLineType= ConnectorLineType.Bezier,
                    DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                    StartAngle = 75,
                     EndAngle = 435,
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = chartColorCollectionnew
                    }
                };

                PieSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Outer,
                        BackgroundColor = Color.Transparent,
                        FontSize = 12

                    }
                    
                };
              
                sfchart.Series.Add(PieSeries1);
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    Title = new ChartAxisTitle { Text = data.primaryAxisTitle },
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };

                PieSeries PieSeries1 = new PieSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo,
                    XBindingPath = "name",
                    YBindingPath = "y", EnableSmartLabels = true,
                    ConnectorLineType = ConnectorLineType.Bezier,
                    DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                    StartAngle = 75,
                    EndAngle = 435,
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = chartColorCollectionnew
                    }
                };
                PieSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Outer,
                        BackgroundColor = Color.Transparent,
                        FontSize = 12

                    }
                };
                sfchart.Series.Add(PieSeries1);
            }
            return sfchart;
        }
        //id=6 base Doughnuts
        public SfChart BaseDoughnut(Chart data)
        {
            int labelRotation_ = -90;
            SfChart sfchart = new SfChart();
            sfchart.HorizontalOptions = LayoutOptions.FillAndExpand;
            sfchart.HeightRequest = _chartHieght;
            sfchart.ChartBehaviors = new ChartBehaviorCollection() { new ChartTooltipBehavior() { BackgroundColor = Color.FromHex("#404041") } };
            sfchart.Legend = new ChartLegend()
            {
                ToggleSeriesVisibility = true,
                DockPosition = LegendPlacement.Bottom,
                OverflowMode = ChartLegendOverflowMode.Wrap,
                IconHeight = 14,
                IconWidth = 14
            };
            ChartColorCollection chartColorCollectionnew = new ChartColorCollection() { };
            foreach (var item in data.chartPlottingInfo)
            {
                chartColorCollectionnew.Add(Color.FromHex(item.color));
            }
            //sfchart.ColorModel = new ChartColorModel
            //{
            //    Palette = ChartColorPalette.Custom,
            //    CustomBrushes = chartColorCollectionnew
            //};
            if (isRtl)
            {
                string chartTitle = string.IsNullOrEmpty(data.chartTitleAr) ? data.chartTitle : data.chartTitleAr;
                string primaryAxisTitle = string.IsNullOrEmpty(data.primaryAxisTitleAr) ? data.primaryAxisTitle : data.primaryAxisTitleAr;
                string secondaryAxisTitle = string.IsNullOrEmpty(data.secondaryAxisTitleAr) ? data.secondaryAxisTitle : data.secondaryAxisTitleAr;

                sfchart.Title = new ChartTitle() { Text = chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = secondaryAxisTitle }
                };
                DoughnutSeries PieSeries1 = new DoughnutSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo,
                    XBindingPath = "nameAr",
                    YBindingPath = "y",
                    EnableSmartLabels=true,
                    StartAngle = 75,
                    EndAngle = 435,
                    DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                    ConnectorLineType = ConnectorLineType.Bezier,
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = chartColorCollectionnew
                    }
                };
                PieSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Outer,
                        BackgroundColor = Color.Transparent,
                        FontSize = 12
                    }
                };
                sfchart.Series.Add(PieSeries1);
            }
            else
            {
                sfchart.Title = new ChartTitle() { Text = data.chartTitle };
                sfchart.PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    Title = new ChartAxisTitle { Text = data.primaryAxisTitle },
                    LabelRotationAngle = labelRotation_,
                    LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows,
                    MaximumLabels = 20,
                };
                sfchart.SecondaryAxis = new NumericalAxis()
                {
                    IsVisible = false,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift,
                    ShowMinorGridLines = false,
                    ShowMajorGridLines = false,
                    MajorTickStyle = new ChartAxisTickStyle { TickSize = 8 },
                    Title = new ChartAxisTitle { Text = data.secondaryAxisTitle }
                };
                DoughnutSeries PieSeries1 = new DoughnutSeries()
                {
                    TooltipTemplate = ToolTipTemplate1(),
                    ItemsSource = data.chartPlottingInfo,
                    XBindingPath = "name",
                    YBindingPath = "y",
                    EnableSmartLabels=true,
                    StartAngle = 75,
                    EndAngle = 435,
                    DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                    ConnectorLineType =ConnectorLineType.Bezier,
                    ColorModel = new ChartColorModel()
                    {
                        Palette = ChartColorPalette.Custom,
                        CustomBrushes = chartColorCollectionnew
                    }
                };
                PieSeries1.DataMarker = new ChartDataMarker
                {
                    LabelStyle = new DataMarkerLabelStyle
                    {
                        LabelPosition = DataMarkerLabelPosition.Outer,
                        BackgroundColor = Color.Transparent,
                        FontSize = 12
                    }
                };
                sfchart.Series.Add(PieSeries1);
            }
            return sfchart;
        }

        public void GetCurrentViewModel()
        {
            // var x = Setting.Settings.Dashboardtabs;
            foreach (DashboardChildPageViewModel viewModel in Setting.Settings.Dashboardtabs)
            {
                if (viewModel.currentpage)
                {
                    dashboardChildPageViewModel = viewModel;
                }
            }
        }
        public DataTemplate ToolTipTemplate1()
        {
            var dataTemplate = new DataTemplate(() =>
            {
                var lable = new Label
                {
                    Text = "Imports",
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    
                    Margin = new Thickness(0),
                    FontSize = 12
                };
                var boxView = new BoxView
                {
                    Color = Color.White,
                    HeightRequest = 0.75,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                var lable2 = new Label
                {
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    TextColor = Color.FromHex("#CCCCCC"),
                    FontAttributes = FontAttributes.Bold,
                    
                    FontSize = 12
                };
                lable2.SetBinding(Label.TextProperty, "x");
                var lable3 = new Label
                {
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    
                    FontSize = 12
                };
                lable3.SetBinding(Label.TextProperty, "y");
                var stlabes = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.FromHex("#404041"),
                    Spacing = 0,
                    Padding = 3,
                    Margin = 0,
                    Children = { lable2, lable3 }
                };
                var stmain = new StackLayout
                {
                    BackgroundColor = Color.FromHex("#404041"),
                    Padding = 3
                };
                stmain.Children.Add(lable);
                stmain.Children.Add(boxView);
                stmain.Children.Add(stlabes);
                return stmain;
            });
            return dataTemplate;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            currentPage = false;
            if (dashboardChildPageViewModel != null)
            {
                dashboardChildPageViewModel.currentpage = false;
            }
            
            MessagingCenter.Unsubscribe<DashboardChildPageViewModel, bool>(this, "RenderDesign");
        }

        private int CalculateHeight(Chart data)
        {
            try
            {
                var height = 500;

                int barsCount = 0;
                var seriesCount = data.chartPlottingInfo.Count;

                if (seriesCount > 0)
                {
                    for (int i = 0; i < seriesCount; i++)
                    {
                        barsCount += data.chartPlottingInfo[i].seriesData.Count;
                    }

                    height = barsCount * 20;
                }
                if (height < 500)
                {
                    height = 500;
                }
                return height;
            }
            catch
            {
                return 500;
            }
        }

        private double CalculateBarWidth(Chart data)
        {
            try
            {
                int barsCount = 0;
                var width = 0.8;
                var seriesCount = data.chartPlottingInfo.Count;
                
                if (seriesCount > 0)
                {
                    for (int i = 0; i < seriesCount; i++)
                    {
                        barsCount += data.chartPlottingInfo[i].seriesData.Count;
                    }


                    if (barsCount <= 3)
                    {
                        width = 0.1;
                    }
                   else if (barsCount <= 10)
                    {
                        width = 0.4;
                    }
                    /*
                    switch (barsCount)
                    {
                        case 1:
                            width = 0.1;
                            break;
                        case 2:
                            width = 0.2;
                            break;
                        case 3:
                            width = 0.3;
                            break;
                        case 4:
                            width = 0.4;
                            break;
                        case 5:
                            width = 0.5;
                            break;
                        case 6:
                            width = 0.6;
                            break;
                        case 7:
                            width = 0.7;
                            break;
                        case 8:
                            width = 0.8;
                            break;

                        default:
                            width = 0.8;
                            break;
                    }
                    */
                }
                return width;
            }
            catch
            {
                return 0.8;
            }
        }




        private void AdjustPlottingInfoa(List<ChartPlottingInfoModelApi> chartPlottingInfo)
        {
            try
            {
                HashSet<SeriesDataModelApi> tempSeries = new HashSet<SeriesDataModelApi>();
                foreach (var series in chartPlottingInfo)
                {
                    foreach (var item in series.seriesData)
                    {
                        tempSeries.Add(item);
                    }
                }
                foreach (var item in tempSeries)
                {
                    foreach (var series in chartPlottingInfo)
                    {
                        if (!series.seriesData.Exists(i => i.x == item.x))
                        {
                            SeriesDataModelApi model = new SeriesDataModelApi();
                            model.x = item.x;
                            model.x_Ar = item.x_Ar;
                            model.y = null;
                            series.seriesData.Add(model);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private int GetChartHeight(Chart data)
        {
            int height_ = 0;
            int minCount = data.chartPlottingInfo.Count - 3;
            if (minCount > 0 )
            {
                int extra = minCount > 3 ? 20 : 50;
                height_ = minCount * extra;
            }
            // int newHeight = ((data.chartPlottingInfo.Count / 3) - 2) * 50;
            //  height_ = newHeight > 0 ? newHeight : 0;
            int res = height_ + _chartHieght;
            return res > 1000? 1000 : res;
        }

    }
}
