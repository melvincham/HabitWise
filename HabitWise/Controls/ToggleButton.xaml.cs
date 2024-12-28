using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HabitWise.Controls
{
    public partial class ToggleButton : ContentView
    {
        Image ToggleImage = new Image
        {
            Aspect = Aspect.AspectFill
        };

        public ToggleButton()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapped;
            MainGrid.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
            nameof(IsToggled),
            typeof(bool),
            typeof(ToggleButton),
            false,
            propertyChanged: OnToggledChanged
        );
        public static readonly BindableProperty ToggleCommandProperty = BindableProperty.Create(
            nameof(ToggleCommand),
            typeof(Command),
            typeof(ToggleButton),
            default(Command)
        );
        public static readonly BindableProperty ToggledImageProperty = BindableProperty.Create(
           nameof(ToggledImage),
           typeof(ImageSource),
           typeof(ToggleButton),
           default(ImageSource),
           propertyChanged: OnToggledImageChanged
        );
        public static readonly BindableProperty UnToggledImageProperty = BindableProperty.Create(
           nameof(UnToggledImage),
           typeof(ImageSource),
           typeof(ToggleButton),
           default(ImageSource),
           propertyChanged: OnUnToggledImageChanged
        );

        public static readonly BindableProperty ToggleColorProperty = BindableProperty.Create(
            nameof(ToggleColor),
            typeof(Color),
            typeof(ToggleButton),
            Colors.Yellow, 
            propertyChanged: OnToggleColorChanged
        );

        public static readonly BindableProperty ToggleBackgroundColorProperty = BindableProperty.Create(
            nameof(ToggleBackgroundColor),
            typeof(Color),
            typeof(ToggleButton),
            Colors.Transparent, 
            propertyChanged: OnToggleBackgroundColorChanged
        );

        public static readonly BindableProperty ToggleStrokeShapeProperty = BindableProperty.Create(
            nameof(ToggleStrokeShape),
            typeof(Shape),
            typeof(ToggleButton),
            null,
            propertyChanged: OnToggleStrokeShapeChanged
        );

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        public ImageSource ToggledImage
        {
            get => (ImageSource)GetValue(ToggledImageProperty);
            set => SetValue(ToggledImageProperty, value);
        }

        public ImageSource UnToggledImage
        {
            get => (ImageSource)GetValue(UnToggledImageProperty);
            set => SetValue(UnToggledImageProperty, value);
        }

        public Command ToggleCommand
        {
            get => (Command)GetValue(ToggleCommandProperty);
            set => SetValue(ToggleCommandProperty, value);
        }

        public Color ToggleColor
        {
            get => (Color)GetValue(ToggleColorProperty);
            set => SetValue(ToggleColorProperty, value);
        }

        public Color ToggleBackgroundColor
        {
            get => (Color)GetValue(ToggleBackgroundColorProperty);
            set => SetValue(ToggleBackgroundColorProperty, value);
        }

        public Shape ToggleStrokeShape   
        {
            get => (Shape)GetValue(ToggleStrokeShapeProperty);
            set => SetValue(ToggleStrokeShapeProperty, value);
        }

        public static void OnToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton)
            {
                toggleButton.AnimateToggle();
                toggleButton.ExecuteToggleCommand();
            }
        }

        private static void OnToggledImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton && newValue is ImageSource newSource)
            {
                toggleButton.ToggledImage = newSource; 
                toggleButton.AnimateToggle();
            }
        }

        private static void OnUnToggledImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton && newValue is ImageSource newSource)
            {
                toggleButton.UnToggledImage = newSource;
                toggleButton.AnimateToggle();
            }
        }

        private static void OnToggleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton && newValue is Color newColor)
            {
                toggleButton.ToggleImageBorder.BackgroundColor = newColor;
            }
        }

        private static void OnToggleBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton && newValue is Color newColor)
            {
                toggleButton.MainBorder.BackgroundColor = newColor;
            }
        }

        private static void OnToggleStrokeShapeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ToggleButton toggleButton && newValue is Shape newShape)
            {
                toggleButton.MainBorder.StrokeShape = newShape;
            }
        }

        private void UpdateImageSource()
        {
           ToggleImage.Source =  IsToggled ? ToggledImage : UnToggledImage;
        }

        private void AnimateToggle()
        {
            UpdateImageSource();
            ToggleImageBorder.Content = ToggleImage;
            var targetColumn = IsToggled ? 1 : 0;
            //await ToggleImage.TranslateTo(targetColumn == 1 ? 80 / 2 : 0, 0, 200);
            Grid.SetColumn(ToggleImageBorder, targetColumn);
        }

        private void ExecuteToggleCommand()
        {
            if (ToggleCommand?.CanExecute(IsToggled) == true)
            {
                ToggleCommand.Execute(IsToggled);
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            IsToggled = !IsToggled;
        }
    }
}
