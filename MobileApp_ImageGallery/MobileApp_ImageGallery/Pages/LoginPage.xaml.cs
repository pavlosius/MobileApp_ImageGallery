using MobileApp_ImageGallery.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_ImageGallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        // Константа для текста кнопки
        public const string BUTTON_TEXT = "Войти";
        // Переменная счетчика
        public static int loginCouner = 0;

        public LoginPage()
        {
            InitializeComponent();

            this.BindingContext = new PinCode(Navigation);

            // Изменяем внешний вид кнопки для Windows-версии
            //if (Device.RuntimePlatform == Device.UWP)
            //    loginButton.CornerRadius = 0;
        }

        /// <summary>
        /// По клику "логинимся" на главный экран приложения
        /// </summary>
        private async void Login_Click(object sender, EventArgs e)
        {
            //loginButton.Text = $"Выполняется вход..";
            // Имитация задержки (приложение загружает данные с сервера)
            await Task.Delay(150);

            // Переход на следующую страницу - страницу списка устройств
            await Navigation.PushAsync(new DeviceListPage());
            // Восстановим первоначальный текст на кнопке (на случай, если пользователь вернется на этот экран чтобы выполнить вход снова)
            //loginButton.Text = BUTTON_TEXT;
        }
    }
}