﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities.Messages;

namespace MyEvernote.BusinessLayer
{
    public class BusinessLayerResult<T> where T:class
    {
        public List<ErrorMessageObj> Errors { get; set; }

        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();


        }

        //yeni hata mesajlarını daha kolay ekleyebilmek için tanımlandı
        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObj()
            {
                Code=code,
                Message=message
            });
        }
    }
}
