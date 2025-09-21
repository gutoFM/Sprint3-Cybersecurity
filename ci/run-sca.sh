#!/bin/bash

# Detecta caminho do Java automaticamente
if [ -z "$JAVA_HOME" ]; then
  JAVA_PATH=$(readlink -f $(which java) | sed "s:/bin/java::")
  export JAVA_HOME=$JAVA_PATH
  export PATH=$JAVA_HOME/bin:$PATH
fi

echo "Usando JAVA_HOME=$JAVA_HOME"

./depcheck/dependency-check/bin/dependency-check.sh \
  --project "Sprint3-CSHARP" \
  --scan Sprint3-CSHARP/ \
  --format "HTML" \
  --out ./docs/sca-report.html