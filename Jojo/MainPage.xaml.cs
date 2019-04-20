using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using CrossGraphics;

namespace Jojo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        SensorSpeed speed = SensorSpeed.UI;
        IGraphics g;
        public MainPage()
        {
            InitializeComponent();
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
            g.BeginEntity(this);
        }
        void OnButtonClicked(object sender, EventArgs args)
        {
            button.RotateTo(360);
        }
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            g.Clear(new CrossGraphics.Color (0, 0, 0));
            var data = e.Reading;
            X.Text = $"X: {data.Acceleration.X}";
            Y.Text = $"Y: {data.Acceleration.Y}";
            Z.Text = $"Z: {data.Acceleration.Z}";
            g.DrawLine(0, 0, data.Acceleration.X * 100, data.Acceleration.Y * 100, 1);
            g.FillOval(data.Acceleration.X * 100 - 12, data.Acceleration.Y * 100 - 10, 10, 10);
        }
    }
}
