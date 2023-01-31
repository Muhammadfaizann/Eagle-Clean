using System.Collections.ObjectModel;

namespace Custodian.Pages;

public partial class AddPicturesPage : ContentPage
{
    private ObservableCollection<string> images = new ObservableCollection<string>();

	public AddPicturesPage()
	{
		InitializeComponent();
    }

    private void CameraButton_Clicked(object sender, EventArgs e)
    {
        _ = TakePictureFromCamera();
        pictures.ItemsSource = images;
    }

    private async Task TakePictureFromCamera()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                images.Add(photo.FullPath);
                pictures.ItemsSource = images;
            }
        }
    }
    private void DeleteImage_Clicked(object sender, EventArgs e)
    {
        var args = e as TappedEventArgs;
        if (args != null)
        {
            var picture = args.Parameter as Image;
            string filepath = picture.Source.ToString();
            string path=filepath.Remove(0, 6);
            images.Remove(path);
            pictures.ItemsSource = images;
        }
    }
}