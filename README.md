# 🐾 PatiYuva – Hayvan Sahiplendirme Web Uygulaması

PatiYuva, hayvan sahipleri ile sahiplenmek isteyen kullanıcıları bir araya getiren,  
**ASP.NET Core MVC** mimarisi ile geliştirilmiş modern ve sevimli bir hayvan sahiplendirme platformudur.

Bu proje, **MVC Core ile Web Uygulama Geliştirme** dersi kapsamında geliştirilmiştir.

---

## 🎯 Projenin Amacı

- Sahiplendirilmeyi bekleyen hayvanları güvenli ve düzenli bir şekilde listelemek  
- Hayvan sahiplerinin kendi hayvanlarını sisteme ekleyebilmesini sağlamak  
- Sahiplenmek isteyen kullanıcıların hayvanları inceleyip iletişime geçebilmesini sağlamak  
- Sahiplendirme sürecini dijital ve kullanıcı dostu bir yapıya taşımak  

---

## 🧠 Kullanılan Teknolojiler

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **Code First** yaklaşımı
- **SQLite** veritabanı
- **ASP.NET Identity**
- **Razor View Engine**
- **HTML5 / CSS3 / Bootstrap**
- **Responsive Tasarım**

---

## 🧩 Mimari Yapı (MVC)

### 📌 Model
- `Animal`
- `ApplicationUser`
- `AdoptionRequest`
- Veri doğrulama için **Data Annotations** kullanılmıştır.

### 📌 View
- Razor View’lar ile kullanıcı arayüzü oluşturulmuştur  
- Hayvanlar **kart yapısında**, sevimli ve sade bir tasarımla listelenmektedir  
- Tekrar eden yapılar **Partial View** olarak düzenlenmiştir  

### 📌 Controller
- Kullanıcıdan gelen istekler karşılanır  
- İş kuralları uygulanır  
- Model ile View arasında veri aktarımı sağlanır  

---

## 👥 Kullanıcı Rolleri

Uygulamada **rol bazlı kullanıcı yönetimi** bulunmaktadır:

- **Hayvan Sahibi**
  - Hayvan ekleyebilir
  - Kendi eklediği hayvanları görüntüleyebilir

- **Sahiplenmek İsteyen Kullanıcı**
  - Hayvanları listeleyebilir
  - Hayvan detaylarını inceleyebilir
  - Sahip ile iletişime geçebilir

Rol yönetimi **ASP.NET Identity** ile sağlanmıştır.

---

## 🐶 Hayvan Özellikleri

Her hayvan için aşağıdaki bilgiler tutulmaktadır:

- Ad
- Tür (Kedi, Köpek, vb.)
- Cins
- Yaş
- Cinsiyet
- Aşı Durumu
- Açıklama
- Fotoğraf
- Sahip Bilgisi
- İletişim Numarası

Fotoğraf yükleme işlemleri **wwwroot** klasörü üzerinden yapılmaktadır.

---

## 🔍 Ek Özellikler

- Hayvan adı veya türüne göre **arama**
- Tür ve yaşa göre **filtreleme**
- Mobil uyumlu (**responsive**) tasarım
- Bağış sayfası
- Kullanıcıya özel menü yapısı

---

## 🎥 Proje Tanıtım Videosu

📺 YouTube: https://youtu.be/SXOoTEiXKKc


---
<img width="1897" height="988" alt="proje1" src="https://github.com/user-attachments/assets/f54c9fdb-3dc8-48a0-a714-8dccf5cf5cad" />
<img width="1900" height="987" alt="proje2" src="https://github.com/user-attachments/assets/22d0e4a5-5a06-437f-9c5d-353e50df1caa" />
<img width="1918" height="987" alt="proje3" src="https://github.com/user-attachments/assets/dbcc69af-d581-426e-9336-245cc5ddc01e" />
<img width="1897" height="988" alt="proje4" src="https://github.com/user-attachments/assets/ca482236-33ef-4f34-96cb-ed322663ef3e" />
<img width="1918" height="991" alt="proje6" src="https://github.com/user-attachments/assets/b34f7ff3-3648-4a3b-8ac4-81f3a40d6d3e" />
<img width="1903" height="988" alt="proje8" src="https://github.com/user-attachments/assets/ee841df1-0aa3-4d52-b2d0-d5d542691798" />
<img width="1900" height="987" alt="proje5" src="https://github.com/user-attachments/assets/00518533-498a-46e3-9455-3043f7cc6e38" />


