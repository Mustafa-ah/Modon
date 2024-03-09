using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Maham.CustomControl
{
	public class DisconnectedView : ContentView
	{
        string msg;
        private Label MsgLable;
        public DisconnectedView ()
		{
            msg = Maham.Resources.AppResource.notConnectedMsg;

            MsgLable = new Label
            {
                Text = msg,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
          
			Content = new StackLayout {
                BackgroundColor = Color.Black,
                Opacity = 0.8,
                HeightRequest = 60,
                Children = {
					MsgLable
				}
			};
		}

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
            }
        }

        public static BindableProperty TextProperty = BindableProperty.Create(
           propertyName: "Text",
           returnType: typeof(String),
           declaringType: typeof(DisconnectedView),
           defaultValue: null,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: TextValueChanged);

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((DisconnectedView)bindable).MsgLable.Text = (String)newValue;
        }
    }
}