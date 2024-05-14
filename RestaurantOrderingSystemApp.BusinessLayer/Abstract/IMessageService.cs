﻿using RestaurantOrderingSystemApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderingSystemApp.BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        int TMessageCountByStatusFalse();
        List<Message> TGetAllMessageByFalse();
        void TMessageStatusChange(int id);
        void TAllMessageStatusesChangeToTrue();
    }
}