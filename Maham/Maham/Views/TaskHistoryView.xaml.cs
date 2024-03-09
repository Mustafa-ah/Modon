using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maham.CustomControl;
using Maham.Models;
using Maham.Setting;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskHistoryView : ContentPage
	{
        bool isRTL;

        public TaskHistoryView ()
		{
			InitializeComponent ();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            RootStackLayout.FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;

            MessagingCenter.Subscribe<TaskHistoryViewModel, List<ChangePerDate>>(this, "DataLoaded", (sender, arg) => {
                if (arg != null)
                {
                    DrawTaskHistroy(arg);
                }
            });
            isRTL = Settings.IsRtl;
        }


        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<TaskHistoryViewModel, List<ChangePerDate>>(this, "DataLoaded");
            base.OnDisappearing();
        }

        private void DrawTaskHistroy(List<ChangePerDate> changePerDates)
        {
            try
            {
                StackLayout rootStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                foreach (var change in changePerDates)
                {
                    if (change.ChangesPerTime == null)
                    {
                        continue;
                    }
                    RoundedView dateView = new RoundedView
                    {
                        BorderRadius = 15,
                        BorderThickness = 1,
                        BackgroundColor = Color.White,
                        Margin = new Thickness(10, 5)
                    };
                    int containerHeight = 60 + (60 * change.ChangesPerTime.Count);

                    StackLayout containerLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 0,
                        HeightRequest = containerHeight
                    };
                   
                    Label dateLable = new Label
                    {
                        Text = change.Day,
                        TextColor = Color.FromHex("#4DABF6"),
                        FontSize = 30,
                        VerticalTextAlignment = TextAlignment.End,
                        HorizontalTextAlignment = TextAlignment.Center
                    };

                    string month_ = isRTL ? change.MonthAr : change.MonthEn;
                    Label monthLable = new Label
                    {
                        Text = month_,
                        TextColor = Color.FromHex("#4DABF6"),
                        FontSize = 10,
                        VerticalTextAlignment = TextAlignment.Start,
                        HorizontalTextAlignment = TextAlignment.Center
                    };

                    AbsoluteLayout absoluteDate = new AbsoluteLayout()
                    {
                        HeightRequest = 50,
                        WidthRequest = 60
                    };
                    AbsoluteLayout.SetLayoutBounds(dateLable, new Rectangle(0, 0, 50, 40));
                    absoluteDate.Children.Add(dateLable);

                    AbsoluteLayout.SetLayoutBounds(monthLable, new Rectangle(0, 30, 50, 20));
                    absoluteDate.Children.Add(monthLable);

                    containerLayout.Children.Add(absoluteDate);


                    // adding time changes
                    int counter_ = 0;
                    foreach (var timeChange in change.ChangesPerTime)
                    {
                        counter_++;
                        //  string img = GetImage(timeChange.ChangeType);
                        StackLayout timeLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 1,
                            HeightRequest = 60
                        };

                        AbsoluteLayout linedCircle = new AbsoluteLayout()
                        {
                            WidthRequest = 35
                        };

                        
                        StackLayout line = new StackLayout()
                        {
                            WidthRequest =1,
                            BackgroundColor = Color.LightGray
                        };

                        //last one
                        if (counter_ == change.ChangesPerTime.Count)
                        {
                            AbsoluteLayout.SetLayoutBounds(line, new Rectangle(24, 0, 1, 25));
                        }
                        else
                        {
                            AbsoluteLayout.SetLayoutBounds(line, new Rectangle(24, 0, 1, 60));
                        }
                        
                        linedCircle.Children.Add(line);

                        Image imageCircle = new Image
                        {
                            HeightRequest = 16,
                            WidthRequest = 16,
                            Source = ImageSource.FromFile("blueCycle.png"),
                            VerticalOptions = LayoutOptions.Center
                        };
                        AbsoluteLayout.SetLayoutBounds(imageCircle, new Rectangle(17, 25, 16, 16));
                        linedCircle.Children.Add(imageCircle);



                        timeLayout.Children.Add(linedCircle);

                        Label timeLable = new Label
                        {
                            Text = timeChange.ChangeTime,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Color.FromHex("#4DABF6"),
                            FontSize = 12,
                            WidthRequest = 65,
                            LineBreakMode = LineBreakMode.NoWrap,
                            VerticalTextAlignment = TextAlignment.Start,
                            HorizontalTextAlignment = TextAlignment.Center,
                            MinimumWidthRequest = 65
                        };
                        timeLayout.Children.Add(timeLable);

                        string mchangeTxt_ = isRTL ? timeChange.ChangeAr : timeChange.ChangeEn;
                        Label detailsLable = new Label
                        {
                            Text = mchangeTxt_,
                            TextColor = Color.FromHex("#313D57"),
                            FontSize = 12,
                            VerticalOptions = LayoutOptions.Center,
                            VerticalTextAlignment = TextAlignment.Start
                        };
                        timeLayout.Children.Add(detailsLable);

                        containerLayout.Children.Add(timeLayout);
                    }
                    dateView.Content = containerLayout;
                    rootStack.Children.Add(dateView);
                }

              

                HistoryScroll.Content = rootStack;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    {"notificationpageviewmodel", "loadpage"},
                };
                Crashes.TrackError(exception, properties);
            }
        }

        private string GetImage(int typ)
        {
            switch (typ)
            {
                case 1:
                    return "plus_ic.png";
                case 2:
                    return "pin_ic.png";
                case 3:
                    return "repaired_ic.png";
                default:
                    return "notfiy.png";
            }
        }
	}
}