using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Data;

namespace FlavorCart.Models
{
    [FirestoreData]
    public class UserToken : IBaseFirestoreData
    { 
   
        public string Id { get; set; }

        public string Name { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; } // user
        
        [FirestoreProperty]
        public string Token { get; set; } // user's token
        
        [FirestoreProperty]
        public DateTime DateReg { get; set; } // date of registration of the token

        public UserToken()
        {

            DateReg = DateTime.Now;
        }

        public void SetDateReg()
        {
            DateReg = DateTime.Now;
        }
    }
}
