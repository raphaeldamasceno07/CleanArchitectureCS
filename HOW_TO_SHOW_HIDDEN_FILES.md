# 👀 Como Visualizar Arquivos Ocultos no Visual Studio

## 🎯 Problema
Arquivos que começam com ponto (`.`) são **ocultos** por padrão:
- `.gitignore`
- `.env.example`
- `.editorconfig`

## ✅ Solução 1: Mostrar Arquivos Ocultos no VS (Recomendado)

### Via Menu:
1. Abra o **Solution Explorer** (Ctrl + Alt + L)
2. Clique no ícone de **três linhas horizontais** (hambúrguer) no topo
3. Selecione **Show All Files**

### Pronto! 🎉
Agora aparecerão todos os arquivos, inclusive os ocultos.

---

## 📁 Resultado

Antes (sem ocultos):
```
📦 Solution
├── API/
├── Application/
├── Domain/
├── Infrastructure/
├── UnitTest/
├── tools/
└── compose.yaml          ← Visível
```

Depois (com ocultos):
```
📦 Solution
├── .editorconfig         ← Agora visível
├── .env.example          ← Agora visível
├── .gitignore            ← Agora visível
├── API/
├── Application/
├── Domain/
├── Infrastructure/
├── UnitTest/
├── tools/
└── compose.yaml
```

---

## 💡 Dica: `.env` vs `.env.example`

| Arquivo | Visível? | Função | Git |
|---------|----------|--------|-----|
| `.env` | ❌ Oculto | Suas credenciais reais | ❌ Ignorado |
| `.env.example` | ✅ Visível | Template para outros devs | ✅ Incluído |

---

## 🔒 Segurança

✅ `.env` com suas senhas: **NUNCA faz commit**  
✅ `.env.example` sem senhas: **Faz commit**  

---

## 🚀 Atalho
Pressione `Ctrl + Alt + Shift + H` no Solution Explorer para alternar!
