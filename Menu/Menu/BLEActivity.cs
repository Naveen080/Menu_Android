using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Xsl.Runtime;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi.P2p;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using IAdapter = Plugin.BLE.Abstractions.Contracts.IAdapter;

namespace Menu
{
    [Activity(Label = "BLEActivity")]
    public class BLEActivity : AppCompatActivity
    {
        ArrayAdapter<string> adapter_lv;
        IAdapter adapter;
        IDevice device;
        IBluetoothLE ble;
        ObservableCollection<IDevice> deviseList;
        ObservableCollection<string> deviseHashCodeList;
        Button btnStatus_Clicked, btnScan_Clicked, btnConnect_Clicked,btnGetServices_Clicked;
        Button btnGetcharacters_Clicked, btnGetRW_Clicked;
        ListView lv;

        IList<ICharacteristic> Characteristics;
        ICharacteristic Characteristic;
        System.Collections.Generic.IReadOnlyList<IService> Services;
        IService Service;
        System.Collections.Generic.IReadOnlyList<IDescriptor> descriptors;
        IDescriptor descriptor;
        private static int PERMISSION_REQUEST_COARSE_LOCATION = 456;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layoutble);

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviseList = new ObservableCollection<IDevice>();
            deviseHashCodeList = new ObservableCollection<string>();

            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                RequestPermissions(new System.String[] { Manifest.Permission.AccessCoarseLocation }, PERMISSION_REQUEST_COARSE_LOCATION);
            }

            btnStatus_Clicked = (Button)FindViewById(Resource.Id.btnStatus_Clicked);
            btnStatus_Clicked.Click += BtnStatusClicked;

            btnScan_Clicked = (Button)FindViewById(Resource.Id.btnScan_Clicked);
            btnScan_Clicked.Click += BtnScanClicked;

            btnConnect_Clicked = (Button)FindViewById(Resource.Id.btnConnect_Clicked);
            btnConnect_Clicked.Click += BtnConnectClicked;

           

            btnGetServices_Clicked = (Button)FindViewById(Resource.Id.btnGetServices_Clicked);
            btnGetServices_Clicked.Click += BtnGetServicesClicked;

            btnGetcharacters_Clicked = (Button)FindViewById(Resource.Id.btnGetcharacters_Clicked);
            btnGetcharacters_Clicked.Click += BtnGetCharactersClicked;

            

            btnGetRW_Clicked = (Button)FindViewById(Resource.Id.btnGetRW_Clicked);
            btnGetRW_Clicked.Click += BtnGetRWClicked;

            

            lv = (ListView)FindViewById(Resource.Id.lv);
            adapter_lv = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, deviseHashCodeList);

            lv.ItemClick += OnDeviceClicked;
        }

        private void OnDeviceClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (deviseList[e.Position] == null)
            {
                return;
            }
            device = deviseList[e.Position];
            Toast.MakeText(this, "Device: " + device.NativeDevice.ToString(), ToastLength.Short).Show();
        }

        private async void BtnUpdateClicked(object sender, EventArgs e)
        {
            if (Characteristic != null)
            {
                Characteristic.ValueUpdated += (o, args) =>
                {
                    var bytes = args.Characteristic.Value;
                };
                await Characteristic.StartUpdatesAsync();
                this.RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Updated values ", ToastLength.Long).Show();
                });
            }
            else
            {
                Toast.MakeText(this, "Cannot update,Charater is Empty", ToastLength.Long).Show();
            }

        }

        private async void BtnGetRWClicked(object sender, EventArgs e)
        {
            if (Characteristic != null)
            {
                var bytesval = await Characteristic.ReadAsync();
                await Characteristic.WriteAsync(bytesval);

                this.RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Data length: " + bytesval.Length.ToString(), ToastLength.Long).Show();
                });
            }
            else
            {
                Toast.MakeText(this, "Character is Empty", ToastLength.Long).Show();
            }
        }

        private async void BtnDescRWClicked(object sender, EventArgs e)
        {
            if (descriptor != null)
            {
                var bytes = await descriptor.ReadAsync();
                await descriptor.WriteAsync(bytes);
                this.RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Read and write descriptor: " + bytes.ToString(), ToastLength.Long).Show();
                });
            }
            else
            {
                Toast.MakeText(this, "Descriptor is Empty", ToastLength.Long).Show();
            }
        }

        private async void BtnDescriptorsClicked(object sender, EventArgs e)
        {
            if (Characteristic != null)
            {
                descriptors = await Characteristic.GetDescriptorsAsync();

                descriptor = (IDescriptor)Characteristic.GetDescriptorAsync(Guid.Parse("guid"));
                this.RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Descriptors: " + descriptors + "Descriptor: " + descriptor, ToastLength.Long).Show();
                });
            }
            else
            {
                Toast.MakeText(this, "Characteristic is Empty", ToastLength.Long).Show();
            }
        }

        private async void BtnGetCharactersClicked(object sender, EventArgs e)
        {
            if (Service != null)
            {
                
                var serviceUuid = "0000aaa1-0000-1000-8000-aabbccddeeff";
                Guid idGuid = Guid.Parse(serviceUuid);
                Characteristic = await Service.GetCharacteristicAsync(idGuid);
                if (Characteristic != null)
                {
                    this.RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Characteristic: " + Characteristic.Uuid.ToString(), ToastLength.Long).Show();
                    });
                }
                else
                {
                    this.RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Characteristic is empty", ToastLength.Long).Show();
                    });
                }
            }
            else
            {
                Toast.MakeText(this, "Service is Empty", ToastLength.Long).Show();
            }
        }


        private async void BtnGetServicesClicked(object sender, EventArgs e)
        {
           
            var serviceUuid = "0000aaa0-0000-1000-8000-aabbccddeeff";
            Service = await device.GetServiceAsync(Guid.Parse(serviceUuid));

            if (Service != null)
            {
                this.RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Service: " + Service.Id.ToString(), ToastLength.Long).Show();
                });
            }
            else
            {
                Toast.MakeText(this, "Service is Empty", ToastLength.Long).Show();
            }
        }

        private async void BtnKnowConnectClicked(object sender, EventArgs e)
        {
            try
            {
                await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));
            }
            catch (System.Exception exp)
            {
                Toast.MakeText(this, "Notice: " + exp.Message, ToastLength.Long).Show();
            }
        }

        private async void BtnConnectClicked(object sender, EventArgs e)
        {
            try
            {
                if (device != null && !ble.Adapter.IsScanning)
                {
                    try
                    {
                        if (device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                        {
                            this.RunOnUiThread(() =>
                            {
                                Toast.MakeText(this, "Connection sucessfull!!", ToastLength.Long).Show();
                            });
                        }
                        else
                        {
                            var token = new CancellationTokenSource();
                            ConnectParameters connectParams = new ConnectParameters(false);
                            await adapter.StopScanningForDevicesAsync();
                            await adapter.ConnectToDeviceAsync(device, connectParams, token.Token);
                            token.Cancel();
                            this.RunOnUiThread(() =>
                            {
                                Toast.MakeText(this, "Connection sucessfull!!", ToastLength.Long).Show();
                            });
                        }
                    }
                    catch (System.Exception exp)
                    {
                        this.RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "Notice in connect: " + exp.Message, ToastLength.Long).Show();
                        });
                    }
                }
                else
                {
                    Toast.MakeText(this, "No Device selected", ToastLength.Long).Show();
                }
            }
            catch (System.Exception exp)
            {
                Toast.MakeText(this, "Notice: " + exp.Message, ToastLength.Long).Show();
            }
        }

        private async void BtnScanClicked(object sender, EventArgs e)
        {
            try
            {

                deviseList.Clear();
                deviseHashCodeList.Clear();
                adapter.DeviceDiscovered += (s, a) =>
                {
                    if (a.Device != null)
                    {
                        deviseList.Add(a.Device);
                        deviseHashCodeList.Add(a.Device.NativeDevice.ToString());
                    }

                    this.RunOnUiThread(() =>
                    {
                        adapter_lv = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, deviseHashCodeList);
                        lv.Adapter = adapter_lv;
                    });
                };
                if (!ble.Adapter.IsScanning)
                {
                    await adapter.StartScanningForDevicesAsync();
                    this.RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Scanning", ToastLength.Long).Show();
                    });
                }
            }
            catch (System.Exception exp)
            {
                Toast.MakeText(this, "Notice: " + exp.Message, ToastLength.Long).Show();
            }
        }

        private void BtnStatusClicked(object sender, EventArgs e)
        {
            var state = ble.State;
            Toast.MakeText(this, state.ToString(), ToastLength.Long).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case 456:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            // Permission granted, yay! Start the Bluetooth device scan.
                            Toast.MakeText(this, "You can scan now", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "You cannot scan until you allow the location access", ToastLength.Short).Show();
                            // Alert the user that this application requires the location permission to perform the scan.
                        }
                    }
                    break;
            }
        }


    }
}
