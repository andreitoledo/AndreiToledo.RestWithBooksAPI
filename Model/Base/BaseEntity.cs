﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AndreiToledo.RestWithBooksAPI.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}