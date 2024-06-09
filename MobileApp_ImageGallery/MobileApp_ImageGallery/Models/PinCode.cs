using MobileApp_ImageGallery.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp_ImageGallery.Models
{
    public class PinCode : INotifyPropertyChanged
    {
        private const string PinKey = "user_pin";
        private string _enteredPin;
        private string _statusMessage;
        private bool _isPinSet;
        private bool _isPinCorrect;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigation _navigation;
        public PinCode(INavigation navigation)
        {
            var savedPin = SecureStorage.GetAsync(PinKey).Result;
            IsPinSet = string.IsNullOrEmpty(savedPin) ? false : true;
            IsPinCorrect = true;

            SubmitPin = new Command(OnSubmitPin);
            _navigation = navigation;
        }

        public string EnteredPin
        {
            get => _enteredPin;
            set
            {
                _enteredPin = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsPinSet
        {
            get => _isPinSet;
            private set
            {
                _isPinSet = value;
                OnPropertyChanged();
            }
        }

        public bool IsPinCorrect
        {
            get => _isPinCorrect;
            private set
            {
                _isPinCorrect = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitPin { get; }

        public async void OnSubmitPin()
        {
            if (IsPinSet != true)
            {
                if (EnteredPin.Length == 4)
                {
                    await SecureStorage.SetAsync(PinKey, EnteredPin);
                    IsPinSet = true;

                    StatusMessage = "PIN установлен.";

                    await _navigation.PushAsync(new DeviceListPage());
                }
                else
                {
                    StatusMessage = "PIN должен содержать 4 цифры.";
                }
            }
            else
            {
                var savedPin = await SecureStorage.GetAsync(PinKey);
                if (EnteredPin == savedPin)
                {
                    IsPinCorrect = true;
                }
                else
                {
                    IsPinCorrect = false;
                    StatusMessage = "Неверный PIN.";
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
