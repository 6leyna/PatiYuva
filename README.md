# PatiYuva

ASP.NET Core MVC mimarisi kullanılarak geliştirilen bu proje, sahiplendirme sürecini dijital ortama taşıyan kullanıcı dostu bir web uygulamasıdır.

📌 Proje Amacı

Bu uygulamanın amacı;

Sahiplendirilecek hayvanların listelenmesi

Kullanıcıların hayvanlar hakkında detaylı bilgi alabilmesi

Sahiplenme taleplerinin sistem üzerinden yönetilmesi

Rol bazlı kullanıcı deneyimi sunulması

olarak belirlenmiştir.

🧩 Kullanılan Teknolojiler

ASP.NET Core MVC

Entity Framework Core

SQLite

ASP.NET Identity

Razor View Engine

HTML / CSS / Bootstrap

🏗️ Mimari Yapı

Proje, MVC (Model – View – Controller) mimarisi esas alınarak geliştirilmiştir.

🔹 Model

Animal

ApplicationUser

AdoptionRequest

Veri doğrulama işlemleri Data Annotations ile sağlanmıştır.

🔹 View

Razor View’lar kullanılmıştır

Hayvanlar kart (card) yapısında listelenmiştir

Partial View’lar ile tekrar eden yapılar ayrıştırılmıştır

Responsive tasarım ile mobil uyumluluk sağlanmıştır

🔹 Controller

Kullanıcı isteklerini karşılar

İş kurallarını uygular

Model ile View arasındaki veri akışını yönetir

🔐 Kimlik Doğrulama & Yetkilendirme

ASP.NET Identity kullanılmıştır

Rol bazlı yetkilendirme uygulanmıştır:

🐶 Hayvan Sahibi

❤️ Sahiplenmek İsteyen

📂 Veri Tabanı

SQLite kullanılmıştır

Code First yaklaşımı benimsenmiştir

Migration işlemleri ile veritabanı yönetilmiştir

⚙️ Uygulama Özellikleri

Hayvan listeleme ve detay görüntüleme

Sahiplenme talebi oluşturma

Filtreleme ve arama

Fotoğraf yükleme (wwwroot üzerinden)

Kullanıcı kayıt / giriş işlemleri
