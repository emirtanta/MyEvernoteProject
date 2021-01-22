using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            //kullanıcı username var mı yok mu kontrolü
            //kullanıcı e-posta kontrolü
            //kayıt işlemi
            //aktivasyon e-postası gönderimi

            //kullanıcı username var mı yok mu kontrolü ve kullanıcı e-posta kontrolü
            EvernoteUser user= repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);

            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            //kullanıcı veya email varsa
            if (user!=null)
            {
                if (user.Username==data.Username)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.UsernameAlreadyExist,"Kullanıcı adı kayıtlı");
                }

                if (user.Email==data.Email)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.EmailAlreadyExist, "e-posta adresi kayıtlı");
                }
            }

            //kullanıcı yoksa
            else
            {
                int dbResult=repo_user.Insert(new EvernoteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid=Guid.NewGuid(),
                    IsActive =false,
                    IsAdmin=false
                    
                });

                if (dbResult>0)
                {
                    res.Result= repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    //TODO:aktivasyon maili atılacak
                    //layerResult.Result.ActivateGuid
                }
            }

            return res;
        }

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            //giriş kontrolü ve yönledirme
            //hesap aktive edilmiş mi

            
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            //kullanıcı username var mı yok mu kontrolü ve kullanıcı şifre kontrolü
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            //eğer kullanıcı varsa
            if (res.Result != null)
            {
                //kullanıcı aktif ediğilse
                if (!res.Result.IsActive)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(Entities.Messages.ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz");
                }

                
            }

            //kullanıcı eşleşmemişse
            else
            {
                res.AddError(Entities.Messages.ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre uyuşmuyor");
            }

            return res;
        }
    }
}
