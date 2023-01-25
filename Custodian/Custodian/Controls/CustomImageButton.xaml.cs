namespace Custodian.Controls;

public partial class CustomImageButton : Frame
{
    public CustomImageButton()
    {
       InitializeComponent();
        img.Source = this.ImageFile;
        lbl.Text = this.ButtonText;
    }

    public static readonly BindableProperty ButtonTextProperty =
              BindableProperty.Create(
                  nameof(ButtonText),
                  typeof(string),
                  typeof(CustomImageButton),string.Empty, defaultBindingMode: BindingMode.TwoWay);

    public string ButtonText
    {
        get { return (string)GetValue(ButtonTextProperty); }
        set { SetValue(ButtonTextProperty, value); }
    }

    public static readonly BindableProperty ImageFileProperty =
              BindableProperty.Create(
                  nameof(ImageFile),
                  typeof(ImageSource),
                  typeof(CustomImageButton), null, defaultBindingMode: BindingMode.TwoWay);

    public ImageSource ImageFile
    {
        get { return (ImageSource)GetValue(ImageFileProperty); }
        set { SetValue(ImageFileProperty, value); }
    }
    
    
}
