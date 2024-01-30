using MotelDecktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MotelDecktopApp
{
    internal class ApiConfig
    {
        public static string url = "https://localhost:44338/api/";

        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.GetAsync("guests");
                responseTask.Wait();

                var getResult = responseTask.Result;
                if (getResult.IsSuccessStatusCode)
                {
                    var readTask = getResult.Content.ReadAsAsync<List<Guest>>();
                    readTask.Wait();

                    guests = readTask.Result;

                    
                }
                else
                {
                    Console.WriteLine("Failed to retrieve guests. Status code: {0}", getResult.StatusCode);
                }
            }

            return guests;
        }
        public void SendNewGuest(Guest guest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP POST --------------------------------------
               

                var postTask = client.PostAsJsonAsync<Guest>("guests", guest);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {

                    var readTask = PostResult.Content.ReadAsAsync<Guest>();
                    readTask.Wait();

                    

                   
                }
                else
                {
                    Console.WriteLine(PostResult.StatusCode);
                }
            }

        }
        public void ChangeGuest(int guestId, Guest updatedGuest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // HTTP PUT --------------------------------------

                var putTask = client.PutAsJsonAsync($"guests/{guestId}", updatedGuest);
                putTask.Wait();

                var putResult = putTask.Result;
                if (putResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("Guest updated successfully.");
                }
                else
                {
                    Console.WriteLine(putResult.StatusCode);
                }
            }
        }

        public void DeleteGuest(int guestId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // HTTP DELETE --------------------------------------

                var deleteTask = client.DeleteAsync($"guests/{guestId}");
                deleteTask.Wait();

                var deleteResult = deleteTask.Result;
                if (deleteResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("Guest deleted successfully.");
                }
                else
                {
                    Console.WriteLine(deleteResult.StatusCode);
                }
            }
        }

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.GetAsync("rooms");
                responseTask.Wait();

                var getResult = responseTask.Result;
                if (getResult.IsSuccessStatusCode)
                {
                    var readTask = getResult.Content.ReadAsAsync<List<Room>>();
                    readTask.Wait();

                    rooms = readTask.Result;


                }
                else
                {
                    Console.WriteLine("Failed to retrieve guests. Status code: {0}", getResult.StatusCode);
                }
            }

            return rooms;
        }
        public void SendNewRoom(Room room)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP POST --------------------------------------


                var postTask = client.PostAsJsonAsync<Room>("rooms", room);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {

                    var readTask = PostResult.Content.ReadAsAsync<Room>();
                    readTask.Wait();




                }
                else
                {
                    Console.WriteLine(PostResult.StatusCode);
                }
            }

        }
        public void ChangeRoom(int roomId, Room updatedRoom)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // HTTP PUT --------------------------------------

                var putTask = client.PutAsJsonAsync($"rooms/{roomId}", updatedRoom);
                putTask.Wait();

                var putResult = putTask.Result;
                if (putResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("Guest updated successfully.");
                }
                else
                {
                    Console.WriteLine(putResult.StatusCode);
                }
            }
        }

        public void DeleteRoom(int roomId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // HTTP DELETE --------------------------------------

                var deleteTask = client.DeleteAsync($"rooms/{roomId}");
                deleteTask.Wait();

                var deleteResult = deleteTask.Result;
                if (deleteResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("Guest deleted successfully.");
                }
                else
                {
                    Console.WriteLine(deleteResult.StatusCode);
                }
            }
        }
        public List<GuestRoom> GetAllGuestRooms()
        {
            List<GuestRoom> guestRooms = new List<GuestRoom>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var getResult = client.GetAsync("guestrooms").Result;

                if (getResult.IsSuccessStatusCode)
                {
                    guestRooms = getResult.Content.ReadAsAsync<List<GuestRoom>>().Result;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve guest rooms. Status code: {0}", getResult.StatusCode);
                }
            }

            return guestRooms;
        }

        public void SendNewGuestRoom(GuestRoom newGuestRoom)
        {
            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var postResult = client.PostAsJsonAsync("guestrooms", newGuestRoom).Result;

                if (!postResult.IsSuccessStatusCode)
                {
                    Console.WriteLine(postResult.StatusCode);
                }
            }
        }

        public void ChangeGuestRoom(int guestRoomId, int guestId, int roomId)
        {
            var updatedGuestRoom = new GuestRoom
            {
                GuestRoomId = guestRoomId,
                GuestId = guestId,
                RoomId = roomId
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var putResult = client.PutAsJsonAsync($"guestrooms/{guestRoomId}", updatedGuestRoom).Result;

                if (!putResult.IsSuccessStatusCode)
                {
                    Console.WriteLine(putResult.StatusCode);
                }
            }
        }

        public void DeleteGuestRoom(int guestRoomId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var deleteResult = client.DeleteAsync($"guestrooms/{guestRoomId}").Result;

                if (!deleteResult.IsSuccessStatusCode)
                {
                    Console.WriteLine(deleteResult.StatusCode);
                }
            }
        }
        public Guest GetGuestById(int guestId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.GetAsync($"guests/{guestId}");
                responseTask.Wait();

                var getResult = responseTask.Result;
                if (getResult.IsSuccessStatusCode)
                {
                    var readTask = getResult.Content.ReadAsAsync<Guest>();
                    readTask.Wait();

                    return readTask.Result;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve guest. Status code: {0}", getResult.StatusCode);
                    return null;
                }
            }
        }

        public Room GetRoomById(int roomId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.GetAsync($"rooms/{roomId}");
                responseTask.Wait();

                var getResult = responseTask.Result;
                if (getResult.IsSuccessStatusCode)
                {
                    var readTask = getResult.Content.ReadAsAsync<Room>();
                    readTask.Wait();

                    return readTask.Result;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve room. Status code: {0}", getResult.StatusCode);
                    return null;
                }
            }
        }
    }

}
