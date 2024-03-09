using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    class CheckBoxControl : ContentView
    {
        protected Grid ContentGrid;
        protected ContentView ContentContainer;
        public Label TextContainer;
        //protected Image ImageContainer;
        protected Image SVGContainer;

        public CheckBoxControl()
        {
            var TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += TapGestureOnTapped;
            GestureRecognizers.Add(TapGesture);

            ContentGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });
            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            SVGContainer = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            SVGContainer.HeightRequest = 25;
            SVGContainer.WidthRequest = 25;

            ContentGrid.Children.Add(SVGContainer);

            ContentContainer = new ContentView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Grid.SetColumn(ContentContainer, 1);

            TextContainer = new Label
            {
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            ContentContainer.Content = TextContainer;

            ContentGrid.Children.Add(ContentContainer);

            base.Content = ContentGrid;

            this.SVGContainer.Source = ImageUncheck;
            this.BackgroundColor = Color.Transparent;
        }

        public static BindableProperty CheckedProperty = BindableProperty.Create(
            propertyName: "Checked",
            returnType: typeof(Boolean?),
            declaringType: typeof(CheckBoxControl),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: CheckedValueChanged);

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(String),
            declaringType: typeof(CheckBoxControl),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextValueChanged);
        public static BindableProperty ImageCheckedProperty = BindableProperty.Create(
           propertyName: nameof(ImageChecked),
           returnType: typeof(String),
           declaringType: typeof(CheckBoxControl),
           defaultValue: null,
           defaultBindingMode: BindingMode.TwoWay
           );
        public static BindableProperty ImageUncheckProperty = BindableProperty.Create(
          propertyName: nameof(ImageUncheck),
          returnType: typeof(String),
          declaringType: typeof(CheckBoxControl),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay
          );

        public string ImageChecked

        {

            get { return (string)GetValue(ImageCheckedProperty); }

            set { SetValue(ImageCheckedProperty, value);
                OnPropertyChanged();
            }

        }
        public string ImageUncheck

        {

            get { return (string)GetValue(ImageUncheckProperty); }

            set { SetValue(ImageUncheckProperty, value);
                OnPropertyChanged() ; }

        }

        public Boolean? Checked
        {
            get
            {
                if (GetValue(CheckedProperty) == null)
                    return null;
                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue == true)
                ((CheckBoxControl)bindable).SVGContainer.Source = ((CheckBoxControl)bindable).ImageChecked;
            else
                ((CheckBoxControl)bindable).SVGContainer.Source = ((CheckBoxControl)bindable).ImageUncheck;
        }

        public event EventHandler CheckedChanged;
        private void RaiseCheckedChanged()
        {
            if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }

        private Boolean _IsEnabled = true;
        public Boolean IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged();
                this.Opacity = value ? 1 : .5;
                base.IsEnabled = value;
            }
        }

        public event EventHandler Clicked;
        private void TapGestureOnTapped(object sender, EventArgs eventArgs)
        {
            if (IsEnabled)
            {
                Checked = !Checked;
                if (Clicked != null)
                    Clicked(this, new EventArgs());
            }
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CheckBoxControl)bindable).TextContainer.Text = (String)newValue;
        }

        public event EventHandler TextChanged;
        private void RaiseTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, EventArgs.Empty);
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
                RaiseTextChanged();
            }
        }

    }
}