using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    public class ExtViewCell : ViewCell
    {
        private static Dictionary<Xamarin.Forms.ListView, State> cache = new Dictionary<Xamarin.Forms.ListView, State>();
        private class State
        {
            internal void RaiseCellSwiped(Xamarin.Forms.ListView container, ExtViewCell sender, bool on)
            {
                CellSwiped?.Invoke(container, sender, on);
                On = on;
            }
            public bool On { get; private set; }
            public delegate void CellSwipedEventHandler(Xamarin.Forms.ListView container, ExtViewCell sender, bool on);
            public event CellSwipedEventHandler CellSwiped;
        }

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
            nameof(DeleteCommand),
            typeof(ICommand),
            typeof(ExtViewCell));


        public static readonly BindableProperty DeleteCommandParameterProperty = BindableProperty.Create(
            nameof(DeleteCommandParameter),
            typeof(object),
            typeof(ExtViewCell));

        public object DeleteCommandParameter
        {
            get => GetValue(DeleteCommandParameterProperty);
            set => SetValue(DeleteCommandParameterProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly BindableProperty EditCommandProperty = BindableProperty.Create(
        nameof(EditCommand),
        typeof(ICommand),
        typeof(ExtViewCell));


        public static readonly BindableProperty EditCommandParameterProperty = BindableProperty.Create(
            nameof(EditCommandParameter),
            typeof(object),
            typeof(ExtViewCell));

        public object EditCommandParameter
        {
            get => GetValue(EditCommandParameterProperty);
            set => SetValue(EditCommandParameterProperty, value);
        }

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        //----------------------------------
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
       nameof(TapCommand),
       typeof(ICommand),
       typeof(ExtViewCell));


        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(ExtViewCell));

        public object TapCommandParameter
        {
            get => GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }
        //-----------------

        public static readonly BindableProperty CustomViewProperty = BindableProperty.Create(
             nameof(CustomView),
             typeof(View),
             typeof(ExtViewCell),
             new Grid(), propertyChanged: OnValueChanged);
       

        private StackLayout _stDelete;
        private StackLayout _stEdit;

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ExtViewCell viewCell)
            {
                viewCell.DrawGrid();
            }
        }

        public View CustomView
        {

            get => (View)GetValue(CustomViewProperty);
            set => SetValue(CustomViewProperty, value);
        }

        private ICommand SwipeLeftCommand { get; set; }
        private ICommand SwipeRightCommand { get; set; }
        private ICommand CellTapCommand { get; set; }

        private StackLayout DeleteStack { get; set; }
        private StackLayout EditStack { get; set; }
        private Xamarin.Forms.ListView ParentListView { get; set; }
        public ExtViewCell()
        {

        }
        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (this.Parent is Xamarin.Forms.ListView listView)
            {
                this.ParentListView = listView;
                if (!cache.ContainsKey(listView))
                    cache.Add(listView, new State());
                State state = cache[listView];
                state.CellSwiped += State_CellSwiped;
            }
        }

        private void State_CellSwiped(Xamarin.Forms.ListView container, ExtViewCell sender, bool on)
        {
            bool isOn = cache[container].On;
            if (this != sender && isOn && DeleteStack.IsVisible && EditStack.IsVisible)
            {
                DeleteStack.IsVisible = false;
                EditStack.IsVisible = false;
            }
        }

        private void OnSwipeRight()
        {
            //var animate = new Animation(x=>_btnDelete.WidthRequest = x, _btnDelete.WidthRequest, 0, easing: Easing.SinInOut);
            //animate.Commit(_btnDelete, "dtrhtf");
            _stDelete.IsVisible = false;
            _stEdit.IsVisible = false;

            if (cache.ContainsKey(ParentListView))
            {
                cache[ParentListView].RaiseCellSwiped(ParentListView, this, _stDelete.IsVisible);
                cache[ParentListView].RaiseCellSwiped(ParentListView, this, _stEdit.IsVisible);
            }

        }

        private void OnSwipeLeft()
        {
            _stDelete.IsVisible = true;
            _stEdit.IsVisible = true;
            if (cache.ContainsKey(ParentListView))
            {
                cache[ParentListView].RaiseCellSwiped(ParentListView, this, _stDelete.IsVisible);
                cache[ParentListView].RaiseCellSwiped(ParentListView, this, _stEdit.IsVisible);
            }
        }

        private void OnCellTapped()
        {
            TapCommand?.Execute(TapCommandParameter);
        }

        private void DrawGrid()
        {
            if (CustomView != null)
            {
                //Delete Button with stack
                Image _btnDelete = new Image
                {
                    Source = "DeleteICon",
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };

                _stDelete = new StackLayout
                {
                    VerticalOptions=LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    WidthRequest = 45,
                    Children = { _btnDelete },
                    IsVisible = false,
                    BackgroundColor=Color.Red
                };
                DeleteStack = _stDelete;
                var tapGestureRecognizerDelete = new TapGestureRecognizer();
                tapGestureRecognizerDelete.Tapped += BtnDelete_Clicked;
                _stDelete.GestureRecognizers.Add(tapGestureRecognizerDelete);

                //Edit Button with stack
                Image _btnEdit = new Image
                {
                    Source = "editicon.png",
                    WidthRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };

                _stEdit = new StackLayout
                {
                    BackgroundColor = Color.FromHex("#2196F3"),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    WidthRequest = 45,
                    Children = { _btnEdit },
                    IsVisible = false
                };
                EditStack = _stEdit;
                var tapGestureRecognizerEdit = new TapGestureRecognizer();
                tapGestureRecognizerEdit.Tapped += BtnEdit_Clicked;
                _stEdit.GestureRecognizers.Add(tapGestureRecognizerEdit);
                
                var customGrid = new Grid()
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    BackgroundColor = Color.Transparent,
                    ColumnDefinitions =
                {
                    new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Auto)},
                     new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Auto)},
                },
                    RowDefinitions =
                {
                    new RowDefinition(){Height = new GridLength(1,GridUnitType.Star)}
                }
                };
                customGrid.Children.Add(CustomView, 0, 0);
                customGrid.Children.Add(_stEdit, 1, 0);
                customGrid.Children.Add(_stDelete, 2, 0);

                //customGrid.BindingContext = this.View.BindingContext;
                this.View = customGrid;
                SwipeLeftCommand = new Command(OnSwipeLeft);
                Vapolia.Lib.Ui.Gesture.SetSwipeLeftCommand(View, SwipeLeftCommand);
                SwipeRightCommand = new Command(OnSwipeRight);
                Vapolia.Lib.Ui.Gesture.SetSwipeRightCommand(View, SwipeRightCommand);
                CellTapCommand = new Command(OnCellTapped);
                Vapolia.Lib.Ui.Gesture.SetTapCommand(View, CellTapCommand);
            }
            else
            {
                DeleteStack = null;
                EditStack = null;
                this.View = null;
            }
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            DeleteCommand?.Execute(DeleteCommandParameter);
        }
        private void BtnEdit_Clicked(object sender, EventArgs e)
        {
            EditCommand?.Execute(EditCommandParameter);
        }

    }
}
