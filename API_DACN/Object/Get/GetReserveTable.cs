﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetReserveTable
    {
        public string Id { get; set; }
        public int quantity { get; set; }
        public string time { get; set; }
        public string promotionId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string note { get; set; }
        public int ? status { get; set; }
        public Get.GetRestaurant restaurant { get; set; }
    }

    public class Message_ReserveTable
    {
        private int status;
        private string notification;
        private IEnumerable<GetReserveTable> reserveTables;

        public Message_ReserveTable(int status, string notification, IEnumerable<GetReserveTable> reserveTables)
        {
            this.Status = status;
            this.Notification = notification;
            this.ReserveTables = reserveTables;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetReserveTable> ReserveTables { get => reserveTables; set => reserveTables = value; }
    }

    public class GetReserveTable1
    {
        public string reserveTableId { get; set; }
        public int quantity { get; set; }
        public string time { get; set; }
        public string restaurantId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string promotionId { get; set; }
        public string note { get; set; }
        public string userId { get; set; }
    }

    public class Message_ReserveTable1
    {
        private int status;
        private string notification;
        private IEnumerable<GetReserveTable1> reserveTables;

        public Message_ReserveTable1(int status, string notification, IEnumerable<GetReserveTable1> reserveTables)
        {
            this.Status = status;
            this.Notification = notification;
            this.ReserveTables = reserveTables;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetReserveTable1> ReserveTables { get => reserveTables; set => reserveTables = value; }
    }

    public class GetStaticRes
    {
        public string amount_today { get; set; }
        public string amount_toweek { get; set; }
        public bool ? statusRes { get; set; }
    } 

    public class Message_StaticRes
    {
        private int status;
        private string notification;
        private GetStaticRes staticRes;

        public Message_StaticRes(int status, string notification, GetStaticRes staticRes)
        {
            this.status = status;
            this.notification = notification;
            this.staticRes = staticRes;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetStaticRes StaticRes { get => staticRes; set => staticRes = value; }
    }
}
