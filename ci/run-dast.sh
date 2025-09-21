#!/bin/bash

# Fail script on first error
set -e

# Caminho para o projeto CandidatosApi
PROJECT_PATH="Sprint3-CSHARP/CandidatosApi/SprintApi.csproj"
OUTPUT_PATH="./docs/zap-report.html"
API_URL="http://localhost:5000"

echo "Subindo API em background"
dotnet run --project $PROJECT_PATH --urls $API_URL &
API_PID=$!

# Aguarda API subir (máx 20s)
echo "Aguardando API iniciar..."
for i in {1..20}; do
  if curl -s $API_URL >/dev/null; then
    echo "API está no ar em $API_URL"
    break
  fi
  sleep 2
done

# Rodar o OWASP ZAP baseline scan
echo "Executando OWASP ZAP contra $API_URL..."
docker run --rm -v $(pwd)/docs:/zap/wrk/:rw owasp/zap2docker-stable zap-baseline.py \
  -t $API_URL \
  -r zap-report.html

# Finaliza a API
kill $API_PID

echo "Relatório gerado em $OUTPUT_PATH"