# Clean Architecture 101

Uygulamalı clean architecture eğitimi için hazırlanmış baz repodur.

## Ön Hazırlıklar

Örnekte şu an için sqlite tabanlı fiziki bir veritabanı dosyası kullanılmakta. Migration işlemleri için web api uygulaması ayaktayken Data projesi altında aşağıdaki komutları çalıştırmak yeterli.

```bash
# Migration planını hazırlamak için
dotnet ef migrations add InitialCreate --startup-project ../../presentation/GamersWorld.WebApi

# Database'i oluşturmak için
dotnet ef database update --startup-project ../../presentation/GamersWorld.WebApi
```

## Çalışma Zamanı

Uygulamanın son kullanıcı ile temas noktası web api örneğidir. WebApi klasöründeyken proje aşağıdaki komutla çalıştırılabilir.

```bash
dotnet run
```

Postman için örnek komutları [şuradaki dosyadan](Clean%20Architecture%20Training.postman_collection.json) alabilirsiniz.
