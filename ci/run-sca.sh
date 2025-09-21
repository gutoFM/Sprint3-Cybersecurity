#!/bin/bash

# Detecta caminho do Java automaticamente
if [ -z "$JAVA_HOME" ]; then
  JAVA_PATH=$(readlink -f $(which java) | sed "s:/bin/java::")
  export JAVA_HOME=$JAVA_PATH
  export PATH=$JAVA_HOME/bin:$PATH
fi

echo "Usando JAVA_HOME=$JAVA_HOME"

# Rodar Dependency-Check via Docker
docker run --rm -v $(pwd):/src owasp/dependency-check:latest \
  --project "Sprint3-CSHARP" \
  --scan /src/Sprint3-CSHARP \
  --format HTML \
  --out /src/docs/dependency-check-report.html