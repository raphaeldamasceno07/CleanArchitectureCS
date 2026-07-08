# Docker Compose - Guia de Uso

## 📋 Requisitos

- Docker Desktop instalado
- `.env` configurado (copie `.env.example` e ajuste as variáveis)

## 🚀 Iniciando os Serviços

### 1. Copiar o arquivo de configuração
```powershell
cp .env.example .env
```

### 2. Iniciar os containers
```powershell
docker-compose up -d
```

Ou para ver os logs em tempo real:
```powershell
docker-compose up
```

## 📊 Monitorar os Containers

### Ver status dos containers
```powershell
docker-compose ps
```

### Ver logs do SQL Server
```powershell
docker-compose logs sqlserver
```

### Ver logs em tempo real
```powershell
docker-compose logs -f sqlserver
```

## 🛑 Parar os Serviços

```powershell
docker-compose down
```

Para remover volumes (limpar dados):
```powershell
docker-compose down -v
```

## 🌐 Network - Como Funciona

### Dentro de um container (sua API)
Use o hostname: `sqlserver`

**Connection String:**
```
Server=sqlserver,1433;Database=CleanArchitectureDb;User Id=sa;Password=YourSecurePassword123!
```

### Do seu computador (desenvolvimento local)
Use: `localhost,1433`

**Connection String:**
```
Server=localhost,1433;Database=CleanArchitectureDb;User Id=sa;Password=YourSecurePassword123!
```

## 🔍 Testar a Conexão

### Via SQL Server Management Studio (SSMS)
1. Abra o SSMS
2. Servidor: `localhost,1433`
3. Autenticação: SQL Server
4. Login: `sa`
5. Senha: (valor de `DB_PASSWORD` no .env)

### Via PowerShell (sqlcmd)
```powershell
sqlcmd -S localhost,1433 -U sa -P "YourSecurePassword123!" -Q "SELECT @@VERSION"
```

## 📁 Estrutura da Network

```
┌─────────────────────────────────────┐
│   Docker Network (bridge)           │
│   clean-arch-network                │
├─────────────────────────────────────┤
│                                     │
│  ┌──────────────┐   ┌────────────┐  │
│  │  SQL Server  │   │  Sua API   │  │
│  │  sqlserver   │◄─►│ (.NET 10)  │  │
│  │ (container)  │   │(futuro)    │  │
│  └──────────────┘   └────────────┘  │
│                                     │
└─────────────────────────────────────┘
         │
         │ Porta 1433
         ▼
    Seu Computador
    (localhost:1433)
```

## ✅ Benefícios da Network

- ✨ **DNS Resolution**: Use `sqlserver` como hostname
- 🔒 **Isolamento**: Apenas containers na network se veem
- 📦 **Service Discovery**: Automático
- 🏗️ **Escalabilidade**: Fácil adicionar novos serviços
- 🚀 **Production-Ready**: Segue padrões profissionais

## 🔧 Adicionar Sua API ao Docker Compose

Quando estiver pronto, adicione seu container da API:

```yaml
services:
  api:
    build:
      context: .
      dockerfile: API/Dockerfile
    container_name: clean_architecture_api
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=${DB};User Id=sa;Password=${DB_PASSWORD}
    ports:
      - "5000:8080"
    networks:
      - clean-arch-network
    depends_on:
      sqlserver:
        condition: service_healthy
```

## 📝 Troubleshooting

### SQL Server não inicia
- Verifique se `DB_PASSWORD` tem pelo menos 8 caracteres
- Verifique se há espaço em disco

### Não consegue conectar
- Verifique se o container está rodando: `docker-compose ps`
- Verifique a porta: `docker-compose logs sqlserver`

### Remover tudo e começar do zero
```powershell
docker-compose down -v
docker volume prune
docker-compose up -d
```

---

**Criado em**: Janeiro 2026
**Versão**: 1.0
