
using System.Diagnostics;


namespace ReproductionProjectBug15397_ImageSourceFromStream;

public partial class MainPage : ContentPage
{

    byte[] imageBytes;

    public MainPage()
	{
		InitializeComponent();
        Init();

    }

    async void Init()
    {
        string imageUrl = "https://via.placeholder.com/500"; // Replace with your example URL
        imageBytes = await DownloadImage(imageUrl);

        Device.BeginInvokeOnMainThread(() =>
        {
            MemoryStream imageDecodeStream = new(imageBytes);
            image.Source = ImageSource.FromStream(() => imageDecodeStream);
        });

       
    }


    /*
     * Solution
    void Refresh()
    {

        Device.BeginInvokeOnMainThread(() =>
        {
            MemoryStream imageDecodeStream = new(imageBytes);
            image.Source = ImageSource.FromStream(() => imageDecodeStream);
        });


    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (image != null && image.Source != null)
        {
            image.Source = null;
            Refresh();
        }

    }
    */

    static async Task<byte[]> DownloadImage(string imageUrl)
    {
        byte[] imageBytes;

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex);
                return null;
            }
        }

        return imageBytes;
    }
}

