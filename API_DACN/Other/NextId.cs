using API_DACN.Database;
using API_DACN.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class NextId
    {
        private readonly food_location_dbContext db;

        public NextId(food_location_dbContext db)
        {
            this.db = db;
        }

        public string GetUserId()
        {
            string query = "SELECT dbo.AUTO_UserID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetCategoryId()
        {
            string query = "SELECT dbo.AUTO_CategoryID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetFoodId()
        {
            string query = "SELECT dbo.AUTO_FoodID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetMenuId()
        {
            string query = "SELECT dbo.AUTO_MenuID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetPromotionId()
        {
            string query = "SELECT dbo.AUTO_PromotionID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetPromotionTypeId()
        {
            string query = "SELECT dbo.AUTO_PromotionTypeID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetReserveTableId()
        {
            string query = "SELECT dbo.AUTO_ReserveTableID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public string GetRestaurantId()
        {
            string query = "SELECT dbo.AUTO_RestaurantID() AS 'NextId'";
            var nextIdViewModel = db.NextIdViewModel.FromSqlRaw(query).AsEnumerable().FirstOrDefault();
            return nextIdViewModel.NextId;
        }

        public void temp(string key, int quantity)
        {
            string query = "EXEC dbo.Search @strFind = 'com',  @quantity = 20 ";
            var nextIdViewModel = db.searchSql.FromSqlRaw(query).AsEnumerable();
            //var userType = db.Set<NextIdViewModel>().FromSqlRaw(query).AsEnumerable();
            //var h = nextIdViewModel.Count();
            string t = "";
        }
    }

    public class searchSql
    {
        [Key]
        public string id { get; set; }
        public string Rank { get; set; }
    }
}
