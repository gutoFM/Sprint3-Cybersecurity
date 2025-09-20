#!/bin/bash
./depcheck/dependency-check/bin/dependency-check.sh \
  --project "Sprint3-CSHARP" \
  --scan Sprint3-CSHARP/ \
  --format "HTML" \
  --out ./docs/sca-report.html