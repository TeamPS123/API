using API_DACN.Database;
using API_DACN.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class DelImg
    {
        private ImageModel imageModel;

        public DelImg(food_location_dbContext db)
        {
            imageModel = new ImageModel(db);
        }

        public bool OfFood(string foodId)
        {
            List<string> ImgList = imageModel.DelImageOfFood(foodId);
            if(ImgList != null)
            {
                try
                {
                    foreach (var item in ImgList)
                    {
                        string path = Path.Combine(item);
                        System.IO.File.Delete(path);
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
    }
}
