# Eczane Stok Takibi
Hastane içindeki eczaneyeden (ana depo) , hastane bölümleri olan ara depolara (Kardioloji, Beyin Cerrahi ..) ilaç çıkışı ve bu ara depolardan kayıtlı hastalara ilaç çıkışını yönetmek için geliştirilmiştir.

Bu proje Angular ve .Net Core kulllanılarak geliştirilmiştir. Veritabanı olarak Mssql kullanılmıştır. Projenin geliştirilmesini asıl amacı Angular ve .Net Core kullanımını pekiştirmektir.

Bu repo da sadece .Net Core kısmı bulunmakta.
Angular ile geliştirme kısmına [buradan](https://github.com/BurakTy/angular-pharmacy-stock-tracking/) ulaşabilirsiniz. 


##### Bu proje de kullanılan teknolojiler
- Angular 12        --> Arayüz geliştirme
- .Net Core         --> WepAPI geliştirme
- Angular Material  --> Arayüz tasarımı
- Entity Framework  --> Veri bağlatısı , Sql komutları
- Autofac           --> Dependency injection , Transaction Aspect
- Fluent Validation --> Apilere gelen verileri doğrulma , koşul ekleme
- JWt               --> Jwt Token ve Güvenlik
- Itextsharp        --> Raporlama için pdf dokümanları 
