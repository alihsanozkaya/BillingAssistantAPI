using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Constants
{
    public static class Messages
    {
        public static string UserAdded = "Kullanıcı başarıyla eklendi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string UnAuthorized = "Önce giriş yapmanız gerek";

        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";
        public static string Added = "Başarıyla eklendi";
        public static string Listed = "Başarıyla listelendi";
        public static string NotListed = "Başarısız listeleme";
        public static string Updated = "Başarıyla güncellendi";
        public static string Deleted = "Başarıyla silindi";

        public static string VerificationDone = "Doğrulama yapıldı";
        public static string VerificationFailed = "Doğrulama yapılamadı";
        public static string InvalidData = "Geçersiz veri";
        public static string UserAlreadyVerified = "Kullanıcı zaten doğrulandı";

        public static string UserNotActive = "Kullanıcı doğrulanmamış";
        
        public static string VerificationStatusRetrieved = "Doğrulama Durumu Alındı";
    }
}