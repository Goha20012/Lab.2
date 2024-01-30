using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotelDecktopApp.Models
{
    public class GuestRoom
    {
        public int GuestRoomId { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public string GuestFirstName { get; set; } // Добавлено свойство для имени гостя
        public string GuestLastName { get; set; }  // Добавлено свойство для фамилии гостя
        public string RoomNumber { get; set; }     // Добавлено свойство для номера комнаты
    }


}
