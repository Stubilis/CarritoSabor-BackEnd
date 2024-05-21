using FlavorCart.Interfaces;
using Google.Cloud.Firestore;
using System.Globalization;


namespace FlavorCart.Models
{
    [FirestoreData]
    public class Article : IBaseFirestoreData
    {
       
        public string Id { get; set; }

        //Properties to be able to format the text 
        private string _name;
        private string _description;
        private string _brand;

        [FirestoreProperty]
        public string Name { get  => _name; set
            {
                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
                _name = textInfo.ToTitleCase(value);
            } } 

        [FirestoreProperty]
        public string Description {
            get => _description; set
            {
               //Make only the first letter uppercase 
                _description = value.Substring(0, 1).ToUpper() + value.Substring(1);
            }
        }
        [FirestoreProperty]
        public string ImageUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Brand
        {
            get => _brand; set
            {
                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
                _brand = textInfo.ToTitleCase(value);
            }
        }

        [FirestoreProperty]
        public List<string> Categories { get; set; } //Save here the categories id

        [FirestoreProperty]
        public float AveragePrice { get; set; } 

     
        //Set the name with the first letter in uppercase
        public void SetName(string name)
        {
            TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
            Name = textInfo.ToTitleCase(name);
        }
     
        //Set the brand with the first letter in uppercase
        public void SetBrand(string brand)
        {
            TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
            Brand = textInfo.ToTitleCase(brand);
        }

  
    }
}
