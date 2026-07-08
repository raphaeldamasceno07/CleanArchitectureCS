# Script para Validar Integridade de Arquivos .csproj
# Execute: powershell -ExecutionPolicy Bypass -File validate-csproj.ps1

Write-Host "🔍 Validando integridade dos arquivos .csproj..." -ForegroundColor Cyan

$csprojFiles = Get-ChildItem -Recurse -Filter "*.csproj"
$healthy = 0
$corrupted = 0

foreach ($file in $csprojFiles) {
    try {
        # Tentar fazer parse como XML
        [xml]$content = Get-Content $file.FullName

        # Verificar tags suspeitas
        $fileContent = Get-Content $file.FullName -Raw
        $suspiciousTags = @("PropertyGrocp", "ImplicitCsings", "Ncllable", "ItemGrocp", "Inclcde", "FlcentValidation")

        $hasSuspicious = $false
        foreach ($tag in $suspiciousTags) {
            if ($fileContent -like "*$tag*") {
                $hasSuspicious = $true
                Write-Host "❌ $($file.Name)" -ForegroundColor Red
                Write-Host "   CORROMPIDO: Tag suspeita encontrada: $tag" -ForegroundColor Yellow
                $corrupted++
                break
            }
        }

        if (-not $hasSuspicious) {
            Write-Host "✅ $($file.Name)" -ForegroundColor Green
            $healthy++
        }
    }
    catch {
        Write-Host "❌ $($file.Name)" -ForegroundColor Red
        Write-Host "   ERRO: $($_.Exception.Message)" -ForegroundColor Yellow
        $corrupted++
    }
}

Write-Host ""
Write-Host "📊 Resultado:" -ForegroundColor Cyan
Write-Host "✅ Saudáveis: $healthy" -ForegroundColor Green
Write-Host "❌ Corrompidos: $corrupted" -ForegroundColor Red

if ($corrupted -gt 0) {
    Write-Host ""
    Write-Host "💡 Solução rápida:" -ForegroundColor Yellow
    Write-Host "git checkout -- *.csproj" -ForegroundColor Gray
    Write-Host "dotnet clean && dotnet build" -ForegroundColor Gray
}
