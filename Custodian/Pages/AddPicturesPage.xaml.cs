using Custodian.ActivityLog;
using System.Collections.ObjectModel;

namespace Custodian.Pages;

public partial class AddPicturesPage : ContentPage
{
    private ObservableCollection<string> images = new ObservableCollection<string>();

	public AddPicturesPage(string routeTitle)
	{
		InitializeComponent();
        lblTitle.Text=routeTitle;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    private void CameraButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            
            _ = TakePictureFromCamera();
            pictures.ItemsSource = images;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private async Task TakePictureFromCamera()
    {
        try
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
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    private void DeleteImage_Clicked(object sender, EventArgs e)
    {
        try
        {
            var args = e as TappedEventArgs;
            if (args != null)
            {
                var picture = args.Parameter as Image;
                string filepath = picture.Source.ToString();
                string path = filepath.Remove(0, 6);
                images.Remove(path);
                pictures.ItemsSource = images;
            }
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}