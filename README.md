# Stock Management Project

Projede .Net Core 7 MVC ile katmanlı mimari ile yazılmıştır. Repository pattern, DI, AutoMapper, Serilog tabanlı loglama, global exception filter ve soft delete (hard delete yok) uygulanıyor.

## Katmanlar
- `ApplicationWebUI`: MVC UI, controller/view, filtreler (global exception + action log), middleware (request logging), Serilog ayarı.
- `BusinessLayer`: DTO’lar, AutoMapper profilleri, servislerde doğrulama + arama + pagination.
- `DataAccessLayer`: EF Core `ApplicationDbContext`, migrations, seed data, `EfRepository`.
- `EntityLayer`: Domain entity ve enum’lar.

## Özellikler
- CRUD modalları: stok türü, stok birimi, stok item; kayıtlar silinmez, pasif yapılır.
- Pagination ve sayfa boyutu seçimi; her listede arama kutusu + “Ara” butonu.
- Toastr ile başarı/hata bildirimleri (TempData üzerinden).
- Ondalıklı alanlar için (fiyat/miktar) giriş normalizasyonu.
- Request logging middleware → `RequestLogs` tablosu + Serilog zenginleştirme; global exception filter ile kullanıcı dostu hata yönlendirmesi.

## Gereksinimler
- .Net 7 SDK
- SQL Server / LocalDB (varsayılan `(localdb)\\MSSQLLocalDB`)
- (İsteğe bağlı) Migrations’ı manuel çalıştırmak için `dotnet-ef`

## Konfigürasyon
1) `ApplicationWebUI/appsettings.json` (ve gerekirse `appsettings.Development.json`) içindeki `DefaultConnection` değerini kendi SQL Server’ınıza göre güncelleyin.  
2) Serilog, `Logs/` klasörüne günlük dosyaları yazar ve console'a basar; isterse `appsettings*.json` üzerinden özelleştirilebilir.

## Veritabanı
- Migrations klasörü: `DataAccessLayer/Migrations`.
- Uygulama açılışında `Database.Migrate()` çalışır; tablolar boşsa seed verisi eklenir.
- Migrations’ı manuel uygulamak isterseniz:
  ```powershell
  dotnet ef database update --project DataAccessLayer --startup-project ApplicationWebUI
  ```

## Çalıştırma
```powershell
dotnet restore
dotnet run --project ApplicationWebUI/ApplicationWebUI.csproj
```
Sunucu profili: `http://localhost:5021` (ve `https://localhost:7229`).

## Ekran Notları
- **Stock Types**: İsim tekil, modal ile ekle/düzenle, pasifleştirerek gizlenir.
- **Stock Units**: Kod tekil; stok türü seçimi, miktar birimi, alım/satım para birimi, fiyatlar, kağıt ağırlığı, açıklama; kod/açıklama/türe göre arama.
- **Stock Items**: Her stok birimi için tek kayıt (tekil `StockUnitId`); miktar, kritik miktar, raf/dolap; kritik altı satırlar vurgulu; birim kodu/açıklama/tür/lokasyona göre arama.
- Tüm listelerde pagination ve arama + “Ara” butonu bulunur; işlemler sonrası Toastr bildirimleri gösterilir.