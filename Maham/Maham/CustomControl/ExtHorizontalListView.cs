using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    public class HorizontalListView : Grid
    {
        private ICommand _innerSelectedCommand;
        private readonly ScrollView _scrollView;
        private readonly StackLayout _itemsStackLayout;

        public event EventHandler SelectedItemChanged;

        public StackOrientation ListOrientation { get; set; }

        public double Spacing { get; set; }

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(HorizontalListView), null);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalListView), default(IEnumerable<object>), BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(HorizontalListView), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public static readonly BindableProperty ScrollViewProperty =
             BindableProperty.Create("ScrollView", typeof(ScrollView), typeof(HorizontalListView), default(ScrollView));

        public ScrollView ScrollView
        {
            get { return (ScrollView)GetValue(ScrollViewProperty); }
            set { SetValue(ScrollViewProperty, value); }
        }

        public static readonly BindableProperty StackLayoutProperty =
            BindableProperty.Create("StackLayout", typeof(StackLayout), typeof(HorizontalListView), default(StackLayout));

        public StackLayout StackLayout
        {
            get { return (StackLayout)GetValue(StackLayoutProperty); }
            set { SetValue(StackLayoutProperty, value); }
        }

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (HorizontalListView)bindable;
            itemsLayout.SetItems();
        }

        public HorizontalListView()
        {
            int _CurrentLanguage = new Helpers.Helper().CurrentLanguage();
            _itemsStackLayout = new StackLayout
            {
                BackgroundColor = BackgroundColor,
                Padding = Padding,
                Spacing = Spacing,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FlowDirection = _CurrentLanguage == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft,

            };
            _scrollView = new ScrollView()
            {
                FlowDirection = FlowDirection.LeftToRight,
                BackgroundColor = BackgroundColor,
                Content = _itemsStackLayout,
            };

            Children.Add(_scrollView);
            StackLayout = _itemsStackLayout;
            ScrollView = _scrollView;
            //ScrollView.FlowDirection = FlowDirection.LeftToRight;
        }

        protected virtual void SetItems()
        {
            _itemsStackLayout.Children.Clear();
            _itemsStackLayout.Spacing = Spacing;

            _innerSelectedCommand = new Command<Xamarin.Forms.View>(view =>
            {
                SelectedItem = view.BindingContext;
                SelectedItem = null; // Allowing item second time selection
            });

            _itemsStackLayout.Orientation = ListOrientation;
            _scrollView.Orientation = ListOrientation == StackOrientation.Horizontal
                ? ScrollOrientation.Horizontal
                : ScrollOrientation.Vertical;

            if (ItemsSource == null)
            {
                return;
            }

            foreach (var item in ItemsSource)
            {
                _itemsStackLayout.Children.Add(GetItemView(item));
            }

            _itemsStackLayout.BackgroundColor = BackgroundColor;
            SelectedItem = null;
        }

        protected virtual Xamarin.Forms.View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as Xamarin.Forms.View;

            if (view == null)
            {
                return null;
            }

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = _innerSelectedCommand,
                CommandParameter = view
            };

            AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(Xamarin.Forms.View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<Xamarin.Forms.View>;

            if (layout == null)
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                AddGesture(child, gesture);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (HorizontalListView)bindable;
            if (newValue == oldValue && newValue != null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, EventArgs.Empty);

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }
    }
}
