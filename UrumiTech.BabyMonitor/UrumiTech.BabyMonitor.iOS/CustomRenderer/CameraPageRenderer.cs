using System;
using System.Linq;
using AVFoundation;
using CoreGraphics;
using Foundation;
using UIKit;
using UrumiTech.BabyMonitor.iOS.CustomRenderer;
using UrumiTech.BabyMonitor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace UrumiTech.BabyMonitor.iOS.CustomRenderer
{
    public class CameraPageRenderer : PageRenderer
    {
        AVCaptureSession _captureSession;
        AVCaptureDeviceInput _captureDeviceInput;
        AVCaptureStillImageOutput _stillImageOutput;
        UIView _liveCameraStream;
        UIButton _takePhotoButton;
        UIButton _toggleCameraButton;
        UIButton _toggleFlashButton;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                SetupLiveCameraStream();
                AuthorizeCameraUse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_captureDeviceInput != null)
            {
                _captureSession?.RemoveInput(_captureDeviceInput);
            }

            if (_captureDeviceInput != null)
            {
                _captureDeviceInput.Dispose();
                _captureDeviceInput = null;
            }

            if (_captureSession != null)
            {
                _captureSession.StopRunning();
                _captureSession.Dispose();
                _captureSession = null;
            }

            if (_stillImageOutput != null)
            {
                _stillImageOutput.Dispose();
                _stillImageOutput = null;
            }

            base.Dispose(disposing);
        }

        void SetupUserInterface()
        {
            
                var centerButtonX = View?.Bounds.GetMidX() - 35f;
                var topLeftX = View?.Bounds.X + 25;
                var topRightX = View?.Bounds.Right - 65;
                var bottomButtonY = View?.Bounds.Bottom - 150;
                var topButtonY = View?.Bounds.Top + 15;
                var buttonWidth = 70;
                var buttonHeight = 70;

                if (View?.Bounds.Width != null)
                    _liveCameraStream = new UIView
                    {
                        Frame = new CGRect(0f, 0f, (nfloat) View?.Bounds.Width, View.Bounds.Height)
                    };

                _takePhotoButton = new UIButton
                {
                    Frame = new CGRect((nfloat)centerButtonX, (nfloat)bottomButtonY, buttonWidth, buttonHeight)
                };
                _takePhotoButton.SetBackgroundImage(UIImage.FromFile("TakePhotoButton.png"), UIControlState.Normal);

                _toggleCameraButton = new UIButton
                {
                    Frame = new CGRect((nfloat)topRightX, (nfloat)topButtonY + 5, 35, 26)
                };
                _toggleCameraButton.SetBackgroundImage(UIImage.FromFile("ToggleCameraButton.png"), UIControlState.Normal);

                _toggleFlashButton = new UIButton
                {
                    Frame = new CGRect((nfloat)topLeftX, (nfloat)topButtonY, 37, 37)
                };
            

            _toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);

            View?.Add(_liveCameraStream);
            View?.Add(_takePhotoButton);
            View?.Add(_toggleCameraButton);
            View?.Add(_toggleFlashButton);
        }

        void SetupEventHandlers()
        {
            _takePhotoButton.TouchUpInside += (sender, e) =>
            {
                CapturePhoto();
            };

            _toggleCameraButton.TouchUpInside += (sender, e) =>
            {
                ToggleFrontBackCamera();
            };

            _toggleFlashButton.TouchUpInside += (sender, e) =>
            {
                ToggleFlash();
            };
        }

        async void CapturePhoto()
        {
            var videoConnection = _stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await _stillImageOutput.CaptureStillImageTaskAsync(videoConnection ?? throw new InvalidOperationException("Missing Video Connection"));
            var jpegImage = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

            var photo = new UIImage(jpegImage ?? throw new InvalidOperationException("Missing Image"));
            photo.SaveToPhotosAlbum((image, error) =>
            {
                if (!string.IsNullOrEmpty(error?.LocalizedDescription))
                {
                    Console.Error.WriteLine($"\t\t\tError: {error?.LocalizedDescription}");
                }
            });
        }

        void ToggleFrontBackCamera()
        {
            var devicePosition = _captureDeviceInput.Device.Position;
            if (devicePosition == AVCaptureDevicePosition.Front)
            {
                devicePosition = AVCaptureDevicePosition.Back;
            }
            else
            {
                devicePosition = AVCaptureDevicePosition.Front;
            }

            var device = GetCameraForOrientation(devicePosition);
            ConfigureCameraForDevice(device);

            _captureSession.BeginConfiguration();
            _captureSession.RemoveInput(_captureDeviceInput);
            _captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
            _captureSession.AddInput(_captureDeviceInput);
            _captureSession.CommitConfiguration();
        }

        void ToggleFlash()
        {
            var device = _captureDeviceInput.Device;

            if (!device.HasFlash) return;
            if (device.FlashMode == AVCaptureFlashMode.On)
            {
                device.LockForConfiguration(out _);
                device.FlashMode = AVCaptureFlashMode.Off;
                device.UnlockForConfiguration();
                _toggleFlashButton.SetBackgroundImage(UIImage.FromFile("NoFlashButton.png"), UIControlState.Normal);
            }
            else
            {
                device.LockForConfiguration(out _);
                device.FlashMode = AVCaptureFlashMode.On;
                device.UnlockForConfiguration();
                _toggleFlashButton.SetBackgroundImage(UIImage.FromFile("FlashButton.png"), UIControlState.Normal);
            }
        }

        AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
        {
            var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

            return devices.FirstOrDefault(device => device.Position == orientation);
        }

        void SetupLiveCameraStream()
        {
            _captureSession = new AVCaptureSession();

            var videoPreviewLayer = new AVCaptureVideoPreviewLayer(_captureSession)
            {
                Frame = _liveCameraStream.Bounds
            };
            _liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

            var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            _captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);

            //new NSMutableDictionary { [AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG) };
            _stillImageOutput = new AVCaptureStillImageOutput
            {
                OutputSettings = new NSDictionary()
            };

            _captureSession.AddOutput(_stillImageOutput);
            _captureSession.AddInput(_captureDeviceInput);
            _captureSession.StartRunning();
        }

        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out _);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out _);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out _);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

        async void AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }
    }

}