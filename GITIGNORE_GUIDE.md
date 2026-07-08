# 📋 Guia Completo do .gitignore

## 📝 Conteúdo Atual do seu `.gitignore`

```gitignore
# Pastas do Visual Studio
.idea/
.vscode/
.vs/
**/bin/
**/obj/

# Arquivos temporários
*.user
*.suo
*.userosscache
*.sln.docstates

# Arquivos de log e cache
*.log

# Variáveis de ambiente
.env
!.env.example          ← Negação: INCLUA este arquivo

publish/

*.nupkg
packages/

Thumbs.db
```

---

## 🎯 O Que Está Sendo Ignorado?

### 🚫 Ignorado (Não sobe no Git):

| Padrão | Descrição |
|--------|-----------|
| `.idea/` | Pasta de configuração do IntelliJ |
| `.vscode/` | Pasta de configuração do VS Code |
| `.vs/` | Pasta do Visual Studio |
| `**/bin/` | Pastas de compilação (todas as subpastas) |
| `**/obj/` | Pastas de objetos compilados |
| `*.user` | Arquivos de configuração VS |
| `*.suo` | Arquivos de solução VS |
| `*.log` | Arquivos de log |
| `.env` | **Variáveis de ambiente com senhas** |
| `publish/` | Pasta de publicação |
| `*.nupkg` | Pacotes NuGet |
| `Thumbs.db` | Cache do Windows Explorer |

### ✅ Permitido (Sobe no Git):

| Arquivo | Motivo |
|---------|--------|
| `.env.example` | Template das variáveis (sem senhas) |
| `.gitignore` | Própria configuração (sim, é um arquivo normal) |
| `.editorconfig` | Configuração de formatação |
| `compose.yaml` | Configuração Docker |

---

## 🔒 Segurança: .env vs .env.example

```
Your Machine
│
├── .env              (NÃO SOBE) ❌
│   ├── DB_PASSWORD=SenhaReal123!
│   ├── API_KEY=chave_super_secreta
│   └── ... dados sensíveis
│
└── .env.example      (SOBE SIM) ✅
    ├── DB_PASSWORD=YourSecurePassword123!
    ├── API_KEY=your_api_key_here
    └── ... template sem valores reais
```

---

## ➕ Como Adicionar Novos Padrões?

Se quiser ignorar mais coisas, adicione ao final:

```gitignore
# Novos padrões
*.tmp              # Ignora todos os arquivos .tmp
cache/             # Ignora a pasta cache
*.backup           # Ignora todos os backups
```

---

## 📌 Exemplos Comuns de Padrões

```gitignore
# Ignorar arquivo específico
config.local.json

# Ignorar extensão em qualquer pasta
**/logs/*.log

# Ignorar pasta e seu conteúdo
node_modules/

# Ignorar tudo EXCETO um arquivo (negação)
*.txt
!importante.txt

# Ignorar múltiplas extensões
*.bak
*.tmp
*.cache
```

---

## ✨ Seu .gitignore Está Ótimo!

✅ Já tem tudo que você precisa para um projeto .NET  
✅ Segue as melhores práticas  
✅ Protege dados sensíveis  

---

## 🚀 Como Usar

Se precisar adicionar algo novo:

1. Abra o `.gitignore` (agora você já sabe onde está!)
2. Adicione o padrão na seção apropriada
3. Salve e faça commit

Exemplo:
```
# Variáveis de ambiente
.env
!.env.example
*.local              # ← Adicione se precisar ignorar arquivos .local
```

---

## 🐛 Troubleshooting

### "Arquivo foi ignorado mas eu quero adicionar"

Use a negação (`!`):
```gitignore
*.log        # Ignora todos os logs
!importante.log  # EXCETO este arquivo
```

### "Arquivo já estava rastreado e agora quero ignorar"

```powershell
git rm --cached nome_do_arquivo
git commit -m "remove: stop tracking arquivo"
```

---

**Última atualização**: 08/07/2026  
**Versão**: 1.0
