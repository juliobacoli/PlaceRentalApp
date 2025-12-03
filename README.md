# PlaceRentalApp

API para gerenciamento de aluguel de lugares (casas, apartamentos, etc).

## Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- Swagger

## Funcionalidades

- Gerenciamento de usuários
- Cadastro de lugares para aluguel
- Sistema de reservas
- Comentários e avaliações
- Comodidades (amenities)

## Como Executar

```bash
cd PlaceRentalApp.API
dotnet run
```

## Banco de Dados

Configure a connection string em `appsettings.json`:

```json
"ConnectionStrings": {
  "PlaceRental": "sua-connection-string"
}
```

Execute as migrations:

```bash
dotnet ef database update
```