

namespace MyEvernote.Entities.Messages
{
    //hata mesajlarının değerlerini tanımlamak için kullandık
    public enum ErrorMessageCode
    {
        UsernameAlreadyExist=101,
        EmailAlreadyExist=102,
        UserIsNotActive=151,
        UsernameOrPassWrong=152 ,//kullanıcı ve şifre uyuşmuyor
        CheckYourEmail=153 //Email adresinizi kontrol edin kodu
    }
}
