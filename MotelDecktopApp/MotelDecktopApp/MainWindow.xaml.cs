using MotelDecktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MotelDecktopApp
{
    public partial class MainWindow : Window
    {
        private ApiConfig apiConfig;

        public MainWindow()
        {
            InitializeComponent();
            apiConfig = new ApiConfig();
            LoadGuests();
            LoadRooms();
            LoadGuestRooms();
        }
        private void LoadRooms()
        {
            var rooms = apiConfig.GetAllRooms();
            roomListView.ItemsSource = rooms;
        }
        private void LoadGuests()
        {
            var guests = apiConfig.GetAllGuests();
            guestListView.ItemsSource = guests;
        }
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            // Создаем нового гостя
            Guest newGuest = new Guest
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
                // Добавьте другие свойства гостя, если они есть
            };

            // Отправляем нового гостя на сервер
            apiConfig.SendNewGuest(newGuest);

            // Перезагружаем список гостей после добавления нового гостя
            LoadGuests();

            // Очищаем текстовые поля после добавления гостя
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
        }
        private void btnUpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли гость
            if (guestListView.SelectedItem != null)
            {
                // Получаем выбранного гостя
                Guest selectedGuest = (Guest)guestListView.SelectedItem;

                // Создаем обновленного гостя
                Guest updatedGuest = new Guest
                {
                    GuestId = selectedGuest.GuestId,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text
                    // Добавьте другие свойства гостя, если они есть
                };

                // Обновляем гостя на сервере
                apiConfig.ChangeGuest(selectedGuest.GuestId, updatedGuest);

                // Перезагружаем список гостей после обновления гостя
                LoadGuests();

                // Очищаем текстовые поля после обновления гостя
                txtFirstName.Text = "First Name";
                txtLastName.Text = "Last Name";
            }
            else
            {
                MessageBox.Show("Select a guest to update.");
            }
        }
        private void btnAddRoom_Click(object sender,RoutedEventArgs e)
        {
            // Создаем нового гостя
            Room newRoom = new Room
            {
                RoomNumber = txtRoomNumber.Text,
               
              
            };

            // Отправляем нового гостя на сервер
            apiConfig.SendNewRoom(newRoom);

            // Перезагружаем список гостей после добавления нового гостя
            LoadRooms();

            // Очищаем текстовые поля после добавления гостя
            txtRoomNumber.Text = string.Empty;
            

        }
        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли гость
            if (roomListView.SelectedItem != null)
            {
                // Получаем выбранного гостя
                Room selectedRoom = (Room)roomListView.SelectedItem;

                // Создаем обновленного гостя
                Room updatedGuest = new Room
                {
                    RoomId = selectedRoom.RoomId,
                    RoomNumber = txtRoomNumber.Text,
                    
                    // Добавьте другие свойства гостя, если они есть
                };

     
                apiConfig.ChangeRoom(selectedRoom.RoomId, updatedGuest);

            
                LoadRooms();

                // Очищаем текстовые поля после обновления гостя
                txtRoomNumber.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Select a room to update.");
            }
        }
        private void btnAddGuestRoom_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбраны ли гость и комната
            if (guestListView.SelectedItem != null && roomListView.SelectedItem != null)
            {
                // Получаем выбранных гостя и комнату
                Guest selectedGuest = (Guest)guestListView.SelectedItem;
                Room selectedRoom = (Room)roomListView.SelectedItem;

                // Создаем новую связь между гостем и комнатой
                GuestRoom newGuestRoom = new GuestRoom
                {
                    GuestId = selectedGuest.GuestId,
                    RoomId = selectedRoom.RoomId,
                    GuestFirstName = selectedGuest.FirstName,
                    GuestLastName = selectedGuest.LastName,
                    RoomNumber = selectedRoom.RoomNumber
                };

                // Отправляем новую связь на сервер
                apiConfig.SendNewGuestRoom(newGuestRoom);

                // Перезагружаем список связей после добавления новой связи
                LoadGuestRooms();
            }
            else
            {
                MessageBox.Show("Select a guest and a room to create a guest-room relationship.");
            }
        }

        private void btnDeleteGuestRoom_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбрана ли связь гостя и комнаты
            if (guestRoomListView.SelectedItem != null)
            {
                // Получаем выбранную связь
                GuestRoom selectedGuestRoom = (GuestRoom)guestRoomListView.SelectedItem;

                // Удаляем связь на сервере
                apiConfig.DeleteGuestRoom(selectedGuestRoom.GuestRoomId);

                // Перезагружаем список связей после удаления связи
                LoadGuestRooms();
            }
            else
            {
                MessageBox.Show("Select a guest-room relationship to delete.");
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {

            // Проверяем, выбран ли гость
            if (roomListView.SelectedItem != null)
            {
                // Получаем выбранного гостя
                Room selectedRoom = (Room)roomListView.SelectedItem;

                // Удаляем гостя на сервере
                apiConfig.DeleteRoom(selectedRoom.RoomId);

                // Перезагружаем список гостей после удаления гостя
                LoadRooms();

                
            }
            else
            {
                MessageBox.Show("Select a room to delete.");
            }
        }



        private void LoadGuestRooms()
        {
            var guestRooms = apiConfig.GetAllGuestRooms();

            // Для каждого элемента в списке GuestRoom добавляем информацию о госте и комнате
            foreach (var guestRoom in guestRooms)
            {
                var guest = apiConfig.GetGuestById(guestRoom.GuestId); // Метод для получения гостя по Id
                var room = apiConfig.GetRoomById(guestRoom.RoomId); // Метод для получения комнаты по Id

                guestRoom.GuestFirstName = guest.FirstName;
                guestRoom.GuestLastName = guest.LastName;
                guestRoom.RoomNumber = room.RoomNumber;
            }

            guestRoomListView.ItemsSource = guestRooms;
        }

        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли гость
            if (guestListView.SelectedItem != null)
            {
                // Получаем выбранного гостя
                Guest selectedGuest = (Guest)guestListView.SelectedItem;

                // Удаляем гостя на сервере
                apiConfig.DeleteGuest(selectedGuest.GuestId);

                // Перезагружаем список гостей после удаления гостя
                LoadGuests();

                // Очищаем текстовые поля после удаления гостя
                txtFirstName.Text = "First Name";
                txtLastName.Text = "Last Name";
            }
            else
            {
                MessageBox.Show("Select a guest to delete.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

