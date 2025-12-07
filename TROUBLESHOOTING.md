# Troubleshooting

## Erro 500 ao retornar ViewModel com navegação

**Problema:** Erro 500 Internal Server Error ao buscar dados que usam propriedades de navegação no ViewModel.

**Causa:** Ao usar ViewModels que acessam propriedades de navegação (ex: `entity.User.FullName`), o Entity Framework precisa carregar essas entidades com `.Include()`.

**Solução:** Sempre adicionar `.Include(p => p.User)` nas queries que retornam dados usados em ViewModels.

**Exemplo:**
```csharp
var place = _context.Places
    .Include(p => p.User)  // Necessário
    .SingleOrDefault(p => p.Id == id);
```
